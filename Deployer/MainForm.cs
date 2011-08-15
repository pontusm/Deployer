using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Deployer.Wizards.NewProject;
using DeployerEngine;
using DeployerEngine.Database;
using DeployerEngine.Database.Objects;
using DeployerEngine.Project;
using DeployerEngine.Timestamp;
using DeployerEngine.Util;
using DeployerPluginInterfaces;
using GlacialComponents.Controls;

namespace Deployer {

	public partial class MainForm : Form {
		#region Private enums

		private enum FileListColumns {
			LastModified = 1,
			Size = 2,
			DeployType = 3,
			RemotePath = 4,
			RemoteFileName = 5
		}

		private enum FileQueueListColumns {
			LastModified = 1,
			Size = 2,
			DeployType = 3,
			RemotePath = 4,
			RemoteName = 5,
			Progress = 6
		}

		private enum DatabaseListColumns {
			Type = 1,
			Progress = 2,
			Summary = 3
		}

		private enum FolderTreeIcons {
			FolderClosed = 0,
			FolderOpen = 1,
			ServerFromClient = 2
		}

		[Flags]
		private enum FolderTreeNodeFlags {
			None = 0,
			HasDeployFiles = 1,
			HasModifiedFiles = 2
		}
		#endregion

		#region Private constants

		private const string LastProjectRegKey = "LastProject";
		private const int MaxRecentFiles = 10;

		#endregion

		#region Private members

		private DeploymentProject _currentProject;
		private DeploymentStructure _deployStructure;
		private DatabaseComparison _databaseComparison;
		private DatabaseDeploymentStructure _scannedDbStructure;
		private StringCollection _recentfiles = new StringCollection();

		private ProgressForm _progressdialog;
		private DateTime _lastUploadTime = DateTime.MinValue;
		private DateTime _lastScanTime = DateTime.MinValue;
		private DateTime _lastProgressUpdate = DateTime.MinValue;
		private int _currentQueueItem;
		//private bool _isDirty;
		private string[] _args;
		private bool _restartAfterUpgrade;
		private TreeNode _rightclickedNode;

		#endregion

		#region Constructor

		public MainForm(string[] args) {
			_args = args;

			PersistWindowState.HandlePersistance(this, RegistryHandler.RegistryPath, false);

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		#endregion

		#region Main startup/shutdown

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args) {
			CustomExceptionHandler.HandleApplicationExceptions();

			Application.EnableVisualStyles();
			Application.DoEvents();
			Application.Run(new MainForm(args));
		}

		#region MainForm events (load/close)

		private void MainForm_Closed(object sender, EventArgs e) {
			if(_currentProject != null && _currentProject.FileName != null)
				RegistryHandler.SetValue(LastProjectRegKey, _currentProject.FileName);
			else
				RegistryHandler.DeleteValue(LastProjectRegKey);

			SaveRecentFilelist();
		}

		private void MainForm_Closing(object sender, CancelEventArgs e) {
			// Performing operation?
			if (_progressdialog != null)
				e.Cancel = true;
			else if (!CheckSave())
				e.Cancel = true; // Save failed or was cancelled
		}

		/// <summary>
		/// Handles the Load event of the MainForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void MainForm_Load(object sender, EventArgs e) {
			AddToLog(string.Format("Deployer v{0}\r\n", Application.ProductVersion), false);

			HookupEvents();

			LoadRecentFilelist();

			// Init plugin manager
			string pluginpath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Plugins");
			PluginManager.ScanPlugins(pluginpath);

			ProcessCommandLine();

			if (_currentProject == null) {
				// Load last project (if any)
				LoadLastProject();
				// Init new project
				//NewProject();
			}
			UpdateUI();
			//MessageBox.Show(this, "Testing new version 2", "Deployer test", MessageBoxButtons.OK, MessageBoxIcon.Information);
			if (_restartAfterUpgrade)
				AddToLog("Upgrade to new version was successful.");
		}

		private void LoadLastProject() {
			try {
				string lastproject = RegistryHandler.GetValue(LastProjectRegKey, null);
				if(lastproject == null)
					return;

				if(!File.Exists(lastproject))
					return;

				AddToLog("Loading last project...");
				LoadProject(lastproject);
			}
			catch(Exception ex) {
				AddToLog("Unable to load last project. Error: " + ex.Message);
			}
		}

		/// <summary>
		/// Process command line parameters
		/// </summary>
		private void ProcessCommandLine() {
			// Any file specified on command line?
			if (_args.Length == 0)
				return;

			string projectfilename = null;
			for (int i = 0; i < _args.Length; i++) {
				if (_args[i] == "/appstartversion")
					i++; // Skip version arg
				else if (_args[i] == "/restart")
					_restartAfterUpgrade = true;
				else if (!_args[i].StartsWith("/"))
					projectfilename = _args[i];
			}

			// Load project if specified
			if (projectfilename != null)
				LoadProject(projectfilename);
		}

		/// <summary>
		/// Hookup events fired by the deploy engine.
		/// </summary>
		private void HookupEvents() {
			EventManager.CommandSent += new DeployMessageHandler(EventManager_CommandSent);
			EventManager.ReplyReceived += new DeployMessageHandler(EventManager_ReplyReceived);
			EventManager.TransferBegin += new DeployFileTransferHandler(EventManager_TransferBegin);
			EventManager.TransferProgress += new DeployFileTransferHandler(EventManager_TransferProgress);
			EventManager.TransferComplete += new DeployFileTransferHandler(EventManager_TransferComplete);
			EventManager.DatabaseTransferBegin += new DeployDatabaseTransferHandler(EventManager_DatabaseTransferBegin);
			EventManager.DatabaseTransferComplete += new DeployDatabaseTransferHandler(EventManager_DatabaseTransferComplete);
			EventManager.NotificationMessage += new DeployMessageHandler(EventManager_Notification);
			EventManager.SourceTableFound += new DeployTableFoundHandler(EventManager_SourceTableFound);
			EventManager.TableComparisonComplete += new DeployTableComparisonCompleteHandler(EventManager_TableComparisonComplete);
		}

		#endregion
		
		#endregion

		#region Event handlers

		#region Deployer engine events

		private void EventManager_Notification(string message) {
			SetStatusText(message);
		}

		#region Database verification

		/// <summary>
		/// This occurs when verifying databases.
		/// </summary>
		private void EventManager_SourceTableFound(string tablename) {
			GLItem item = _databaselist.Items.Add(tablename);
			_progressdialog.TotalProgress.Maximum++;
			item.SubItems[(int) DatabaseListColumns.Type].Text = "Table";
			ProgressBar pb = new ProgressBar();
			pb.Maximum = 100;
			item.SubItems[(int) DatabaseListColumns.Progress].Control = pb;
			CheckDeployAbort();
			Application.DoEvents();
		}

		/// <summary>
		/// This occurs when verifying databases.
		/// </summary>
		private void EventManager_TableComparisonComplete(TableComparison comparison) {
			_progressdialog.TotalText = string.Format("Table '{0}' compared.", comparison.TableName);
			_progressdialog.TotalProgress.Value++;
			UpdateProgressPercent();

			if (comparison.SourceTable == null)
				return; // We only show source tables in the list

			// Find item to update
			GLItem item = FindDatabaseListItem(comparison.TableName);
			if (item == null)
				return;

			// Update item with information about the comparison
			item.Checked = comparison.IsEqual;
			item.Tag = comparison;
			item.SubItems[(int) DatabaseListColumns.Summary].Text = comparison.Summary;
			ProgressBar pb = (ProgressBar) item.SubItems[(int) DatabaseListColumns.Progress].Control;
			pb.Value = 100;

			if (!comparison.IsEqual) {
				item.ForeColor = Color.Red;
			}
			_databaselist.Refresh();
			CheckDeployAbort();
			Application.DoEvents();
		}

		#endregion

		#region Database deployment

		private void EventManager_DatabaseTransferBegin(DatabaseTransferEventArgs e) {
			_progressdialog.ItemText = string.Format("Deploying \"{0}\"...", e.DatabaseObject.Name);
			_progressdialog.ItemProgress.Value = 50;
			GLItem item = FindDatabaseListItem(e.DatabaseObject.Name);
			if (item == null)
				return;

			// Indicate progress
			ProgressBar pb = (ProgressBar) item.SubItems[(int) DatabaseListColumns.Progress].Control;
			pb.Value = 50;

			CheckDeployAbort();
			Application.DoEvents();
		}

