using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DeployerEngine;
using DeployerEngine.Project;
using DeployerEngine.Util;
using DeployerPluginInterfaces;
using GlacialComponents.Controls;

namespace Deployer
{
	/// <summary>
	/// Summary description for ProjectSettings.
	/// </summary>
	public partial class ProjectSettingsForm : Form
	{
		private enum PluginListColumns {
			Identifier = 1,
			Version = 2
		}

		private enum ExcludeProceduresListColumns {
			Expression = 0,
			ExpressionType = 1
		}

		private DeploymentProject _project;

		public ProjectSettingsForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Gets or sets the project to edit settings for.
		/// </summary>
		public DeploymentProject Project {
			get { return _project; }
			set { _project = value; }
		}

		#region Event handlers

		private void ProjectSettingsForm_Load(object sender, EventArgs e) {
			_destinationsView.Project = _project;
			_filtersView.Project = _project;
			_timestampView.Project = _project;
			
			_localpath.Text = _project.LocalPath;

			UpdatePluginList();
			
/*

			// Update include files filter
			DataTable dt = new DataTable();
			dt.Columns.Add("Expression");
			dt.Columns.Add("Type", typeof(FilterExpressionType));
			dt.Columns.Add("Deploy With");
			dt.Columns.Add("Remote Path");
			foreach (Filter filter in _project.ActiveFilter.IncludeFiles) {
				DataRow row = dt.NewRow();
				row[(int)IncludeFilesListColumns.Expression] = filter.Expression;
				row[(int)IncludeFilesListColumns.ExpressionType] = filter.ExpressionType;
				row[(int)IncludeFilesListColumns.DeployWith] = _project.GetDeployPluginIdentifier(filter);
				row[(int)IncludeFilesListColumns.RemotePath] = filter.RemotePath != null ? filter.RemotePath : string.Empty;
				dt.Rows.Add(row);
			}
			_includefiles.DataSource = dt;

			// Update exclude directories filter
			dt = new DataTable();
			dt.Columns.Add("Expression");
			dt.Columns.Add("Type", typeof(FilterExpressionType));
			foreach (Filter filter in _project.ActiveFilter.ExcludeDirectories) {
				DataRow row = dt.NewRow();
				row[(int)ExcludeDirectoriesListColumns.Expression] = filter.Expression;
				row[(int)ExcludeDirectoriesListColumns.ExpressionType] = filter.ExpressionType;
				dt.Rows.Add(row);
			}
			_excludeDirectories.DataSource = dt;

			// Update exclude files filter
			dt = new DataTable();
			dt.Columns.Add("Expression");
			dt.Columns.Add("Type", typeof(FilterExpressionType));
			foreach (Filter filter in _project.ActiveFilter.ExcludeFiles) {
				DataRow row = dt.NewRow();
				row[(int)ExcludeFilesListColumns.Expression] = filter.Expression;
				row[(int)ExcludeFilesListColumns.ExpressionType] = filter.ExpressionType;
				dt.Rows.Add(row);
			}
			_excludeFiles.DataSource = dt;
*/
			// Update exclude stored procedures filter
			DataTable dt = new DataTable();
			dt.Columns.Add("Expression");
			dt.Columns.Add("Type", typeof(FilterExpressionType));
			foreach (Filter filter in _project.ActiveDeployConfig.DatabaseSettings.ExcludeProcedures) {
				DataRow row = dt.NewRow();
				row[(int)ExcludeProceduresListColumns.Expression] = filter.Expression;
				row[(int)ExcludeProceduresListColumns.ExpressionType] = filter.ExpressionType;
				dt.Rows.Add(row);
			}
			_excludeProcedures.DataSource = dt;

			//Update checkbox
			_useProjectFilter.Checked = _project.ActiveDeployConfig.UseProjectFilter;

			// Update database (project settings can handle many databases, but the GUI cannot do it - yet...)
			if(_project.ActiveDeployConfig.DatabaseSettings.Databases.Count > 0) {
				DatabasePair db = _project.ActiveDeployConfig.DatabaseSettings.Databases[0];
				_sourcedbname.Text = db.Source.Name;
				_sourcedburl.Text = db.Source.Url;
				_destinationdbname.Text = db.Destination.Name;
				_destinationdburl.Text = db.Destination.Url;
			}
		}

		private void _browse_Click(object sender, EventArgs e) {
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.Description = "Please select a directory";
			dlg.SelectedPath = _project.LocalPathAbsolute;

			if(dlg.ShowDialog(this) == DialogResult.Cancel)
				return;

			string newpath = dlg.SelectedPath;

			// If project has been saved, maybe we should try to make a relative path
			if(_project.FileName != null && Path.IsPathRooted(newpath)) {
				newpath = PathHelper.GetRelativePath(_project.FileName, newpath);
			}

			_localpath.Text = newpath;
		}