		private void EventManager_DatabaseTransferComplete(DatabaseTransferEventArgs e) {
			_progressdialog.ItemProgress.Value = 100;
			_progressdialog.ItemText = string.Format("Deployed \"{0}\"...", e.DatabaseObject.Name);
			_progressdialog.TotalProgress.Value++;
			_progressdialog.TotalText =
				string.Format("Objects: {0}/{1}", _progressdialog.TotalProgress.Value, _progressdialog.TotalProgress.Maximum);
			UpdateProgressPercent();
			
			GLItem item = FindDatabaseListItem(e.DatabaseObject.Name);
			if (item == null)
				return;

			// Completed
			ProgressBar pb = (ProgressBar) item.SubItems[(int) DatabaseListColumns.Progress].Control;
			pb.Value = 100;

			CheckDeployAbort();
			Application.DoEvents();
		}

		/// <summary>
		/// Tries to locate the item with the specified name in the database list.
		/// </summary>
		/// <param name="objectname"></param>
		/// <returns></returns>
		private GLItem FindDatabaseListItem(string objectname) {
			// Locate source table in the list
			for (int i = 0; i < _databaselist.Items.Count; i++) {
				if (_databaselist.Items[i].Text == objectname) {
					return _databaselist.Items[i];
				}
			}
			return null;
		}
		
		#endregion

		#region File transfer events

		private void EventManager_CommandSent(string message) {
			AddToLog("FTP : " + message);
		}

		private void EventManager_ReplyReceived(string message) {
			AddToLog("FTP : " + message);
		}

		private void EventManager_TransferBegin(TransferEventArgs e) {
			// Find current item
			_currentQueueItem = -1; // Assume we can't find it
			for (int i = 0; i < _fileQueueList.Items.Count; i++) {
				if (_fileQueueList.Items[i].Text == e.Filename) {
					_currentQueueItem = i;
					_progressdialog.ItemProgress.Maximum = 100;
					_progressdialog.ItemProgress.Value = 0;
					_progressdialog.ItemText = string.Format("Transfering: \"{0}\"...", e.Filename);

					Application.DoEvents();
					break;
				}
			}
			CheckDeployAbort();
		}

		private void EventManager_TransferProgress(TransferEventArgs e) {
			if (_currentQueueItem >= 0) {
				int percent = CalculateTransferPercent(e.BytesSent, e.TotalBytes);
				_progressdialog.ItemProgress.Value = percent;

				ProgressBar pb = (ProgressBar)_fileQueueList.Items[_currentQueueItem].SubItems[(int)FileQueueListColumns.Progress].Control;
				pb.Value = percent;

				PumpProgressEvents();
			}

			CheckDeployAbort();
		}

		private void EventManager_TransferComplete(TransferEventArgs e) {
			_progressdialog.TotalProgress.Value++;
			_progressdialog.TotalText =
				string.Format("Files: {0}/{1}", _progressdialog.TotalProgress.Value, _progressdialog.TotalProgress.Maximum);
			UpdateProgressPercent();

			
			int percent = CalculateTransferPercent(e.BytesSent, e.TotalBytes);
			if (_currentQueueItem >= 0) {
				_progressdialog.ItemProgress.Value = percent;

				ProgressBar pb = (ProgressBar)_fileQueueList.Items[_currentQueueItem].SubItems[(int)FileQueueListColumns.Progress].Control;
				pb.Value = percent;

				if (percent < 100 || !_queueAutoRemove.Checked)
					_currentQueueItem++;		// Skip item
				else {
					_fileQueueList.Items.Remove(_currentQueueItem);		// Remove item
					_fileQueueList.Refresh();
				}
			}

			CheckDeployAbort();

			Application.DoEvents();
		}

		private static int CalculateTransferPercent(long bytessent, long totalbytes) {
			if (totalbytes <= 0)
				return 100;

			return (int)Math.Round(((float)bytessent / totalbytes) * 100);
		}

		/// <summary>
		/// Checks if user wants to abort the deploy process.
		/// </summary>
		private void CheckDeployAbort() {
			if (_progressdialog.IsCancelled)
				throw new DeployCancelException("User cancelled.");
		}

		#endregion

		#endregion

		#region AppUpdater events

		//private void _appUpdater_OnUpdateComplete(object sender, UpdateCompleteEventArgs e) {
		//    if (!e.UpdateSucceeded) {
		//        ShowError("Automatic upgrade failed.\n\nError: " + e.ErrorMessage);
		//        return;
		//    }

		//    DialogResult res =
		//        MessageBox.Show(this,
		//                        string.Format(
		//                            "A new version of Deployer is available.\n\nYour version: {0}\nNew version: {1}\n\nDo you wish to upgrade now?",
		//                            Application.ProductVersion, e.NewVersion), "Deployer upgrade", MessageBoxButtons.YesNo,
		//                        MessageBoxIcon.Question);
		//    if (res == DialogResult.No) {
		//        MessageBox.Show(this, "Automatic upgrade will be performed the next time you start the application.",
		//                        "Deployer upgrade", MessageBoxButtons.OK, MessageBoxIcon.Information);
		//        return;
		//    }

		//    // Restart app
		//    _appUpdater.RestartApp();
		//}

		#endregion

		#region Databaselist events

		private void _databaselist_DoubleClick(object sender, EventArgs e) {
			ShowDatabaseDetailsDialog();
		}

		private void _databaselistmenu_Popup(object sender, EventArgs e) {
			_menuIncludeDbObject.Enabled = _databaselist.SelectedItems.Count > 0;
			_menuExcludeDbObject.Enabled = _databaselist.SelectedItems.Count > 0;
			_menuIncludeAllDbObjects.Enabled = _databaselist.Items.Count > 0;
			_menuExcludeAllDbObjects.Enabled = _databaselist.Items.Count > 0;
			_menuViewDetails.Enabled = _databaselist.SelectedItems.Count > 0;
		}

		#endregion

		#region FileList events

		private void _filelist_DoubleClick(object sender, EventArgs e) {
			if (_filelist.SelectedItems.Count == 0)
				return;

			// Determine whether to open a folder
			if(_filelist.SelectedItems.Count == 1) {
				GLItem item = (GLItem)_filelist.SelectedItems[0];
				DirectoryInfo di = item.Tag as DirectoryInfo;
				if(di != null) {
					// This is a folder, open it
					foreach (TreeNode node in _folderTree.SelectedNode.Nodes) {
						if(node.Text == di.Name) {
							_folderTree.SelectedNode = node;
							node.EnsureVisible();
							return;
						}
					}
				}
			}

			AddSelectedFilesToQueue();
		}
		
		private void _fileListMenu_Opening(object sender, CancelEventArgs e) {
			//_menuInclude.Enabled = _filelist.SelectedItems.Count > 0;
			//_menuExclude.Enabled = _filelist.SelectedItems.Count > 0;
			_menuQueueFiles.Enabled = _filelist.SelectedItems.Count > 0;
			_menuSelectAll.Enabled = _filelist.Items.Count > 0;
			_menuUnselectAll.Enabled = _filelist.Items.Count > 0;
			//_menuAlwaysExclude.Enabled = _filelist.SelectedItems.Count > 0;
			_menuEditFiles.Enabled = _filelist.SelectedItems.Count > 0;

			//GLItem item = (GLItem)_filelist.SelectedItems[0];
			//_menuAlwaysExclude.Text = string.Format("Do not deploy files of type *{0} (in this project)", Path.GetExtension(item.Text));

			_menuInclude.Enabled = false;

			/* Temp disabled
			_menuInclude.DropDownItems.Clear();
			if(_filelist.SelectedItems.Count == 1) {
				GLItem item = (GLItem) _filelist.SelectedItems[0];
				if (!(item.Tag is DirectoryInfo)) {
					DeploymentFile file = item.Tag as DeploymentFile;

					// No matching filter?
					if (file == null) {
						_menuInclude.DropDownItems.Add(string.Format("Only \"{0}\"", item.Text), null,
													   new EventHandler(_menuIncludeSingle_Click));
					}
				}
			}
			_menuInclude.Enabled = _filelist.SelectedItems.Count > 0 && _menuInclude.DropDownItems.Count > 0;
			*/
		}

		#region Include file popup events

		private void _menuIncludeSingle_Click(object sender, EventArgs e) {
			if(_filelist.SelectedItems.Count == 0)
				return;

			GLItem item = (GLItem) _filelist.SelectedItems[0];

			string folderpath = GetFullPathForNode(_folderTree.SelectedNode);
			string virtualpath = PathHelper.GetVirtualPath(folderpath, _currentProject.LocalPathAbsolute);

			// Lookup or create filter settings for the virtual path
			FilterSettings fs = _currentProject.ActiveDeployConfig.FilterSettings.Get(virtualpath);
			if(fs == null) {
				fs = new FilterSettings(virtualpath);
				_currentProject.ActiveDeployConfig.FilterSettings.Add(fs);
			}
			fs.IncludeFiles.Add(new Filter(FilterExpressionType.ExactFileName, item.Text));
		}

		#endregion

		#endregion

		#region FileQueue events

		private void _fileQueueList_DoubleClick(object sender, EventArgs e) {
			RemoveSelectedQueueItems();
		}
		
		private void _fileQueueMenu_Opening(object sender, CancelEventArgs e) {
			_menuQueueDeploy.Enabled = _menuQueueClear.Enabled = !IsQueueEmpty;
			_menuQueueRemove.Enabled = _fileQueueList.SelectedItems.Count > 0;
			_menuQueueRemoveOther.Enabled = _fileQueueList.Items.Count > 0;
			_menuSelectAll.Enabled = _fileQueueList.Items.Count > 0;
			_menuUnselectAll.Enabled = _fileQueueList.Items.Count > 0;
		}

		#endregion
		
		#region Folder tree events

		private void _folderTree_AfterSelect(object sender, TreeViewEventArgs e) {
			RefreshFileList();
		}