		private void _cancel_Click(object sender, EventArgs e) {
			// Reload old settings for all plugins
			PluginManager.LoadPluginSettings(_project);

			this.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void _ok_Click(object sender, EventArgs e) {
			try {
				StoreSettings();

				this.DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception ex) {
				ErrorForm.ShowErrorDialog(this, ex);
			}
		}

		#endregion

		#region Private methods

		//private void ConfigureSelectedPlugin() {
		//    if(_pluginlist.SelectedItems.Count == 0)
		//        return;

		//    GLItem item = (GLItem)_pluginlist.SelectedItems[0];
		//    IDeployerPlugin plugin = (IDeployerPlugin)item.Tag;
		//    plugin.ShowConfigureSettingsDialog(this);
		//}

		/// <summary>
		/// Retrieves the values from the dialog and stores them in the project settings.
		/// </summary>
		private void StoreSettings() {
			// Make sure settings are saved for all plugins
			PluginManager.SavePluginSettings(_project);

			// Save timestamp plugin
			// TODO: Fix timestamp setting
			//_project.TimestampPluginIdentifier = (string)_timestampplugin.SelectedItem;
/*
			// Save include files
			_project.ActiveFilter.IncludeFiles.Clear();
			DataTable dt = (DataTable)_includefiles.DataSource;
			foreach(DataRow row in dt.Rows) {
				Filter filter = new Filter();
				filter.Expression = (string)row[(int)IncludeFilesListColumns.Expression];
				filter.ExpressionType = (FilterExpressionType)row[(int)IncludeFilesListColumns.ExpressionType];
				filter.DeployPluginIdentifier = (string)row[(int)IncludeFilesListColumns.DeployWith];
				string remotePath = Convert.ToString(row[(int)IncludeFilesListColumns.RemotePath]);
				if(remotePath.Length != 0)
					filter.RemotePath = remotePath;
				else
					filter.RemotePath = null;
				_project.ActiveFilter.IncludeFiles.Add(filter);
			}

			// Save exclude directories
			_project.ActiveFilter.ExcludeDirectories.Clear();
			dt = (DataTable)_excludeDirectories.DataSource;
			foreach(DataRow row in dt.Rows) {
				Filter filter = new Filter();
				filter.Expression = (string)row[(int)ExcludeDirectoriesListColumns.Expression];
				filter.ExpressionType = (FilterExpressionType)row[(int)ExcludeDirectoriesListColumns.ExpressionType];
				_project.ActiveFilter.ExcludeDirectories.Add(filter);
			}

			// Save exclude files
			_project.ActiveFilter.ExcludeFiles.Clear();
			dt = (DataTable)_excludeFiles.DataSource;
			foreach(DataRow row in dt.Rows) {
				Filter filter = new Filter();
				filter.Expression = (string)row[(int)ExcludeFilesListColumns.Expression];
				filter.ExpressionType = (FilterExpressionType)row[(int)ExcludeFilesListColumns.ExpressionType];
				_project.ActiveFilter.ExcludeFiles.Add(filter);
			}
*/
			// Save database
			if (_project.ActiveDeployConfig.DatabaseSettings.Databases.Count == 0)
				_project.ActiveDeployConfig.DatabaseSettings.Databases.Add(new DatabasePair(new DatabaseDescriptor(), new DatabaseDescriptor()));
			DatabasePair db = _project.ActiveDeployConfig.DatabaseSettings.Databases[0];
			db.Source.Name = _sourcedbname.Text;
			db.Source.Url = _sourcedburl.Text;
			db.Destination.Name = _destinationdbname.Text;
			db.Destination.Url = _destinationdburl.Text;

			// Save exclude procedures
			_project.ActiveDeployConfig.DatabaseSettings.ExcludeProcedures.Clear();
			DataTable dt = (DataTable)_excludeProcedures.DataSource;
			foreach(DataRow row in dt.Rows) {
				Filter filter = new Filter();
				filter.Expression = (string)row[(int)ExcludeProceduresListColumns.Expression];
				filter.ExpressionType = (FilterExpressionType)row[(int)ExcludeProceduresListColumns.ExpressionType];
				_project.ActiveDeployConfig.DatabaseSettings.ExcludeProcedures.Add(filter);
			}

			// Save project settings
			_project.LocalPath = _localpath.Text;
			_project.ActiveDeployConfig.UseProjectFilter = _useProjectFilter.Checked;
		}

		private void UpdatePluginList() {
			// Setup plugin list
			_pluginlist.Items.Clear();
			//_timestampplugin.Items.Clear();
			foreach (PluginDescriptor descriptor in PluginManager.GetPluginDescriptors()) {
				//IDeployerPlugin plugin = PluginManager.Plugins.Get(pluginsettings.Identifier);
				GLItem item = _pluginlist.Items.Add(descriptor.PluginName);
				//item.Tag = plugin;
				item.SubItems[(int)PluginListColumns.Identifier].Text = descriptor.Identifier;
				item.SubItems[(int)PluginListColumns.Version].Text = descriptor.PluginVersion.ToString();
//				item.SubItems[(int)PluginListColumns.Filename].Text = plugin.PluginIdentifier;
//				_timestampplugin.Items.Add(pluginsettings.Identifier);
			}

			// Update selected timestamp plugin
			//if (_project.TimestampPluginIdentifier != null && _project.TimestampPluginIdentifier.Length > 0) {
			//    int selindex = _timestampplugin.FindStringExact(_project.TimestampPluginIdentifier);
			//    if (selindex >= 0)
			//        _timestampplugin.SelectedIndex = selindex;
			//}
			
			UpdateUI();
		}

		private void UpdateUI() {
			//_addPlugin.Enabled = PluginManager.Plugins.Count > _pluginlist.Items.Count;
			//if(_pluginlist.SelectedItems.Count == 0) {
			//    _configurePlugin.Enabled = false;
			//    _removePlugin.Enabled = false;
			//}
			//else {
			//    _configurePlugin.Enabled = true;
			//    _removePlugin.Enabled = true;
			//}
		}
		#endregion

	}

}