		private void _folderTree_MouseDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Right)
				_rightclickedNode = _folderTree.GetNodeAt(e.X, e.Y);
			else
				_rightclickedNode = null;
		}

		private void _folderTreeMenu_Opening(object sender, CancelEventArgs e) {
			_menuQueueFolder.Enabled = _rightclickedNode != null;
		}

		#endregion

		private void _activeConfiguration_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
			ChangeConfiguration((string) e.ClickedItem.Tag);
		}

		#region Menu events

		private void _menuAbout_Click(object sender, EventArgs e) {
			AboutForm dlg = new AboutForm();
			dlg.ShowDialog(this);
		}
		
		private void _menuAlwaysExclude_Click(object sender, EventArgs e) {
			foreach (GLItem item in _filelist.SelectedItems) {
				// Find matching filters for the selected file
				DeploymentFile file = item.Tag as DeploymentFile;

				if (file != null) {
					DialogResult result =
						MessageBox.Show(this,
										string.Format("Are you sure you want to remove this filter?\r\n\r\nExpression: {0} ({1})",
													  file.MatchingFilter.Expression, file.MatchingFilter.ExpressionType), "Remove filter",
										MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if (result == DialogResult.Cancel)
						break; // Cancel operation

					if (result == DialogResult.Yes) {
						// Remove filter from project settings
						//_currentProject.ActiveFilter.IncludeFiles.Remove(file.MatchingFilter);
						item.Checked = false;
					}
				}
			}
		}

		private void _menuQueueClear_Click(object sender, EventArgs e) {
			ClearQueue();
		}

		private void _menuQueueRemove_Click(object sender, EventArgs e) {
			RemoveSelectedQueueItems();
		}

		private void _menuQueueRemoveOther_Click(object sender, EventArgs e) {
			RemoveNonSelectedQueueItems();
		}

		private void _menuCloseProject_Click(object sender, EventArgs e) {
			CloseProject();
		}
		
		private void _menuDeployDatabase_Click(object sender, EventArgs e) {
			DeployDatabase();
		}

		private void _menuExcludeDbObject_Click(object sender, EventArgs e) {
			foreach (GLItem item in _databaselist.SelectedItems) {
				item.Checked = false;
			}
		}

		private void _menuExcludeAllDbObjects_Click(object sender, EventArgs e) {
			foreach (GLItem item in _databaselist.Items) {
				item.Checked = false;
			}
		}

		private void _menuIncludeDbObject_Click(object sender, EventArgs e) {
			foreach (GLItem item in _databaselist.SelectedItems) {
				item.Checked = true;
			}
		}

		private void _menuIncludeAllDbObjects_Click(object sender, EventArgs e) {
			foreach (GLItem item in _databaselist.Items) {
				item.Checked = true;
			}
		}

		private void _menuSelectAll_Click(object sender, EventArgs e) {
			foreach (GLItem item in _filelist.Items) {
				item.Selected = true;
			}
		}

		private void _menuUnselectAll_Click(object sender, EventArgs e) {
			foreach (GLItem item in _filelist.Items) {
				item.Selected = false;
			}
		}

		private void _menuExit_Click(object sender, EventArgs e) {
			Close();
		}

		private void _menuNewProject_Click(object sender, EventArgs e) {
			NewProject();
		}

		private void _menuEditFiles_Click(object sender, EventArgs e) {
			EditSelectedFiles();
		}

		private void _menuOpenProject_Click(object sender, EventArgs e) {
			ShowOpenProjectDialog();
		}

		private void _menuConfiguration_Clicked(object sender, EventArgs e) {
			ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;
			ChangeConfiguration((string) menuitem.Tag);
		}

		private void _menuRecentProject_Clicked(object sender, EventArgs e) {
			if (!CheckSave())
				return;

			ToolStripMenuItem menuitem = (ToolStripMenuItem) sender;
			LoadProject(menuitem.Text);
		}

		private void _menuRefresh_Click(object sender, EventArgs e) {
			RefreshUI(false);
		}

		private void _menuQueueFolder_Click(object sender, EventArgs e) {
			AddSelectedFolderToQueue();
		}

		private void _menuQueueFiles_Click(object sender, EventArgs e) {
			AddSelectedFilesToQueue();
		}

		private void _menuQueueDeploy_Click(object sender, EventArgs e) {
			DeployQueue();
		}

		private void _menuQueueSelectAll_Click(object sender, EventArgs e) {
			foreach (GLItem item in _fileQueueList.Items) {
				item.Selected = true;
			}
		}

		private void _menuQueueUnselectAll_Click(object sender, EventArgs e) {
			foreach (GLItem item in _fileQueueList.Items) {
				item.Selected = false;
			}
		}
		
		private void _menuSaveProject_Click(object sender, EventArgs e) {
			SaveProject();
		}

		private void _menuSaveProjectAs_Click(object sender, EventArgs e) {
			SaveProjectAs();
		}

		private void _menuScanAll_Click(object sender, EventArgs e) {
			ScanAllFiles();
		}

		private void _menuScanDatabase_Click(object sender, EventArgs e) {
			ScanDatabase();
		}

		private void _menuScanLastDeployment_Click(object sender, EventArgs e) {
			ScanLastModifiedFiles();
		}

		private void _menuScanModified_Click(object sender, EventArgs e) {
			string tag = (string)((ToolStripMenuItem)sender).Tag;
			switch(tag) {
				case "last":
					ScanLastModifiedFiles();
					break;
				case "hour":
					ScanFiles(DateTime.Now.AddHours(-1));
					break;
				case "4hours":
					ScanFiles(DateTime.Now.AddHours(-4));
					break;
				case "yesterday":
					ScanFiles(DateTime.Now.AddDays(-1));
					break;
				case "twodays":
					ScanFiles(DateTime.Now.AddDays(-2));
					break;
				case "week":
					ScanFiles(DateTime.Now.AddDays(-7));
					break;
				case "month":
					ScanFiles(DateTime.Now.AddMonths(-1));
					break;
				case "date":
					ScanFilesModifiedSince();
					break;
			}
		}
		
		private void _menuSettings_Click(object sender, EventArgs e) {
			ShowProjectSettingsDialog();
		}

		private void _menuUpload_Click(object sender, EventArgs e) {
			DeployQueue();
		}

		private void _menuVerifyDatabase_Click(object sender, EventArgs e) {
			VerifyDatabase();
		}

		private void _menuViewDetails_Click(object sender, EventArgs e) {
			ShowDatabaseDetailsDialog();
		}

		#endregion

		#endregion

		#region Log window

		/// <summary>
		/// Adds a text to the log window and automatically inserts timestamp.
		/// </summary>
		/// <param name="text">The text to add to the log.</param>
		private void AddToLog(string text) {
			AddToLog(text, true);
		}

		/// <summary>
		/// Adds a text to the log window.
		/// </summary>
		/// <param name="text">The text to add to the log.</param>
		/// <param name="prependTime">Inserts time before log message if set to <c>true</c>.</param>
		private void AddToLog(string text, bool prependTime) {
			Debug.WriteLine(text);

			// Need to cut text?
			_logtext.MaxLength = 3000;
			if (_logtext.Text.Length + text.Length > _logtext.MaxLength) {
				int rowend = _logtext.Text.IndexOf("\r\n", text.Length);
				if (rowend > 0)
					_logtext.Text = _logtext.Text.Substring(rowend + 2);
			}
			if(prependTime) {
				_logtext.AppendText(string.Format("[{0}] ", DateTime.Now.ToLongTimeString()));
			}
			_logtext.AppendText(text);
			_logtext.AppendText("\r\n");
		}

		#endregion

		/// <summary>
		/// Changes the currently active configuration.
		/// </summary>
		/// <param name="name">The name of the configuration.</param>
		private void ChangeConfiguration(string name) {
			if (_currentProject == null || _currentProject.ActiveDeployConfigName == name)
				return;
			AddToLog(string.Format("Changing to configuration '{0}'.", name));
			_currentProject.ActiveDeployConfigName = name;
			ResetProjectState();
			UpdateLastUploadTime();
			RefreshUI(false);
		}

		#region Save/load/close/new project

		/// <summary>
		/// Closes the current project.
		/// </summary>
		private void CloseProject() {
			try {
				if (!CheckSave())
					return;

				// Close all
				ResetProjectState();
				_currentProject = null;
				//_isDirty = false;
				SetStatusText("Project closed.");
				UpdateMainFormTitle();
				UpdateUI();
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
		}

		/// <summary>
		/// Checks if project is dirty, and in that case lets user save.
		/// </summary>
		private bool CheckSave() {
			try {
				if (_currentProject != null && _currentProject.IsDirty()) {
					DialogResult result =
						MessageBox.Show(this, "Project has been modified. Do you wish to save it before closing?", "Project modified",
										MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					switch (result) {
						case DialogResult.Yes:
							if (!SaveProject())
								return false; // Save failed or was cancelled
							break;
						case DialogResult.No:
							break;
						case DialogResult.Cancel:
							return false; // Operation cancelled
					}
				}
				return true;
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
				return false;
			}
		}

		/// <summary>
		/// Initializes a new empty project.
		/// </summary>
		private void NewProject() {
			try {
				if (!CheckSave())
					return;

				// *** Wizard test
				NewProjectWizard wizard = new NewProjectWizard();
				DialogResult result = wizard.ShowDialog(this);
				if (result == DialogResult.Cancel)
					return;

				// Get new project
				_currentProject = wizard.Project;

				ResetProjectState();

				RefreshUI(false);
				SetStatusText("New project created.");
				UpdateMainFormTitle();
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
		}

		/// <summary>
		/// Rescan the deployment file structure.
		/// </summary>
		private void RescanStructure() {
			//AddToLog(string.Format("Scanning directory \"{0}\"...", _currentProject.LocalPathAbsolute));
			_deployStructure = DeploymentManager.Instance.ScanFiles(_currentProject, DateTime.MinValue);
			//AddToLog(string.Format("Found {0} file(s) to deploy.", _deployStructure.Files.Count));
		}

		/// <summary>
		/// Shows the open project file dialog.
		/// </summary>
		private void ShowOpenProjectDialog() {
			try {
				if (!CheckSave())
					return;

				OpenFileDialog dlg = new OpenFileDialog();
				dlg.DefaultExt = "deploy";
				dlg.Filter = "Deploy files (*.deploy)|*.deploy";
				dlg.Title = "Open Project";
				if (dlg.ShowDialog(this) == DialogResult.Cancel)
					return;

				LoadProject(dlg.FileName);
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
		}

		/// <summary>
		/// Loads the project with the specified filename.
		/// </summary>
		private void LoadProject(string filename) {
			try {
				ResetProjectState();
				SetStatusText(string.Format("Loading project \"{0}\".", filename));
				UpdateProgressPercent(5);

				LockUI();

				// Load project file
				_currentProject = DeploymentProject.Load(filename);

				//_isDirty = false;	// Must be set after loading since the loading itself fires the modification event
				
				// Load cached timestamp file
				UpdateLastUploadTime();

				AddRecentFile(filename);
				UpdateMainFormTitle();
				RefreshUI(false);
				SetStatusText(string.Format("Loaded project \"{0}\".", filename));
			}
			catch (FileLoadException ex) {
				ShowError(ex.Message);
			}
			catch (FileNotFoundException) {
				ShowError(string.Format("The file '{0}' could not be found.", filename));
			}
			catch (UnauthorizedAccessException ex) {
				MessageBox.Show(this, ex.Message, "Unable to load project", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
			UnlockUI();
		}

		/// <summary>
		/// Updates the last upload time from the locally cached timestamp file.
		/// </summary>
		private void UpdateLastUploadTime() {
			TimestampFile timestamp = DeploymentManager.Instance.GetLocalTimestampFile(_currentProject);
			if (timestamp != null) {
				_lastUploadTime = timestamp.LastDeployment;
				AddToLog(string.Format("Project was last deployed on {0}.", _lastUploadTime));
			}
			else {
				_lastUploadTime = DateTime.MinValue; // Clear last deploy time because it is unknown
				AddToLog("No last deployment time found. Project has never been deployed from this computer.");
			}
		}

		/// <summary>
		/// Populates the folder tree with the folders in the project.
		/// </summary>
		private void PopulateFolderTree(bool updateFolderStatus) {
			if (_currentProject == null)
				return;

			_folderTree.Nodes.Clear();

			TreeNode node = new TreeNode(_currentProject.Name, (int)FolderTreeIcons.ServerFromClient, (int)FolderTreeIcons.ServerFromClient);
			_folderTree.Nodes.Add(node);
			PopulateFolderTreeRecurse(node, _currentProject.LocalPathAbsolute);
			node.Expand();

			if(updateFolderStatus)
				UpdateFolderTree();			
		}

		private static void PopulateFolderTreeRecurse(TreeNode parent, string path) {
			string[] directories = Directory.GetDirectories(path);
			foreach(string dir in directories) {
				DirectoryInfo di = new DirectoryInfo(dir);
				if ((di.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden) {
					//EnhancedTreeNode node = new EnhancedTreeNode(di.Name, 0, 1);
					TreeNode node = new TreeNode(di.Name, (int) FolderTreeIcons.FolderClosed, (int) FolderTreeIcons.FolderOpen);
					//node.ForeColor = Color.LightGray;
					parent.Nodes.Add(node);
					PopulateFolderTreeRecurse(node, dir);
				}
			}
		}

		/// <summary>
		/// Updates the folder tree to to reflect status changes in the deployment files.
		/// </summary>
		private void UpdateFolderTree() {
			if (_folderTree.Nodes.Count == 0)
				return;
			
			UpdateFolderTreeRecurse(_folderTree.Nodes[0]);
		}

		private FolderTreeNodeFlags UpdateFolderTreeRecurse(TreeNode current) {
			// Examine children
			FolderTreeNodeFlags flags = FolderTreeNodeFlags.None;
			foreach (TreeNode child in current.Nodes) {
				flags |= UpdateFolderTreeRecurse(child);
			}
			
			// Examine files in current node
			IList<DeploymentFile> files = _deployStructure.GetFilesInDirectory(GetFullPathForNode(current));
			if(files.Count > 0) {
				flags |= FolderTreeNodeFlags.HasDeployFiles;

				// Any modified file?
				foreach (DeploymentFile file in files) {
					if (file.GetLastWriteTime() >= _lastUploadTime) {
						flags |= FolderTreeNodeFlags.HasModifiedFiles;
						break;
					}
				}
			}

			if((flags & FolderTreeNodeFlags.HasModifiedFiles) == FolderTreeNodeFlags.HasModifiedFiles)
				current.ForeColor = Color.Red;
			else if ((flags & FolderTreeNodeFlags.HasDeployFiles) == FolderTreeNodeFlags.HasDeployFiles)
				current.ForeColor = Color.Black;
			else
				current.ForeColor = Color.LightGray;

			return flags;
		}

		/// <summary>
		/// Resets variables and UI when a project is loaded or created.
		/// </summary>
		private void ResetProjectState() {
			_filelist.Items.Clear();
			_fileQueueList.Items.Clear();
			_databaselist.Items.Clear();
			_folderTree.Nodes.Clear();
			_lastUploadTime = DateTime.MinValue;
			_deployStructure = null;
			_scannedDbStructure = null;
			_databaseComparison = null;
		}

		/// <summary>
		/// Saves the project if it already has a filename, otherwise prompts the user.
		/// </summary>
		private bool SaveProject() {
			try {
				// Project has not been saved yet?
				if (_currentProject.FileName == null) {
					return SaveProjectAs();
				}

				_currentProject.Save(_currentProject.FileName);
				//_isDirty = false;
				AddRecentFile(_currentProject.FileName);
				UpdateMainFormTitle();
				UpdateUI();
				SetStatusText(string.Format("Saved project \"{0}\".", _currentProject.FileName));
				return true;
			}
			catch(UnauthorizedAccessException ex) {
				MessageBox.Show(this, ex.Message, "Unable to save project", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
				return false;
			}
		}

		/// <summary>
		/// Shows the SaveAs dialog and lets the user save the file.
		/// </summary>
		private bool SaveProjectAs() {
			try {
				SaveFileDialog dlg = new SaveFileDialog();
				dlg.DefaultExt = "deploy";
				dlg.Filter = "Deploy files (*.deploy)|*.deploy";
				dlg.Title = "Save Project As";
				if (dlg.ShowDialog(this) == DialogResult.Cancel)
					return false;

				_currentProject.Save(dlg.FileName);
				//_isDirty = false;
				AddRecentFile(dlg.FileName);
				UpdateMainFormTitle();
				RefreshUI(false);
				SetStatusText(string.Format("Saved project \"{0}\".", dlg.FileName));
				return true;
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
				return false;
			}
		}

		#endregion

		#region Queue handling

		/// <summary>
		/// Whether the queue is empty or not.
		/// </summary>
		private bool IsQueueEmpty {
			get {
				return _fileQueueList.Items.Count == 0;
			}
		}

		/// <summary>
		/// Contains the size of the deployment queue.
		/// </summary>
		private int QueueSize {
			get {
				return _fileQueueList.Items.Count;
			}
		}

		/// <summary>
		/// Adds the selected files in the file list to the queue.
		/// </summary>
		private void AddSelectedFilesToQueue() {
			bool added = false;
			foreach (GLItem item in _filelist.SelectedItems) {
				if (item.Tag != null && item.Tag is DeploymentFile) {
					added = true;
					AddToQueue((DeploymentFile) item.Tag);
				}
			}
			if (added)
				AddToLog("Files(s) added to queue.");
			else
				MessageBox.Show(this, "The file or files you selected have no deployment rules set up. You need to configure matching filters in order to deploy the files.",
								"No files found", MessageBoxButtons.OK, MessageBoxIcon.Information);
			UpdateUI();
		}

		/// <summary>
		/// Adds the selected folder to queue.
		/// </summary>
		private void AddSelectedFolderToQueue() {
			if(_rightclickedNode == null)
				return;

			string path = GetFullPathForNode(_rightclickedNode);
			AddFolderToQueue(path);

			UpdateUI();
		}

		/// <summary>
		/// Adds a file to the deployment queue.
		/// </summary>
		private void AddToQueue(DeploymentFile file) {
			GLItem item = _fileQueueList.Items.Add(file.LocalPath);
			item.SubItems[(int)FileQueueListColumns.LastModified].Text = file.GetLastWriteTime().ToString();
			item.SubItems[(int)FileQueueListColumns.Size].Text = Utility.FormatFileSize(file.Size);

			item.SubItems[(int)FileQueueListColumns.DeployType].Text = GetDeployType(file);
			item.SubItems[(int)FileQueueListColumns.RemotePath].Text = file.RemotePath;
			item.SubItems[(int)FileQueueListColumns.RemoteName].Text = file.RemoteName;

			ProgressBar pb = new ProgressBar();
			pb.Maximum = 100;
			item.SubItems[(int)FileQueueListColumns.Progress].Control = pb;

			item.Tag = file;
		}

		/// <summary>
		/// Adds all deployment files in a folder and its subfolders to the queue.
		/// </summary>
		/// <param name="directoryPath">The path to the folder.</param>
		private void AddFolderToQueue(string directoryPath) {
			IList<DeploymentFile> files = _deployStructure.GetFilesInDirectory(directoryPath);
			foreach(DeploymentFile file in files) {
				AddToQueue(file);
			}

			// Scan sub directories
			string[] dirs = Directory.GetDirectories(directoryPath);
			foreach(string dir in dirs) {
				AddFolderToQueue(dir);
			}
		}

		/// <summary>
		/// Clears the deployment queue.
		/// </summary>
		private void ClearQueue() {
			_fileQueueList.Items.Clear();
			UpdateUI();
		}

		/// <summary>
		/// Removes the queue items that are not selected.
		/// </summary>
		private void RemoveNonSelectedQueueItems() {
			try {
				LockUI();
				for (int i = _fileQueueList.Items.Count - 1; i >= 0; i--) {
					if (!_fileQueueList.Items[i].Selected)
						_fileQueueList.Items.Remove(i);
				}
				_fileQueueList.Refresh();
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
			UnlockUI();
		}

		/// <summary>
		/// Removes selected items from the queue.
		/// </summary>
		private void RemoveSelectedQueueItems() {
			try {
				LockUI();
				for (int i = _fileQueueList.Items.Count - 1; i >= 0; i--) {
					if (_fileQueueList.Items[i].Selected)
						_fileQueueList.Items.Remove(i);
				}
				_fileQueueList.Refresh();
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
			UnlockUI();
		}
		
		/// <summary>
		/// Gets a list of queued files.
		/// </summary>
		/// <returns></returns>
		private IList<DeploymentFile> GetQueuedFiles() {
			List<DeploymentFile> files = new List<DeploymentFile>();
			foreach(GLItem item in _fileQueueList.Items) {
				files.Add((DeploymentFile) item.Tag);
			}
			return files;
		}

		#endregion
		
		#region Edit selected files

		/// <summary>
		/// Open selected files in editor.
		/// </summary>
		private void EditSelectedFiles() {
			try {
				DialogResult result =
					MessageBox.Show(this, "Are you sure you want to edit the selected file(s)?", "Open file(s)",
									MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (result == DialogResult.No)
					return;

				foreach (GLItem item in _filelist.SelectedItems) {
					// Get file info
					DeploymentFile file = item.Tag as DeploymentFile;

					if(file != null) {
						try {
							ProcessStartInfo psi = new ProcessStartInfo(file.LocalPath);
							psi.Verb = "edit";
							Process.Start(psi);
						}
						catch (Win32Exception) {
							try {
								// Try to just open
								ProcessStartInfo psi = new ProcessStartInfo(file.LocalPath);
								Process.Start(psi);
							}
							catch (Win32Exception ex) {
								// Unable to open file as well!
								string errtxt = string.Format("Unable to edit file '{0}'. Error: {1}", file.Name, ex.Message);
								AddToLog(errtxt);
								DialogResult res = MessageBox.Show(this, errtxt, "Unable to edit file",
																   MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
								if (res == DialogResult.Cancel)
									return;							// Skip any other files as well

							}
						}
					}
				}
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
		}

		#endregion
		
		#region Manage recent file list

		/// <summary>
		/// Adds a file to the recent files list.
		/// </summary>
		private void AddRecentFile(string filename) {
			// Remove file if already present
			RemoveRecentFile(filename);

			_recentfiles.Insert(0, filename);

			// Limit nr of recent files
			while (_recentfiles.Count > MaxRecentFiles) {
				_recentfiles.RemoveAt(_recentfiles.Count - 1);
			}
		}

		/// <summary>
		/// Removes a file from the recent file list.
		/// </summary>
		private void RemoveRecentFile(string filename) {
			for (int i = _recentfiles.Count - 1; i >= 0; i--) {
				if (_recentfiles[i] == filename)
					_recentfiles.RemoveAt(i);
			}
		}

		/// <summary>
		/// Loads the recent file list.
		/// </summary>
		private void LoadRecentFilelist() {
			_recentfiles.Clear();
			for (int i = 0; i < MaxRecentFiles; i++) {
				string filename = RegistryHandler.GetValue("RecentFile" + i, null);
				if (filename != null && File.Exists(filename))
					_recentfiles.Add(filename);
			}
		}

		/// <summary>
		/// Saves the recent files list to the registry.
		/// </summary>
		private void SaveRecentFilelist() {
			for (int i = 0; i < _recentfiles.Count; i++) {
				RegistryHandler.SetValue("RecentFile" + i, _recentfiles[i]);
			}
		}

		#endregion

		#region File scanning and deployment

		/// <summary>
		/// Deploys the files in the queue.
		/// </summary>
		public void DeployQueue() {
			try {
				int totalfiles = _fileQueueList.Items.Count;
				_currentQueueItem = 0;

				UpdateMainFormTitle(0);
				ClearFileQueueProgressBars();
				//UpdateFileStructureUI();

				if(_currentProject.ActiveDeployConfig.Destinations.Count == 0) {
					MessageBox.Show(this, "You must add at least one deployment destination in the project settings.",
									"No deployment destination setup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (totalfiles == 0) {
					MessageBox.Show(this,
									"No files have been added to the deployment queue.",
									"No files to deploy", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				ShowProgressDialog("Deploying files", totalfiles);
				
				DateTime startTime = DateTime.Now;
				_tabcontrol.SelectedTab = _filetab;

				// Verify timestamp
				if (VerifyTimestamp()) {
					// Deploy files
					LockUI();

					DeploymentManager.Instance.DeployFiles(_currentProject, GetQueuedFiles());

					// All went well, so we save last deploy time
					_lastUploadTime = startTime;
					
					// Update folder tree to reflect changes
					UpdateFolderTree();
					RefreshFileList();
				}
			}
			catch(UnauthorizedAccessException ex) {
				MessageBox.Show(this, ex.Message, "Deploy failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
			CloseProgressDialog();
			UnlockUI();
		}


		/// <summary>
		/// Calculates how many files that have been marked for deployment.
		/// </summary>
		//private int GetTotalFilesToDeploy() {
		//    int count = 0;
		//    foreach (GLItem item in _filelist.Items) {
		//        if (item.Checked)
		//            count++;
		//    }
		//    return count;
		//}

		/// <summary>
		/// Verifies that no one else has deployed since we deployed.
		/// </summary>
		private bool VerifyTimestamp() {
			try {
				if(string.IsNullOrEmpty(_currentProject.ActiveDeployConfig.TimestampDestinationIdentifier)) {
					// Timestamping not used
					return true;
					//    MessageBox.Show(this,
					//                    "No destination has been selected for timestamping handling. You can configure this in the project settings.",
					//                    "No timestamp destination", MessageBoxButtons.OK, MessageBoxIcon.Information);
					//    return false;
				}
				//if (!DeploymentManager.Instance.HasTimestampSupport(_currentProject.ActiveDeployConfig.TimestampDestinationIdentifier)) {
				//    MessageBox.Show(this,
				//                    string.Format("Plugin for destination '{0}' does not have support for timestamp handling.",
				//                                  _currentProject.ActiveDeployConfig.TimestampDestinationIdentifier), "No timestamp plugin",
				//                    MessageBoxButtons.OK, MessageBoxIcon.Information);
				//    return false;
				//}

				SetStatusText("Verifying timestamp...");
				LockUI();
				TimestampFile localtimestamp = DeploymentManager.Instance.GetLocalTimestampFile(_currentProject);
				TimestampFile remotetimestamp = DeploymentManager.Instance.GetRemoteTimestampFile(_currentProject);
				UnlockUI();

				if (localtimestamp == null || remotetimestamp == null)
					return true;

				if (remotetimestamp.LastDeployment > localtimestamp.LastDeployment) {
					DialogResult res =
						MessageBox.Show(this,
										string.Format(
											"Project has been deployed by {0} ({1}) since you last deployed. Are you sure you want to continue?",
											remotetimestamp.DeployedBy, remotetimestamp.LastDeployment), "Deployment warning",
										MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (res == DialogResult.No)
						return false;
				}
				return true;
			}
			catch (Exception) {
				UnlockUI();
				throw;
			}
		}

		/// <summary>
		/// Clears all the small progress bars for the files in the queue.
		/// </summary>
		private void ClearFileQueueProgressBars() {
			foreach (GLItem item in _fileQueueList.Items) {
				ProgressBar pb = (ProgressBar)item.SubItems[(int)FileQueueListColumns.Progress].Control;
				pb.Value = 0;
			}
			Application.DoEvents();
		}

		/// <summary>
		/// Refreshes all UI views.
		/// </summary>
		private void RefreshUI(bool updateFolderStatus) {
			RefreshConfigurations();
			UpdateProgressPercent(50);
			RefreshStructure(updateFolderStatus);
			UpdateProgressPercent(75);
			RefreshFileList();
			UpdateProgressPercent(90);
			UpdateUI();
			UpdateProgressPercent(100);
		}

		private void RefreshConfigurations() {
			_menuConfiguration.DropDownItems.Clear();
			_activeConfiguration.DropDownItems.Clear();
			if (_currentProject == null)
				return;

			foreach (DeployConfig config in _currentProject.DeployConfigurations) {
				ToolStripItem item1 = _activeConfiguration.DropDown.Items.Add(config.DisplayName);
				item1.Tag = config.Name;

				ToolStripMenuItem item2 = new ToolStripMenuItem(config.DisplayName, null, _menuConfiguration_Clicked);
				item2.Tag = config.Name;
				if(config.Name == _currentProject.ActiveDeployConfigName)
					item2.Checked = true;
				_menuConfiguration.DropDownItems.Add(item2);
			}

			_activeConfiguration.Text = _currentProject.ActiveDeployConfig.DisplayName;
		}

		/// <summary>
		/// Refreshes the deployment file structure.
		/// </summary>
		private void RefreshStructure(bool updateFolderStatus) {
			try {
				SetStatusText("Scanning file structure...");
				LockUI();
				RescanStructure();
				PopulateFolderTree(updateFolderStatus);
				SetStatusText("Scanning complete.");
			}
			catch(DirectoryNotFoundException ex) {
				ShowError(ex.Message);
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
			UnlockUI();
		}

		/// <summary>
		/// Shows a dialog that lets the user specify the last modified date for the files to scan.
		/// </summary>
		private void ScanFilesModifiedSince() {
			try {
				LastModifiedForm frm = new LastModifiedForm();
				if (_lastScanTime != DateTime.MinValue)
					frm.SelectedDate = _lastScanTime;					
				if (frm.ShowDialog(this) == DialogResult.Cancel)
					return;

				ScanFiles(frm.SelectedDate);
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
		}

		/// <summary>
		/// Scans files that have been modified since we last uploaded, or from a specified date.
		/// </summary>
		private void ScanLastModifiedFiles() {
			try {
				if (_lastUploadTime == DateTime.MinValue)
					ScanFilesModifiedSince();
				else
					ScanFiles(_lastUploadTime);
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
		}

		/// <summary>
		/// Adds all files to the queue.
		/// </summary>
		private void ScanAllFiles() {
			ScanFiles(DateTime.MinValue);
		}

		/// <summary>
		/// Scans files that should be deployed.
		/// </summary>
		/// <param name="modifiedSince"></param>
		private void ScanFiles(DateTime modifiedSince) {
			try {
				if (_deployStructure == null)
					throw new ApplicationException("There is no structure to deploy.");

				SetStatusText(string.Format("Scanning directory \"{0}\"...", _currentProject.LocalPathAbsolute));
				_tabcontrol.SelectedTab = _filetab;
				_lastScanTime = modifiedSince;

				LockUI();
				ClearQueue();

				RescanStructure();
				foreach(DeploymentFile file in _deployStructure.Files) {
					if(file.GetLastWriteTime() >= modifiedSince)
						AddToQueue(file);
				}
				
				if(QueueSize > 0)
					SetStatusText(string.Format("Added {0} file(s) to the queue.", QueueSize));
				else
					SetStatusText("No files were found.");
				
				UpdateFolderTree();
				RefreshFileList();
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
			UnlockUI();
		}

		/// <summary>
		/// Populates the file list with the files in the specified path.
		/// </summary>
		/// <param name="path"></param>
		private void PopulateFileList(string path) {
			if(!Directory.Exists(path)) {
				ShowError("Directory not found.");
				return;
			}

			IDictionary<string, DeploymentFile> deployfiles = _deployStructure.GetFilesInDirectoryAsTable(path);

			_filelist.Items.Clear();

			// Add directories
			string[] dirs = Directory.GetDirectories(path);
			foreach (string dir in dirs) {
				DirectoryInfo di = new DirectoryInfo(dir);
				if ((di.Attributes & FileAttributes.Hidden) == 0) {
					GLItem item = _filelist.Items.Add(di.Name);
					item.Tag = di;
					item.ForeColor = Color.DodgerBlue;
				}
			}

			// Add files
			string[] files = Directory.GetFiles(path);
			foreach(string file in files) {
				//FileAttributes attributes = File.GetAttributes(file);
				//if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
				//    continue;
				
				GLItem item = _filelist.Items.Add(Path.GetFileName(file));

				//PictureBox pb = new PictureBox();
				//Bitmap bm = FileInfoHelper.GetFileIconAsBitmap(file);
				//pb.Size = bm.Size;
				//pb.Image = bm;
				//item.SubItems[(int) FileListColumns.Icon].Control = pb;

				item.SubItems[(int)FileListColumns.LastModified].Text = File.GetLastWriteTime(file).ToString();
				item.SubItems[(int)FileListColumns.Size].Text = Utility.FormatFileSize(new FileInfo(file).Length);

				// Check if file is among the deploy files
				string key = file.ToLower();
				if(deployfiles.ContainsKey(key)) {
					DeploymentFile df = deployfiles[key];
					item.SubItems[(int)FileListColumns.DeployType].Text = GetDeployType(df);
					item.SubItems[(int)FileListColumns.RemotePath].Text = df.RemotePath;
					item.SubItems[(int)FileListColumns.RemoteFileName].Text = df.RemoteName;

					item.Tag = df;

					if (df.GetLastWriteTime() >= _lastUploadTime)
						item.ForeColor = Color.Red;
				}
				else
					item.ForeColor = Color.LightGray;
			}
		}

		/// <summary>
		/// Gets the display name for the deploy plugin type.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		private string GetDeployType(DeploymentFile file) {
			//string id = _currentProject.GetDeployPluginIdentifier(file);
			//IDeployerPlugin plugin = PluginManager.Plugins.Get(id);
			//if (plugin == null)
			//    return "Unknown";
			//return plugin.Name;
			DeployDestination destination = _currentProject.ActiveDeployConfig.Destinations.Get(file.DeployDestinationIdentifier);
			if (destination == null)
				return "Unknown";
			return destination.Name;
		}
		
		#endregion

		#region Database scanning and deployment

		/// <summary>
		/// Deploys the database.
		/// </summary>
		private void DeployDatabase() {
			try {
				UpdateDatabaseStructureUI();
				UpdateMainFormTitle(0);
				ClearDatabaseProgressBars();

				int totalobjects = _scannedDbStructure.GetTotalObjectsToDeploy();
				if (totalobjects == 0) {
					MessageBox.Show(this, "No objects have been selected for deployment.", "No files to deploy", MessageBoxButtons.OK,
									MessageBoxIcon.Information);
					return;
				}

				ShowProgressDialog("Deploying database", totalobjects);

				_tabcontrol.SelectedTab = _databasetab;
				LockUI();

				// Deploy objects
				DeploymentManager.Instance.DeployDatabase(_scannedDbStructure);
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
			CloseProgressDialog();
			UnlockUI();
		}

		/// <summary>
		/// Clears all the small progress bars for the database objects.
		/// </summary>
		private void ClearDatabaseProgressBars() {
			foreach (GLItem item in _databaselist.Items) {
				ProgressBar pb = (ProgressBar) item.SubItems[(int) DatabaseListColumns.Progress].Control;
				pb.Value = 0;
			}
			Application.DoEvents();
		}

		/// <summary>
		/// Scans the database to find the objects to deploy.
		/// </summary>
		private void ScanDatabase() {
			try {
				if (_currentProject.ActiveDeployConfig.DatabaseSettings.Databases.Count == 0) {
					MessageBox.Show(this, "No databases have been configured.", "Configuration missing", MessageBoxButtons.OK,
									MessageBoxIcon.Exclamation);
					return;
				}

				SetStatusText("Scanning database...");
				_tabcontrol.SelectedTab = _databasetab;
				LockUI();
				_databaselist.Items.Clear();

				// Just pick the first pair (GUI has no support for multiple database pairs yet...)
				DatabasePair databases = _currentProject.ActiveDeployConfig.DatabaseSettings.Databases[0];

				// Scan structure
				_scannedDbStructure = DeploymentManager.Instance.ScanDatabase(_currentProject, databases);
				SetStatusText(string.Format("Found {0} database object(s) to deploy.", _scannedDbStructure.DatabaseObjects.Count));
				PopulateDatabaseList(_scannedDbStructure);
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
			UnlockUI();
		}

		/// <summary>
		/// Populates the database list.
		/// </summary>
		private void PopulateDatabaseList(DatabaseDeploymentStructure structure) {
			// Add objects
			foreach (IDatabaseObject obj in structure.DatabaseObjects) {
				GLItem item = _databaselist.Items.Add(obj.Name);
				item.Tag = obj;
				item.Checked = true;
				item.SubItems[(int)DatabaseListColumns.Type].Text = obj.TypeName;
				ProgressBar pb = new ProgressBar();
				pb.Maximum = 100;
				item.SubItems[(int)DatabaseListColumns.Progress].Control = pb;
				if (obj.IsNew)
					item.ForeColor = Color.Blue;
			}
		}

		public bool HasConfiguredDatabases {
			get {
				if (_currentProject == null || _currentProject.ActiveDeployConfig.DatabaseSettings.Databases.Count == 0)
					return false;

				// Just pick the first pair (GUI has no support for multiple database pairs yet...)
				DatabasePair databases = _currentProject.ActiveDeployConfig.DatabaseSettings.Databases[0];

				if (databases.Source.Url.Length == 0 || databases.Destination.Url.Length == 0)
					return false;

				return true;
			}
		}		

		/// <summary>
		/// Verifies the database
		/// </summary>
		private void VerifyDatabase() {
			try {
				if(!HasConfiguredDatabases) {
					MessageBox.Show(this, "No databases have been configured.", "Configuration missing", MessageBoxButtons.OK,
									MessageBoxIcon.Exclamation);
					return;
				}

				// Just pick the first pair (GUI has no support for multiple database pairs yet...)
				DatabasePair databases = _currentProject.ActiveDeployConfig.DatabaseSettings.Databases[0];

				SetStatusText("Verifying databases...");
				_tabcontrol.SelectedTab = _databasetab;
				
				ShowProgressDialog("Verifying databases", 0);
				_progressdialog.ItemText = "Scanning tables...";

				LockUI();
				_databaselist.Items.Clear();

				// **** TEST
				//DatabasePair databases = new DatabasePair();
				//databases.Source = new DatabaseDescriptor("Internal", "http://ecdev.stendahls.net/Custom/Admin/DatabaseInfo.asmx");
				//databases.Destination = new DatabaseDescriptor("Internal", "http://localhost/webservices/ServerDatabase/DatabaseInfo.asmx");
				// *********

				_databaseComparison = DeploymentManager.Instance.CompareDatabases(databases);

				//PopulateFileList(_scannedStructure);
				if(_databaseComparison != null)
					SetStatusText("Verification completed.");
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
			CloseProgressDialog();
			UnlockUI();
		}
		
		#endregion

		#region Dialog handling

		/// <summary>
		/// View details of a database comparison.
		/// </summary>
		private void ShowDatabaseDetailsDialog() {
			try {
				if (_databaselist.SelectedItems.Count == 0)
					return;

				GLItem item = (GLItem)_databaselist.SelectedItems[0];
				if (item.Tag is TableComparison) {
					TableComparison comparison = (TableComparison)item.Tag;

					DatabaseDetailsForm frm = new DatabaseDetailsForm();
					frm.Comparison = comparison;
					frm.ShowDialog(this);
				}
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
		}

		/// <summary>
		/// Shows the progress dialog.
		/// </summary>
		private void ShowProgressDialog(string title, int totalitems) {
			if (_progressdialog != null)
				throw new ApplicationException("Progress dialog already open!");
			
			_progressdialog = new ProgressForm();
			
			_progressdialog.ResetDialog(title);
			_progressdialog.TotalProgress.Maximum = totalitems;
			_progressdialog.Show(this);
		}

		private void CloseProgressDialog() {
			if (_progressdialog != null) {
				_progressdialog.Close();
				_progressdialog = null;
			}
		}

		/// <summary>
		/// Edits the settings for the server to deploy to.
		/// </summary>
		private void ShowProjectSettingsDialog() {
			try {
				// Save project state
				byte[] state = _currentProject.SaveState();
				
				// Show dialog
				ProjectSettingsForm frm = new ProjectSettingsForm();
				frm.Project = _currentProject;
				if (frm.ShowDialog(this) == DialogResult.Cancel) {
					// Recall old project state
					_currentProject = DeploymentProject.LoadState(state);
					return;
				}

				RefreshUI(false);
			}
			catch (Exception ex) {
				ShowNonFatalException(ex);
			}
		}

		/// <summary>
		/// Shows a message box with an error text and an error icon.
		/// </summary>
		/// <param name="message"></param>
		private void ShowError(string message) {
			MessageBox.Show(this, message, "Deployer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		/// <summary>
		/// Shows a non fatal error report.
		/// </summary>
		/// <param name="ex"></param>
		private void ShowNonFatalException(Exception ex) {
			Debug.WriteLine(ex.ToString());
			if(ex is ApplicationException || ex is DirectoryNotFoundException)
				ShowError(ex.Message);
			else
				ErrorForm.ShowErrorDialog(this, ex);
		}
		
		#endregion
		
		#region UI handling

		/// <summary>
		/// Locks the UI so that no buttons can be pressed. This is done during long work operations
		/// to prevent the user from re-executing operations before finished. A call to UnlockUI must
		/// be done to enable UI again.
		/// </summary>
		private void LockUI() {
			_toolstripTop.Enabled = false;
			//_editProject.Enabled = false;
			_folderTree.Enabled = false;
			_filelist.ContextMenuStrip = null;
			_fileQueueList.ContextMenuStrip = null;
			_databaselist.ContextMenu = null;

			//DisableMenu(_mainmenu);
			_mainmenu.Enabled = false;

			Application.DoEvents();

			Cursor = Cursors.WaitCursor;
		}

		/// <summary>
		/// Sets the text in the status bar and also
		/// add the text to the log window.
		/// </summary>
		private void SetStatusText(string text) {
			_statusLabel.Text = text;
			AddToLog(text);
		}

		/// <summary>
		/// Unlocks a previously locked UI so that it can be used again. This will also automatically call
		/// UpdateUI() to ensure that the UI is in the right state.
		/// </summary>
		private void UnlockUI() {
			Cursor = Cursors.Default;

			_toolstripTop.Enabled = true;
			//_editProject.Enabled = true;
			_folderTree.Enabled = true;
			_filelist.ContextMenuStrip = _fileListMenu;
			_fileQueueList.ContextMenuStrip = _fileQueueMenu;
			_databaselist.ContextMenu = _databaselistmenu;

			//EnableMenu(_mainmenu);
			_mainmenu.Enabled = true;

			UpdateUI();
		}

		/// <summary>
		/// Updates the database deployment structure with the changes made in the UI database object list.
		/// </summary>
		private void UpdateDatabaseStructureUI() {
			foreach (GLItem item in _databaselist.Items) {
				IDatabaseObject obj = (IDatabaseObject)item.Tag;
				obj.IncludeInDeployment = item.Checked;
			}
		}

		/// <summary>
		/// Updates the file list for the currently selected node in the node tree,
		/// or clears it if no node is selected.
		/// </summary>
		private void RefreshFileList() {
			if(_folderTree.SelectedNode != null) {
				// Extract node path without root node (always the project name)
				string fullpath = GetFullPathForNode(_folderTree.SelectedNode);
				PopulateFileList(fullpath);
			}
			else {
				_filelist.Items.Clear();
			}
		}

		/// <summary>
		/// Returns the full physical directory path for the specified tree node.
		/// </summary>
		private string GetFullPathForNode(TreeNode node) {
			string nodepath;
			if (_currentProject != null && !string.IsNullOrEmpty(_currentProject.Name) && node.FullPath.Length >= _currentProject.Name.Length)
				nodepath = node.FullPath.Substring(_currentProject.Name.Length);
			else
				nodepath = node.FullPath;

			// Strip path separator
			if (nodepath.StartsWith(_folderTree.PathSeparator))
				nodepath = nodepath.Substring(_folderTree.PathSeparator.Length);

			return Path.Combine(_currentProject.LocalPathAbsolute, nodepath);
		}
		
		/// <summary>
		/// Updates the main form window title after the filename has changed.
		/// </summary>
		private void UpdateMainFormTitle() {
			UpdateMainFormTitle(-1);
		}

		/// <summary>
		/// Updates the main window title.
		/// </summary>
		/// <param name="percent">The percent to show in the title. Use -1 to remove percent from title.</param>
		private void UpdateMainFormTitle(int percent) {
			string title = string.Empty;

			//string percentText = string.Empty;
			if (percent != -1)
				title += string.Format("[{0}%] - ", percent);

			//string projectNameText = string.Empty;
			if (_currentProject != null) {
				if(_currentProject.ActiveDeployConfig != null)
					title += string.Format("{0} - ", _currentProject.ActiveDeployConfig.DisplayName);
				if (!string.IsNullOrEmpty(_currentProject.Name))
					title += string.Format("{0} - ", _currentProject.Name);
			}

			Text = title + "Deployer";
		}

		/// <summary>
		/// Updates percent indicators based on the value of the main progress bar.
		/// </summary>
		private void UpdateProgressPercent() {
			int percent = (int)Math.Round(((float)_progressdialog.TotalProgress.Value / _progressdialog.TotalProgress.Maximum) * 100);
			UpdateProgressPercent(percent);
		}

		/// <summary>
		/// Updates the progress indicators.
		/// </summary>
		/// <param name="percent">The current percent to show.</param>
		private void UpdateProgressPercent(int percent) {
			_statusProgressBar.Text = percent + "%";
			_statusProgressBar.Value = percent;
			if (!_statusProgressBar.Visible)
				_statusProgressBar.Visible = true;
			UpdateMainFormTitle(percent);
			PumpProgressEvents();
		}

		private void PumpProgressEvents() {
			// Only pump events if more than one second passed
			if (_lastProgressUpdate.AddSeconds(1) < DateTime.Now) {
				Application.DoEvents();
				_lastProgressUpdate = DateTime.Now;
			}
		}

		/// <summary>
		/// Updates the UI to reflect the current state of the program.
		/// </summary>
		private void UpdateUI() {
			//if(_currentProject != null) {
			//    _localPath.Text = _currentProject.LocalPathAbsolute;
			//}
			//else {
			//    _localPath.Text = string.Empty;
			//}

			bool hasProject = _currentProject != null;

			_menuSettings.Enabled = _btnSettings.Enabled =
				_menuSaveProject.Enabled = _menuSaveProjectAs.Enabled = _btnSave.Enabled =
				_menuCloseProject.Enabled =
				_menuRefresh.Enabled = _btnRefresh.Enabled =
				_menuQueue.Enabled = _btnQueueAll.Enabled = _btnQueueModified.Enabled =
				_menuConfiguration.Enabled =
				_menuVerifyDatabase.Enabled = _btnVerifyDatabase.Enabled = hasProject;
			
			_menuQueueLastDeployment.Enabled = _btnQueueSinceLastDeploy.Enabled = hasProject && (_lastUploadTime != DateTime.MinValue);
			_menuDeployQueue.Enabled = _btnDeployQueue.Enabled = (_deployStructure != null && !IsQueueEmpty);

			_menuVerifyDatabase.Enabled = _btnVerifyDatabase.Enabled = HasConfiguredDatabases;
			_menuScanDatabase.Enabled =
				_btnScanDatabase.Enabled = hasProject && (_databaseComparison != null && _databaseComparison.IsEqual);
			_menuDeployDatabase.Enabled = _btnDeployDatabase.Enabled = hasProject && _scannedDbStructure != null;

			// Merge scan menu
			//_menuScan.MenuItems.Clear();
			//_menuScan.MergeMenu(_scanmenu);

			// Rebuild recent files sub menu
			_menuRecentProjects.DropDownItems.Clear();
			if (_recentfiles.Count > 0) {
				foreach (string filename in _recentfiles) {
					_menuRecentProjects.DropDownItems.Add(filename, null, new EventHandler(_menuRecentProject_Clicked));
				}
			}
			else {
				ToolStripMenuItem menuitem = new ToolStripMenuItem("Empty");
				menuitem.Enabled = false;
				_menuRecentProjects.DropDownItems.Add(menuitem);
			}

			// Time to reset progress status?
			if (_statusProgressBar.Visible && _lastProgressUpdate.AddSeconds(10) < DateTime.Now) {
				_statusProgressBar.Visible = false;
				Application.DoEvents();
			}
		}
		
		#endregion


	}
}