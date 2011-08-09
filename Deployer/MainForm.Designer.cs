using System.ComponentModel;
using System.Windows.Forms;
using GlacialComponents.Controls;

namespace Deployer {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TabControl _bottomtabcontrol;
			GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn4 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn5 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn6 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn7 = new GlacialComponents.Controls.GLColumn();
			System.Windows.Forms.ToolStripMenuItem _menuScanAll;
			System.Windows.Forms.ToolStripMenuItem _menuScanOneHour;
			System.Windows.Forms.ToolStripMenuItem _menuScanFourHours;
			System.Windows.Forms.ToolStripMenuItem _menuScanYesterday;
			System.Windows.Forms.ToolStripMenuItem _menuScanTwoDays;
			System.Windows.Forms.ToolStripMenuItem _menuScanLastWeek;
			System.Windows.Forms.ToolStripMenuItem _menuScanLastMonth;
			System.Windows.Forms.ToolStripMenuItem _menuScanSince;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			GlacialComponents.Controls.GLColumn glColumn8 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn9 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn10 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn11 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn12 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn13 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn14 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn15 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn16 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn17 = new GlacialComponents.Controls.GLColumn();
			this._queuetab = new System.Windows.Forms.TabPage();
			this._fileQueueList = new GlacialComponents.Controls.GlacialList();
			this._fileQueueMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this._menuQueueRemove = new System.Windows.Forms.ToolStripMenuItem();
			this._menuQueueRemoveOther = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this._menuQueueSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this._menuQueueUnselectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this._menuQueueDeploy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this._menuQueueClear = new System.Windows.Forms.ToolStripMenuItem();
			this._toolstripBottom = new System.Windows.Forms.ToolStrip();
			this._queueAutoRemove = new System.Windows.Forms.ToolStripButton();
			this._logtab = new System.Windows.Forms.TabPage();
			this._logtext = new System.Windows.Forms.TextBox();
			this._tabcontrol = new System.Windows.Forms.TabControl();
			this._filetab = new System.Windows.Forms.TabPage();
			this._splitHorizontal = new System.Windows.Forms.SplitContainer();
			this._splitVertical = new System.Windows.Forms.SplitContainer();
			this._folderTree = new System.Windows.Forms.TreeView();
			this._folderTreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this._menuQueueFolder = new System.Windows.Forms.ToolStripMenuItem();
			this._folderImages = new System.Windows.Forms.ImageList(this.components);
			this._filelist = new GlacialComponents.Controls.GlacialList();
			this._fileListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this._menuQueueFiles = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this._menuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this._menuUnselectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this._menuEditFiles = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
			this._menuInclude = new System.Windows.Forms.ToolStripMenuItem();
			this._menuIncludeSingleFile = new System.Windows.Forms.ToolStripMenuItem();
			this._menuIncludeAllInFolder = new System.Windows.Forms.ToolStripMenuItem();
			this._menuIncludeAllInSubfolders = new System.Windows.Forms.ToolStripMenuItem();
			this._menuIncludeAll = new System.Windows.Forms.ToolStripMenuItem();
			this._menuExclude = new System.Windows.Forms.ToolStripMenuItem();
			this._databasetab = new System.Windows.Forms.TabPage();
			this._databaselist = new GlacialComponents.Controls.GlacialList();
			this._databaselistmenu = new System.Windows.Forms.ContextMenu();
			this._menuViewDetails = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this._menuIncludeDbObject = new System.Windows.Forms.MenuItem();
			this._menuExcludeDbObject = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this._menuIncludeAllDbObjects = new System.Windows.Forms.MenuItem();
			this._menuExcludeAllDbObjects = new System.Windows.Forms.MenuItem();
			this._toolstripTop = new System.Windows.Forms.ToolStrip();
			this._btnOpen = new System.Windows.Forms.ToolStripButton();
			this._btnSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this._btnSettings = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this._btnRefresh = new System.Windows.Forms.ToolStripButton();
			this._btnQueueAll = new System.Windows.Forms.ToolStripButton();
			this._btnQueueModified = new System.Windows.Forms.ToolStripDropDownButton();
			this._scanMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this._menuQueueLastDeployment = new System.Windows.Forms.ToolStripMenuItem();
			this._btnQueueSinceLastDeploy = new System.Windows.Forms.ToolStripButton();
			this._btnDeployQueue = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this._btnVerifyDatabase = new System.Windows.Forms.ToolStripButton();
			this._btnScanDatabase = new System.Windows.Forms.ToolStripButton();
			this._btnDeployDatabase = new System.Windows.Forms.ToolStripButton();
			this._menuQueue = new System.Windows.Forms.ToolStripMenuItem();
			this._mainmenu = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._menuNewProject = new System.Windows.Forms.ToolStripMenuItem();
			this._menuOpenProject = new System.Windows.Forms.ToolStripMenuItem();
			this._menuSaveProject = new System.Windows.Forms.ToolStripMenuItem();
			this._menuSaveProjectAs = new System.Windows.Forms.ToolStripMenuItem();
			this._menuCloseProject = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this._menuRecentProjects = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this._menuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._menuRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this._menuDeployQueue = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this._menuVerifyDatabase = new System.Windows.Forms.ToolStripMenuItem();
			this._menuScanDatabase = new System.Windows.Forms.ToolStripMenuItem();
			this._menuDeployDatabase = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this._menuConfiguration = new System.Windows.Forms.ToolStripMenuItem();
			this._menuSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._menuAbout = new System.Windows.Forms.ToolStripMenuItem();
			this._statusStrip = new System.Windows.Forms.StatusStrip();
			this._statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this._statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this._statusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this._activeConfiguration = new System.Windows.Forms.ToolStripDropDownButton();
			_bottomtabcontrol = new System.Windows.Forms.TabControl();
			_menuScanAll = new System.Windows.Forms.ToolStripMenuItem();
			_menuScanOneHour = new System.Windows.Forms.ToolStripMenuItem();
			_menuScanFourHours = new System.Windows.Forms.ToolStripMenuItem();
			_menuScanYesterday = new System.Windows.Forms.ToolStripMenuItem();
			_menuScanTwoDays = new System.Windows.Forms.ToolStripMenuItem();
			_menuScanLastWeek = new System.Windows.Forms.ToolStripMenuItem();
			_menuScanLastMonth = new System.Windows.Forms.ToolStripMenuItem();
			_menuScanSince = new System.Windows.Forms.ToolStripMenuItem();
			_bottomtabcontrol.SuspendLayout();
			this._queuetab.SuspendLayout();
			this._fileQueueMenu.SuspendLayout();
			this._toolstripBottom.SuspendLayout();
			this._logtab.SuspendLayout();
			this._tabcontrol.SuspendLayout();
			this._filetab.SuspendLayout();
			this._splitHorizontal.Panel1.SuspendLayout();
			this._splitHorizontal.Panel2.SuspendLayout();
			this._splitHorizontal.SuspendLayout();
			this._splitVertical.Panel1.SuspendLayout();
			this._splitVertical.Panel2.SuspendLayout();
			this._splitVertical.SuspendLayout();
			this._folderTreeMenu.SuspendLayout();
			this._fileListMenu.SuspendLayout();
			this._databasetab.SuspendLayout();
			this._toolstripTop.SuspendLayout();
			this._scanMenu.SuspendLayout();
			this._mainmenu.SuspendLayout();
			this._statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// _bottomtabcontrol
			// 
			_bottomtabcontrol.Controls.Add(this._queuetab);
			_bottomtabcontrol.Controls.Add(this._logtab);
			_bottomtabcontrol.Dock = System.Windows.Forms.DockStyle.Fill;
			_bottomtabcontrol.Location = new System.Drawing.Point(0, 0);
			_bottomtabcontrol.Name = "_bottomtabcontrol";
			_bottomtabcontrol.SelectedIndex = 0;
			_bottomtabcontrol.Size = new System.Drawing.Size(988, 184);
			_bottomtabcontrol.TabIndex = 12;
			// 
			// _queuetab
			// 
			this._queuetab.Controls.Add(this._fileQueueList);
			this._queuetab.Controls.Add(this._toolstripBottom);
			this._queuetab.Location = new System.Drawing.Point(4, 22);
			this._queuetab.Name = "_queuetab";
			this._queuetab.Padding = new System.Windows.Forms.Padding(3);
			this._queuetab.Size = new System.Drawing.Size(980, 158);
			this._queuetab.TabIndex = 0;
			this._queuetab.Text = "Queue";
			// 
			// _fileQueueList
			// 
			this._fileQueueList.BackColor = System.Drawing.SystemColors.Window;
			glColumn1.Name = "Filename";
			glColumn1.Text = "Name";
			glColumn1.Width = 450;
			glColumn2.Name = "LastModified";
			glColumn2.Text = "Last modified";
			glColumn2.Width = 118;
			glColumn3.Name = "Size";
			glColumn3.Text = "Size";
			glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
			glColumn3.Width = 50;
			glColumn4.Name = "Destination";
			glColumn4.Text = "Destination";
			glColumn4.Width = 130;
			glColumn5.Name = "RemotePath";
			glColumn5.Text = "Remote path";
			glColumn5.Width = 170;
			glColumn6.Name = "RemoteName";
			glColumn6.Text = "Remote name";
			glColumn7.Name = "Progress";
			glColumn7.Text = "Progress";
			this._fileQueueList.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3,
            glColumn4,
            glColumn5,
            glColumn6,
            glColumn7});
			this._fileQueueList.ContextMenuStrip = this._fileQueueMenu;
			this._fileQueueList.Dock = System.Windows.Forms.DockStyle.Fill;
			this._fileQueueList.HotItemTracking = true;
			this._fileQueueList.HotTrackingColor = System.Drawing.SystemColors.HotTrack;
			this._fileQueueList.ImageList = null;
			this._fileQueueList.Location = new System.Drawing.Point(3, 28);
			this._fileQueueList.MultiSelect = true;
			this._fileQueueList.Name = "_fileQueueList";
			this._fileQueueList.SelectedTextColor = System.Drawing.SystemColors.HighlightText;
			this._fileQueueList.SelectionColor = System.Drawing.SystemColors.Highlight;
			this._fileQueueList.Size = new System.Drawing.Size(974, 127);
			this._fileQueueList.TabIndex = 11;
			this._fileQueueList.UnfocusedSelectionColor = System.Drawing.SystemColors.Highlight;
			this._fileQueueList.DoubleClick += new System.EventHandler(this._fileQueueList_DoubleClick);
			// 
			// _fileQueueMenu
			// 
			this._fileQueueMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuQueueRemove,
            this._menuQueueRemoveOther,
            this.toolStripSeparator6,
            this._menuQueueSelectAll,
            this._menuQueueUnselectAll,
            this.toolStripSeparator7,
            this._menuQueueDeploy,
            this.toolStripSeparator5,
            this._menuQueueClear});
			this._fileQueueMenu.Name = "_fileQueueMenu";
			this._fileQueueMenu.Size = new System.Drawing.Size(257, 154);
			this._fileQueueMenu.Opening += new System.ComponentModel.CancelEventHandler(this._fileQueueMenu_Opening);
			// 
			// _menuQueueRemove
			// 
			this._menuQueueRemove.Name = "_menuQueueRemove";
			this._menuQueueRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this._menuQueueRemove.Size = new System.Drawing.Size(256, 22);
			this._menuQueueRemove.Text = "Remove file(s) from Queue";
			this._menuQueueRemove.Click += new System.EventHandler(this._menuQueueRemove_Click);
			// 
			// _menuQueueRemoveOther
			// 
			this._menuQueueRemoveOther.Name = "_menuQueueRemoveOther";
			this._menuQueueRemoveOther.Size = new System.Drawing.Size(256, 22);
			this._menuQueueRemoveOther.Text = "Remove non-selected from Queue";
			this._menuQueueRemoveOther.Click += new System.EventHandler(this._menuQueueRemoveOther_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(253, 6);
			// 
			// _menuQueueSelectAll
			// 
			this._menuQueueSelectAll.Name = "_menuQueueSelectAll";
			this._menuQueueSelectAll.Size = new System.Drawing.Size(256, 22);
			this._menuQueueSelectAll.Text = "Select All";
			this._menuQueueSelectAll.Click += new System.EventHandler(this._menuQueueSelectAll_Click);
			// 
			// _menuQueueUnselectAll
			// 
			this._menuQueueUnselectAll.Name = "_menuQueueUnselectAll";
			this._menuQueueUnselectAll.Size = new System.Drawing.Size(256, 22);
			this._menuQueueUnselectAll.Text = "Unselect All";
			this._menuQueueUnselectAll.Click += new System.EventHandler(this._menuQueueUnselectAll_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(253, 6);
			// 
			// _menuQueueDeploy
			// 
			this._menuQueueDeploy.Name = "_menuQueueDeploy";
			this._menuQueueDeploy.Size = new System.Drawing.Size(256, 22);
			this._menuQueueDeploy.Text = "Deploy Queue";
			this._menuQueueDeploy.Click += new System.EventHandler(this._menuQueueDeploy_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(253, 6);
			// 
			// _menuQueueClear
			// 
			this._menuQueueClear.Name = "_menuQueueClear";
			this._menuQueueClear.Size = new System.Drawing.Size(256, 22);
			this._menuQueueClear.Text = "&Clear Queue";
			this._menuQueueClear.Click += new System.EventHandler(this._menuQueueClear_Click);
			// 
			// _toolstripBottom
			// 
			this._toolstripBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._queueAutoRemove});
			this._toolstripBottom.Location = new System.Drawing.Point(3, 3);
			this._toolstripBottom.Name = "_toolstripBottom";
			this._toolstripBottom.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this._toolstripBottom.Size = new System.Drawing.Size(974, 25);
			this._toolstripBottom.TabIndex = 12;
			this._toolstripBottom.Text = "toolStrip1";
			// 
			// _queueAutoRemove
			// 
			this._queueAutoRemove.Checked = true;
			this._queueAutoRemove.CheckOnClick = true;
			this._queueAutoRemove.CheckState = System.Windows.Forms.CheckState.Checked;
			this._queueAutoRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._queueAutoRemove.Image = global::Deployer.Properties.Resources.index_delete;
			this._queueAutoRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._queueAutoRemove.Name = "_queueAutoRemove";
			this._queueAutoRemove.Size = new System.Drawing.Size(23, 22);
			this._queueAutoRemove.Text = "toolStripButton1";
			this._queueAutoRemove.ToolTipText = "Automatically remove successfully transferred files from the queue.";
			// 
			// _logtab
			// 
			this._logtab.Controls.Add(this._logtext);
			this._logtab.Location = new System.Drawing.Point(4, 22);
			this._logtab.Name = "_logtab";
			this._logtab.Padding = new System.Windows.Forms.Padding(3);
			this._logtab.Size = new System.Drawing.Size(980, 158);
			this._logtab.TabIndex = 1;
			this._logtab.Text = "Log";
			// 
			// _logtext
			// 
			this._logtext.BackColor = System.Drawing.Color.White;
			this._logtext.Dock = System.Windows.Forms.DockStyle.Fill;
			this._logtext.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._logtext.Location = new System.Drawing.Point(3, 3);
			this._logtext.Multiline = true;
			this._logtext.Name = "_logtext";
			this._logtext.ReadOnly = true;
			this._logtext.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._logtext.Size = new System.Drawing.Size(974, 152);
			this._logtext.TabIndex = 1;
			// 
			// _menuScanAll
			// 
			_menuScanAll.Name = "_menuScanAll";
			_menuScanAll.Size = new System.Drawing.Size(190, 22);
			_menuScanAll.Text = "All";
			_menuScanAll.ToolTipText = "Queue all files";
			_menuScanAll.Click += new System.EventHandler(this._menuScanAll_Click);
			// 
			// _menuScanOneHour
			// 
			_menuScanOneHour.Name = "_menuScanOneHour";
			_menuScanOneHour.Size = new System.Drawing.Size(190, 22);
			_menuScanOneHour.Tag = "hour";
			_menuScanOneHour.Text = "Less than an hour";
			_menuScanOneHour.Click += new System.EventHandler(this._menuScanModified_Click);
			// 
			// _menuScanFourHours
			// 
			_menuScanFourHours.Name = "_menuScanFourHours";
			_menuScanFourHours.Size = new System.Drawing.Size(190, 22);
			_menuScanFourHours.Tag = "4hours";
			_menuScanFourHours.Text = "Four hours ago";
			_menuScanFourHours.Click += new System.EventHandler(this._menuScanModified_Click);
			// 
			// _menuScanYesterday
			// 
			_menuScanYesterday.Name = "_menuScanYesterday";
			_menuScanYesterday.Size = new System.Drawing.Size(190, 22);
			_menuScanYesterday.Tag = "yesterday";
			_menuScanYesterday.Text = "Since yesterday";
			_menuScanYesterday.Click += new System.EventHandler(this._menuScanModified_Click);
			// 
			// _menuScanTwoDays
			// 
			_menuScanTwoDays.Name = "_menuScanTwoDays";
			_menuScanTwoDays.Size = new System.Drawing.Size(190, 22);
			_menuScanTwoDays.Tag = "twodays";
			_menuScanTwoDays.Text = "Two days ago";
			_menuScanTwoDays.Click += new System.EventHandler(this._menuScanModified_Click);
			// 
			// _menuScanLastWeek
			// 
			_menuScanLastWeek.Name = "_menuScanLastWeek";
			_menuScanLastWeek.Size = new System.Drawing.Size(190, 22);
			_menuScanLastWeek.Tag = "week";
			_menuScanLastWeek.Text = "Last week";
			_menuScanLastWeek.Click += new System.EventHandler(this._menuScanModified_Click);
			// 
			// _menuScanLastMonth
			// 
			_menuScanLastMonth.Name = "_menuScanLastMonth";
			_menuScanLastMonth.Size = new System.Drawing.Size(190, 22);
			_menuScanLastMonth.Tag = "month";
			_menuScanLastMonth.Text = "Last month";
			_menuScanLastMonth.Click += new System.EventHandler(this._menuScanModified_Click);
			// 
			// _menuScanSince
			// 
			_menuScanSince.Name = "_menuScanSince";
			_menuScanSince.Size = new System.Drawing.Size(190, 22);
			_menuScanSince.Tag = "date";
			_menuScanSince.Text = "Since date...";
			_menuScanSince.Click += new System.EventHandler(this._menuScanModified_Click);
			// 
			// _tabcontrol
			// 
			this._tabcontrol.Controls.Add(this._filetab);
			this._tabcontrol.Controls.Add(this._databasetab);
			this._tabcontrol.Dock = System.Windows.Forms.DockStyle.Fill;
			this._tabcontrol.Location = new System.Drawing.Point(0, 63);
			this._tabcontrol.Name = "_tabcontrol";
			this._tabcontrol.SelectedIndex = 0;
			this._tabcontrol.Size = new System.Drawing.Size(996, 561);
			this._tabcontrol.TabIndex = 11;
			// 
			// _filetab
			// 
			this._filetab.Controls.Add(this._splitHorizontal);
			this._filetab.Location = new System.Drawing.Point(4, 22);
			this._filetab.Name = "_filetab";
			this._filetab.Size = new System.Drawing.Size(988, 535);
			this._filetab.TabIndex = 0;
			this._filetab.Text = "Files";
			// 
			// _splitHorizontal
			// 
			this._splitHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
			this._splitHorizontal.Location = new System.Drawing.Point(0, 0);
			this._splitHorizontal.Name = "_splitHorizontal";
			this._splitHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// _splitHorizontal.Panel1
			// 
			this._splitHorizontal.Panel1.Controls.Add(this._splitVertical);
			// 
			// _splitHorizontal.Panel2
			// 
			this._splitHorizontal.Panel2.Controls.Add(_bottomtabcontrol);
			this._splitHorizontal.Size = new System.Drawing.Size(988, 535);
			this._splitHorizontal.SplitterDistance = 347;
			this._splitHorizontal.TabIndex = 12;
			// 
			// _splitVertical
			// 
			this._splitVertical.Dock = System.Windows.Forms.DockStyle.Fill;
			this._splitVertical.Location = new System.Drawing.Point(0, 0);
			this._splitVertical.Name = "_splitVertical";
			// 
			// _splitVertical.Panel1
			// 
			this._splitVertical.Panel1.Controls.Add(this._folderTree);
			// 
			// _splitVertical.Panel2
			// 
			this._splitVertical.Panel2.Controls.Add(this._filelist);
			this._splitVertical.Size = new System.Drawing.Size(988, 347);
			this._splitVertical.SplitterDistance = 272;
			this._splitVertical.TabIndex = 11;
			// 
			// _folderTree
			// 
			this._folderTree.ContextMenuStrip = this._folderTreeMenu;
			this._folderTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this._folderTree.HideSelection = false;
			this._folderTree.HotTracking = true;
			this._folderTree.ImageIndex = 0;
			this._folderTree.ImageList = this._folderImages;
			this._folderTree.Location = new System.Drawing.Point(0, 0);
			this._folderTree.Name = "_folderTree";
			this._folderTree.SelectedImageIndex = 0;
			this._folderTree.Size = new System.Drawing.Size(272, 347);
			this._folderTree.TabIndex = 0;
			this._folderTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._folderTree_AfterSelect);
			this._folderTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this._folderTree_MouseDown);
			// 
			// _folderTreeMenu
			// 
			this._folderTreeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuQueueFolder});
			this._folderTreeMenu.Name = "_folderTreeMenu";
			this._folderTreeMenu.Size = new System.Drawing.Size(183, 26);
			this._folderTreeMenu.Opening += new System.ComponentModel.CancelEventHandler(this._folderTreeMenu_Opening);
			// 
			// _menuQueueFolder
			// 
			this._menuQueueFolder.Name = "_menuQueueFolder";
			this._menuQueueFolder.Size = new System.Drawing.Size(182, 22);
			this._menuQueueFolder.Text = "Add folder to Queue";
			this._menuQueueFolder.Click += new System.EventHandler(this._menuQueueFolder_Click);
			// 
			// _folderImages
			// 
			this._folderImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_folderImages.ImageStream")));
			this._folderImages.TransparentColor = System.Drawing.Color.Transparent;
			this._folderImages.Images.SetKeyName(0, "folder_closed.png");
			this._folderImages.Images.SetKeyName(1, "folder.png");
			this._folderImages.Images.SetKeyName(2, "server_from_client.png");
			// 
			// _filelist
			// 
			this._filelist.BackColor = System.Drawing.SystemColors.Window;
			glColumn8.Name = "Filename";
			glColumn8.Text = "Name";
			glColumn8.Width = 300;
			glColumn9.Name = "LastModified";
			glColumn9.Text = "Last modified";
			glColumn9.Width = 118;
			glColumn10.Name = "Size";
			glColumn10.Text = "Size";
			glColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
			glColumn10.Width = 50;
			glColumn11.Name = "Destination";
			glColumn11.Text = "Destination";
			glColumn11.Width = 110;
			glColumn12.Name = "RemotePath";
			glColumn12.Text = "Remote path";
			glColumn12.Width = 150;
			glColumn13.Name = "RemoteFileName";
			glColumn13.Text = "Remote filename";
			this._filelist.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn8,
            glColumn9,
            glColumn10,
            glColumn11,
            glColumn12,
            glColumn13});
			this._filelist.ContextMenuStrip = this._fileListMenu;
			this._filelist.Dock = System.Windows.Forms.DockStyle.Fill;
			this._filelist.HotItemTracking = true;
			this._filelist.HotTrackingColor = System.Drawing.SystemColors.HotTrack;
			this._filelist.ImageList = null;
			this._filelist.Location = new System.Drawing.Point(0, 0);
			this._filelist.MultiSelect = true;
			this._filelist.Name = "_filelist";
			this._filelist.SelectedTextColor = System.Drawing.SystemColors.HighlightText;
			this._filelist.SelectionColor = System.Drawing.SystemColors.Highlight;
			this._filelist.Size = new System.Drawing.Size(712, 347);
			this._filelist.TabIndex = 10;
			this._filelist.UnfocusedSelectionColor = System.Drawing.SystemColors.Highlight;
			this._filelist.DoubleClick += new System.EventHandler(this._filelist_DoubleClick);
			// 
			// _fileListMenu
			// 
			this._fileListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuQueueFiles,
            this.toolStripMenuItem5,
            this._menuSelectAll,
            this._menuUnselectAll,
            this.toolStripMenuItem6,
            this._menuEditFiles,
            this.toolStripMenuItem7,
            this._menuInclude,
            this._menuExclude});
			this._fileListMenu.Name = "_fileListMenu";
			this._fileListMenu.Size = new System.Drawing.Size(181, 154);
			this._fileListMenu.Opening += new System.ComponentModel.CancelEventHandler(this._fileListMenu_Opening);
			// 
			// _menuQueueFiles
			// 
			this._menuQueueFiles.Name = "_menuQueueFiles";
			this._menuQueueFiles.Size = new System.Drawing.Size(180, 22);
			this._menuQueueFiles.Text = "Add file(s) to Queue";
			this._menuQueueFiles.Click += new System.EventHandler(this._menuQueueFiles_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(177, 6);
			// 
			// _menuSelectAll
			// 
			this._menuSelectAll.Name = "_menuSelectAll";
			this._menuSelectAll.Size = new System.Drawing.Size(180, 22);
			this._menuSelectAll.Text = "Select All";
			this._menuSelectAll.Click += new System.EventHandler(this._menuSelectAll_Click);
			// 
			// _menuUnselectAll
			// 
			this._menuUnselectAll.Name = "_menuUnselectAll";
			this._menuUnselectAll.Size = new System.Drawing.Size(180, 22);
			this._menuUnselectAll.Text = "Unselect All";
			this._menuUnselectAll.Click += new System.EventHandler(this._menuUnselectAll_Click);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(177, 6);
			// 
			// _menuEditFiles
			// 
			this._menuEditFiles.Name = "_menuEditFiles";
			this._menuEditFiles.Size = new System.Drawing.Size(180, 22);
			this._menuEditFiles.Text = "Edit Files...";
			this._menuEditFiles.Click += new System.EventHandler(this._menuEditFiles_Click);
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(177, 6);
			// 
			// _menuInclude
			// 
			this._menuInclude.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuIncludeSingleFile,
            this._menuIncludeAllInFolder,
            this._menuIncludeAllInSubfolders,
            this._menuIncludeAll});
			this._menuInclude.Name = "_menuInclude";
			this._menuInclude.Size = new System.Drawing.Size(180, 22);
			this._menuInclude.Text = "Include";
			// 
			// _menuIncludeSingleFile
			// 
			this._menuIncludeSingleFile.Name = "_menuIncludeSingleFile";
			this._menuIncludeSingleFile.Size = new System.Drawing.Size(245, 22);
			this._menuIncludeSingleFile.Text = "Only \"myfile.txt\"";
			// 
			// _menuIncludeAllInFolder
			// 
			this._menuIncludeAllInFolder.Name = "_menuIncludeAllInFolder";
			this._menuIncludeAllInFolder.Size = new System.Drawing.Size(245, 22);
			this._menuIncludeAllInFolder.Text = "*.txt in this folder";
			// 
			// _menuIncludeAllInSubfolders
			// 
			this._menuIncludeAllInSubfolders.Name = "_menuIncludeAllInSubfolders";
			this._menuIncludeAllInSubfolders.Size = new System.Drawing.Size(245, 22);
			this._menuIncludeAllInSubfolders.Text = "*.txt in this folder and subfolders";
			// 
			// _menuIncludeAll
			// 
			this._menuIncludeAll.Name = "_menuIncludeAll";
			this._menuIncludeAll.Size = new System.Drawing.Size(245, 22);
			this._menuIncludeAll.Text = "All *.txt files";
			// 
			// _menuExclude
			// 
			this._menuExclude.Enabled = false;
			this._menuExclude.Name = "_menuExclude";
			this._menuExclude.Size = new System.Drawing.Size(180, 22);
			this._menuExclude.Text = "Exclude";
			// 
			// _databasetab
			// 
			this._databasetab.Controls.Add(this._databaselist);
			this._databasetab.Location = new System.Drawing.Point(4, 22);
			this._databasetab.Name = "_databasetab";
			this._databasetab.Size = new System.Drawing.Size(988, 535);
			this._databasetab.TabIndex = 1;
			this._databasetab.Text = "Database";
			// 
			// _databaselist
			// 
			this._databaselist.BackColor = System.Drawing.SystemColors.Window;
			glColumn14.CheckBoxes = true;
			glColumn14.Name = "Name";
			glColumn14.Text = "Object Name";
			glColumn14.Width = 200;
			glColumn15.Name = "Type";
			glColumn15.Text = "Type";
			glColumn16.Name = "Progress";
			glColumn16.Text = "Progress";
			glColumn16.Width = 60;
			glColumn17.Name = "Summary";
			glColumn17.Text = "Summary";
			glColumn17.Width = 400;
			this._databaselist.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn14,
            glColumn15,
            glColumn16,
            glColumn17});
			this._databaselist.ContextMenu = this._databaselistmenu;
			this._databaselist.Dock = System.Windows.Forms.DockStyle.Fill;
			this._databaselist.HotTrackingColor = System.Drawing.SystemColors.HotTrack;
			this._databaselist.ImageList = null;
			this._databaselist.Location = new System.Drawing.Point(0, 0);
			this._databaselist.MultiSelect = true;
			this._databaselist.Name = "_databaselist";
			this._databaselist.SelectedTextColor = System.Drawing.SystemColors.HighlightText;
			this._databaselist.SelectionColor = System.Drawing.SystemColors.Highlight;
			this._databaselist.Size = new System.Drawing.Size(988, 535);
			this._databaselist.TabIndex = 0;
			this._databaselist.UnfocusedSelectionColor = System.Drawing.SystemColors.Highlight;
			this._databaselist.DoubleClick += new System.EventHandler(this._databaselist_DoubleClick);
			// 
			// _databaselistmenu
			// 
			this._databaselistmenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this._menuViewDetails,
            this.menuItem5,
            this._menuIncludeDbObject,
            this._menuExcludeDbObject,
            this.menuItem11,
            this._menuIncludeAllDbObjects,
            this._menuExcludeAllDbObjects});
			this._databaselistmenu.Popup += new System.EventHandler(this._databaselistmenu_Popup);
			// 
			// _menuViewDetails
			// 
			this._menuViewDetails.DefaultItem = true;
			this._menuViewDetails.Index = 0;
			this._menuViewDetails.Text = "&View details";
			this._menuViewDetails.Click += new System.EventHandler(this._menuViewDetails_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.Text = "-";
			// 
			// _menuIncludeDbObject
			// 
			this._menuIncludeDbObject.Index = 2;
			this._menuIncludeDbObject.Text = "&Select Object(s)";
			this._menuIncludeDbObject.Click += new System.EventHandler(this._menuIncludeDbObject_Click);
			// 
			// _menuExcludeDbObject
			// 
			this._menuExcludeDbObject.Index = 3;
			this._menuExcludeDbObject.Text = "&Unselect Object(s)";
			this._menuExcludeDbObject.Click += new System.EventHandler(this._menuExcludeDbObject_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 4;
			this.menuItem11.Text = "-";
			// 
			// _menuIncludeAllDbObjects
			// 
			this._menuIncludeAllDbObjects.Index = 5;
			this._menuIncludeAllDbObjects.Text = "Select &All";
			this._menuIncludeAllDbObjects.Click += new System.EventHandler(this._menuIncludeAllDbObjects_Click);
			// 
			// _menuExcludeAllDbObjects
			// 
			this._menuExcludeAllDbObjects.Index = 6;
			this._menuExcludeAllDbObjects.Text = "U&nselect All";
			this._menuExcludeAllDbObjects.Click += new System.EventHandler(this._menuExcludeAllDbObjects_Click);
			// 
			// _toolstripTop
			// 
			this._toolstripTop.ImageScalingSize = new System.Drawing.Size(32, 32);
			this._toolstripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnOpen,
            this._btnSave,
            this.toolStripSeparator1,
            this._btnSettings,
            this.toolStripSeparator2,
            this._btnRefresh,
            this._btnQueueAll,
            this._btnQueueModified,
            this._btnQueueSinceLastDeploy,
            this._btnDeployQueue,
            this.toolStripSeparator3,
            this._btnVerifyDatabase,
            this._btnScanDatabase,
            this._btnDeployDatabase});
			this._toolstripTop.Location = new System.Drawing.Point(0, 24);
			this._toolstripTop.Name = "_toolstripTop";
			this._toolstripTop.Size = new System.Drawing.Size(996, 39);
			this._toolstripTop.TabIndex = 12;
			this._toolstripTop.Text = "toolStrip2";
			// 
			// _btnOpen
			// 
			this._btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._btnOpen.Image = global::Deployer.Properties.Resources.folder_out;
			this._btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnOpen.Name = "_btnOpen";
			this._btnOpen.Size = new System.Drawing.Size(36, 36);
			this._btnOpen.Text = "Open project";
			this._btnOpen.Click += new System.EventHandler(this._menuOpenProject_Click);
			// 
			// _btnSave
			// 
			this._btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._btnSave.Image = global::Deployer.Properties.Resources.disk_blue;
			this._btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnSave.Name = "_btnSave";
			this._btnSave.Size = new System.Drawing.Size(36, 36);
			this._btnSave.Text = "Save project";
			this._btnSave.Click += new System.EventHandler(this._menuSaveProject_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
			// 
			// _btnSettings
			// 
			this._btnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._btnSettings.Image = global::Deployer.Properties.Resources.preferences;
			this._btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnSettings.Name = "_btnSettings";
			this._btnSettings.Size = new System.Drawing.Size(36, 36);
			this._btnSettings.Text = "Settings";
			this._btnSettings.ToolTipText = "Edit project settings";
			this._btnSettings.Click += new System.EventHandler(this._menuSettings_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
			// 
			// _btnRefresh
			// 
			this._btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._btnRefresh.Image = global::Deployer.Properties.Resources.refresh;
			this._btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnRefresh.Name = "_btnRefresh";
			this._btnRefresh.Size = new System.Drawing.Size(36, 36);
			this._btnRefresh.Text = "Refresh";
			this._btnRefresh.ToolTipText = "Refresh file structure";
			this._btnRefresh.Click += new System.EventHandler(this._menuRefresh_Click);
			// 
			// _btnQueueAll
			// 
			this._btnQueueAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._btnQueueAll.Image = global::Deployer.Properties.Resources.index;
			this._btnQueueAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnQueueAll.Name = "_btnQueueAll";
			this._btnQueueAll.Size = new System.Drawing.Size(36, 36);
			this._btnQueueAll.Text = "Queue all";
			this._btnQueueAll.ToolTipText = "Queue all files";
			this._btnQueueAll.Click += new System.EventHandler(this._menuScanAll_Click);
			// 
			// _btnQueueModified
			// 
			this._btnQueueModified.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._btnQueueModified.DropDown = this._scanMenu;
			this._btnQueueModified.Image = global::Deployer.Properties.Resources.index_view;
			this._btnQueueModified.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnQueueModified.Name = "_btnQueueModified";
			this._btnQueueModified.Size = new System.Drawing.Size(45, 36);
			this._btnQueueModified.Text = "Queue modified";
			this._btnQueueModified.ToolTipText = "Queue modified files";
			// 
			// _scanMenu
			// 
			this._scanMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            _menuScanAll,
            this.toolStripSeparator4,
            this._menuQueueLastDeployment,
            _menuScanOneHour,
            _menuScanFourHours,
            _menuScanYesterday,
            _menuScanTwoDays,
            _menuScanLastWeek,
            _menuScanLastMonth,
            _menuScanSince});
			this._scanMenu.Name = "_scanmenu2";
			this._scanMenu.OwnerItem = this._menuQueue;
			this._scanMenu.Size = new System.Drawing.Size(191, 208);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(187, 6);
			// 
			// _menuQueueLastDeployment
			// 
			this._menuQueueLastDeployment.Name = "_menuQueueLastDeployment";
			this._menuQueueLastDeployment.Size = new System.Drawing.Size(190, 22);
			this._menuQueueLastDeployment.Tag = "last";
			this._menuQueueLastDeployment.Text = "Since last deployment";
			this._menuQueueLastDeployment.ToolTipText = "Queue files modified since last deployment";
			this._menuQueueLastDeployment.Click += new System.EventHandler(this._menuScanModified_Click);
			// 
			// _btnQueueSinceLastDeploy
			// 
			this._btnQueueSinceLastDeploy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._btnQueueSinceLastDeploy.Image = global::Deployer.Properties.Resources.index_refresh;
			this._btnQueueSinceLastDeploy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnQueueSinceLastDeploy.Name = "_btnQueueSinceLastDeploy";
			this._btnQueueSinceLastDeploy.Size = new System.Drawing.Size(36, 36);
			this._btnQueueSinceLastDeploy.Text = "Queue modified";
			this._btnQueueSinceLastDeploy.ToolTipText = "Queue files modified since last deployment";
			this._btnQueueSinceLastDeploy.Click += new System.EventHandler(this._menuScanLastDeployment_Click);
			// 
			// _btnDeployQueue
			// 
			this._btnDeployQueue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._btnDeployQueue.Image = global::Deployer.Properties.Resources.index_up;
			this._btnDeployQueue.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnDeployQueue.Name = "_btnDeployQueue";
			this._btnDeployQueue.Size = new System.Drawing.Size(36, 36);
			this._btnDeployQueue.Text = "Deploy";
			this._btnDeployQueue.ToolTipText = "Deploy queue";
			this._btnDeployQueue.Click += new System.EventHandler(this._menuUpload_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
			// 
			// _btnVerifyDatabase
			// 
			this._btnVerifyDatabase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._btnVerifyDatabase.Image = global::Deployer.Properties.Resources.data_view;
			this._btnVerifyDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnVerifyDatabase.Name = "_btnVerifyDatabase";
			this._btnVerifyDatabase.Size = new System.Drawing.Size(36, 36);
			this._btnVerifyDatabase.Text = "Verify database";
			this._btnVerifyDatabase.Click += new System.EventHandler(this._menuVerifyDatabase_Click);
			// 
			// _btnScanDatabase
			// 
			this._btnScanDatabase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._btnScanDatabase.Image = global::Deployer.Properties.Resources.data_refresh;
			this._btnScanDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnScanDatabase.Name = "_btnScanDatabase";
			this._btnScanDatabase.Size = new System.Drawing.Size(36, 36);
			this._btnScanDatabase.Text = "Scan database";
			this._btnScanDatabase.ToolTipText = "Scan stored procedures in database";
			this._btnScanDatabase.Click += new System.EventHandler(this._menuScanDatabase_Click);
			// 
			// _btnDeployDatabase
			// 
			this._btnDeployDatabase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._btnDeployDatabase.Image = global::Deployer.Properties.Resources.data_up;
			this._btnDeployDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._btnDeployDatabase.Name = "_btnDeployDatabase";
			this._btnDeployDatabase.Size = new System.Drawing.Size(36, 36);
			this._btnDeployDatabase.Text = "Deploy database";
			this._btnDeployDatabase.ToolTipText = "Deploy stored procedures";
			this._btnDeployDatabase.Click += new System.EventHandler(this._menuDeployDatabase_Click);
			// 
			// _menuQueue
			// 
			this._menuQueue.DropDown = this._scanMenu;
			this._menuQueue.Name = "_menuQueue";
			this._menuQueue.Size = new System.Drawing.Size(179, 22);
			this._menuQueue.Text = "&Queue Files";
			// 
			// _mainmenu
			// 
			this._mainmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.projectToolStripMenuItem,
            this.helpToolStripMenuItem});
			this._mainmenu.Location = new System.Drawing.Point(0, 0);
			this._mainmenu.Name = "_mainmenu";
			this._mainmenu.Size = new System.Drawing.Size(996, 24);
			this._mainmenu.TabIndex = 14;
			this._mainmenu.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuNewProject,
            this._menuOpenProject,
            this._menuSaveProject,
            this._menuSaveProjectAs,
            this._menuCloseProject,
            this.toolStripMenuItem1,
            this._menuRecentProjects,
            this.toolStripMenuItem2,
            this._menuExit});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// _menuNewProject
			// 
			this._menuNewProject.Image = global::Deployer.Properties.Resources.document_new;
			this._menuNewProject.Name = "_menuNewProject";
			this._menuNewProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this._menuNewProject.Size = new System.Drawing.Size(195, 22);
			this._menuNewProject.Text = "&New Project...";
			this._menuNewProject.Click += new System.EventHandler(this._menuNewProject_Click);
			// 
			// _menuOpenProject
			// 
			this._menuOpenProject.Image = global::Deployer.Properties.Resources.folder_out;
			this._menuOpenProject.Name = "_menuOpenProject";
			this._menuOpenProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this._menuOpenProject.Size = new System.Drawing.Size(195, 22);
			this._menuOpenProject.Text = "&Open Project...";
			this._menuOpenProject.Click += new System.EventHandler(this._menuOpenProject_Click);
			// 
			// _menuSaveProject
			// 
			this._menuSaveProject.Image = global::Deployer.Properties.Resources.disk_blue;
			this._menuSaveProject.Name = "_menuSaveProject";
			this._menuSaveProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this._menuSaveProject.Size = new System.Drawing.Size(195, 22);
			this._menuSaveProject.Text = "&Save Project";
			this._menuSaveProject.Click += new System.EventHandler(this._menuSaveProject_Click);
			// 
			// _menuSaveProjectAs
			// 
			this._menuSaveProjectAs.Name = "_menuSaveProjectAs";
			this._menuSaveProjectAs.Size = new System.Drawing.Size(195, 22);
			this._menuSaveProjectAs.Text = "Save Project &As...";
			this._menuSaveProjectAs.Click += new System.EventHandler(this._menuSaveProjectAs_Click);
			// 
			// _menuCloseProject
			// 
			this._menuCloseProject.Name = "_menuCloseProject";
			this._menuCloseProject.Size = new System.Drawing.Size(195, 22);
			this._menuCloseProject.Text = "Close Project";
			this._menuCloseProject.Click += new System.EventHandler(this._menuCloseProject_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 6);
			// 
			// _menuRecentProjects
			// 
			this._menuRecentProjects.Name = "_menuRecentProjects";
			this._menuRecentProjects.Size = new System.Drawing.Size(195, 22);
			this._menuRecentProjects.Text = "&Recent Projects";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(192, 6);
			// 
			// _menuExit
			// 
			this._menuExit.Name = "_menuExit";
			this._menuExit.Size = new System.Drawing.Size(195, 22);
			this._menuExit.Text = "E&xit";
			this._menuExit.Click += new System.EventHandler(this._menuExit_Click);
			// 
			// projectToolStripMenuItem
			// 
			this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuRefresh,
            this._menuQueue,
            this._menuDeployQueue,
            this.toolStripMenuItem3,
            this._menuVerifyDatabase,
            this._menuScanDatabase,
            this._menuDeployDatabase,
            this.toolStripMenuItem4,
            this._menuConfiguration,
            this._menuSettings});
			this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
			this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
			this.projectToolStripMenuItem.Text = "&Project";
			// 
			// _menuRefresh
			// 
			this._menuRefresh.Name = "_menuRefresh";
			this._menuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this._menuRefresh.Size = new System.Drawing.Size(179, 22);
			this._menuRefresh.Text = "&Refresh";
			this._menuRefresh.Click += new System.EventHandler(this._menuRefresh_Click);
			// 
			// _menuDeployQueue
			// 
			this._menuDeployQueue.Image = global::Deployer.Properties.Resources.index_up;
			this._menuDeployQueue.Name = "_menuDeployQueue";
			this._menuDeployQueue.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
			this._menuDeployQueue.Size = new System.Drawing.Size(179, 22);
			this._menuDeployQueue.Text = "&Deploy Files";
			this._menuDeployQueue.Click += new System.EventHandler(this._menuUpload_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(176, 6);
			// 
			// _menuVerifyDatabase
			// 
			this._menuVerifyDatabase.Name = "_menuVerifyDatabase";
			this._menuVerifyDatabase.Size = new System.Drawing.Size(179, 22);
			this._menuVerifyDatabase.Text = "&Verify Database";
			this._menuVerifyDatabase.Click += new System.EventHandler(this._menuVerifyDatabase_Click);
			// 
			// _menuScanDatabase
			// 
			this._menuScanDatabase.Name = "_menuScanDatabase";
			this._menuScanDatabase.Size = new System.Drawing.Size(179, 22);
			this._menuScanDatabase.Text = "S&can Database";
			this._menuScanDatabase.ToolTipText = "Scan stored procedures in database";
			this._menuScanDatabase.Click += new System.EventHandler(this._menuScanDatabase_Click);
			// 
			// _menuDeployDatabase
			// 
			this._menuDeployDatabase.Name = "_menuDeployDatabase";
			this._menuDeployDatabase.Size = new System.Drawing.Size(179, 22);
			this._menuDeployDatabase.Text = "D&eploy Database";
			this._menuDeployDatabase.ToolTipText = "Deploy stored procedures";
			this._menuDeployDatabase.Click += new System.EventHandler(this._menuDeployDatabase_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(176, 6);
			// 
			// _menuConfiguration
			// 
			this._menuConfiguration.Name = "_menuConfiguration";
			this._menuConfiguration.Size = new System.Drawing.Size(179, 22);
			this._menuConfiguration.Text = "Configuration";
			// 
			// _menuSettings
			// 
			this._menuSettings.Name = "_menuSettings";
			this._menuSettings.Size = new System.Drawing.Size(179, 22);
			this._menuSettings.Text = "&Settings...";
			this._menuSettings.Click += new System.EventHandler(this._menuSettings_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuAbout});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// _menuAbout
			// 
			this._menuAbout.Name = "_menuAbout";
			this._menuAbout.Size = new System.Drawing.Size(107, 22);
			this._menuAbout.Text = "&About";
			this._menuAbout.Click += new System.EventHandler(this._menuAbout_Click);
			// 
			// _statusStrip
			// 
			this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusProgressBar,
            this._statusLabel,
            this._statusLabel2,
            this._activeConfiguration});
			this._statusStrip.Location = new System.Drawing.Point(0, 624);
			this._statusStrip.Name = "_statusStrip";
			this._statusStrip.Size = new System.Drawing.Size(996, 22);
			this._statusStrip.TabIndex = 15;
			this._statusStrip.Text = "statusStrip1";
			// 
			// _statusProgressBar
			// 
			this._statusProgressBar.Name = "_statusProgressBar";
			this._statusProgressBar.Size = new System.Drawing.Size(100, 16);
			// 
			// _statusLabel
			// 
			this._statusLabel.Name = "_statusLabel";
			this._statusLabel.Size = new System.Drawing.Size(782, 17);
			this._statusLabel.Spring = true;
			this._statusLabel.Text = "Ready";
			this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _statusLabel2
			// 
			this._statusLabel2.Name = "_statusLabel2";
			this._statusLabel2.Size = new System.Drawing.Size(84, 17);
			this._statusLabel2.Text = "Configuration:";
			// 
			// _activeConfiguration
			// 
			this._activeConfiguration.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this._activeConfiguration.Image = ((System.Drawing.Image)(resources.GetObject("_activeConfiguration.Image")));
			this._activeConfiguration.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._activeConfiguration.Name = "_activeConfiguration";
			this._activeConfiguration.Size = new System.Drawing.Size(13, 20);
			this._activeConfiguration.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this._activeConfiguration_DropDownItemClicked);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(996, 646);
			this.Controls.Add(this._tabcontrol);
			this.Controls.Add(this._toolstripTop);
			this.Controls.Add(this._mainmenu);
			this.Controls.Add(this._statusStrip);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this._mainmenu;
			this.MinimumSize = new System.Drawing.Size(500, 300);
			this.Name = "MainForm";
			this.Text = "Deployer";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Closed += new System.EventHandler(this.MainForm_Closed);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
			_bottomtabcontrol.ResumeLayout(false);
			this._queuetab.ResumeLayout(false);
			this._queuetab.PerformLayout();
			this._fileQueueMenu.ResumeLayout(false);
			this._toolstripBottom.ResumeLayout(false);
			this._toolstripBottom.PerformLayout();
			this._logtab.ResumeLayout(false);
			this._logtab.PerformLayout();
			this._tabcontrol.ResumeLayout(false);
			this._filetab.ResumeLayout(false);
			this._splitHorizontal.Panel1.ResumeLayout(false);
			this._splitHorizontal.Panel2.ResumeLayout(false);
			this._splitHorizontal.ResumeLayout(false);
			this._splitVertical.Panel1.ResumeLayout(false);
			this._splitVertical.Panel2.ResumeLayout(false);
			this._splitVertical.ResumeLayout(false);
			this._folderTreeMenu.ResumeLayout(false);
			this._fileListMenu.ResumeLayout(false);
			this._databasetab.ResumeLayout(false);
			this._toolstripTop.ResumeLayout(false);
			this._toolstripTop.PerformLayout();
			this._scanMenu.ResumeLayout(false);
			this._mainmenu.ResumeLayout(false);
			this._mainmenu.PerformLayout();
			this._statusStrip.ResumeLayout(false);
			this._statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private TextBox _logtext;
		private TabControl _tabcontrol;
		private TabPage _filetab;
		private TabPage _databasetab;
		private GlacialList _databaselist;
		private ContextMenu _databaselistmenu;
		private MenuItem _menuViewDetails;
		private MenuItem menuItem5;
		private MenuItem _menuIncludeDbObject;
		private MenuItem _menuExcludeDbObject;
		private MenuItem _menuIncludeAllDbObjects;
		private MenuItem _menuExcludeAllDbObjects;
		private MenuItem menuItem11;
		private SplitContainer _splitVertical;
		private TreeView _folderTree;
		private GlacialList _filelist;
		private SplitContainer _splitHorizontal;
		private ImageList _folderImages;
		private GlacialList _fileQueueList;
		private TabPage _queuetab;
		private TabPage _logtab;
		private ToolStrip _toolstripTop;
		private ToolStripButton _btnOpen;
		private ToolStripButton _btnSave;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripButton _btnSettings;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripButton _btnRefresh;
		private ToolStripButton _btnQueueAll;
		private ToolStripButton _btnDeployQueue;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripButton _btnVerifyDatabase;
		private ToolStripButton _btnScanDatabase;
		private ToolStripButton _btnDeployDatabase;
		private ToolStripDropDownButton _btnQueueModified;
		private ContextMenuStrip _scanMenu;
		private ToolStripSeparator toolStripSeparator4;
		private MenuStrip _mainmenu;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem _menuNewProject;
		private ToolStripMenuItem _menuOpenProject;
		private ToolStripMenuItem _menuSaveProject;
		private ToolStripMenuItem _menuSaveProjectAs;
		private ToolStripSeparator toolStripMenuItem1;
		private ToolStripMenuItem _menuRecentProjects;
		private ToolStripSeparator toolStripMenuItem2;
		private ToolStripMenuItem _menuExit;
		private ToolStripMenuItem projectToolStripMenuItem;
		private ToolStripMenuItem _menuQueue;
		private ToolStripMenuItem _menuDeployQueue;
		private ToolStripSeparator toolStripMenuItem3;
		private ToolStripMenuItem _menuVerifyDatabase;
		private ToolStripMenuItem _menuScanDatabase;
		private ToolStripMenuItem _menuDeployDatabase;
		private ToolStripSeparator toolStripMenuItem4;
		private ToolStripMenuItem _menuSettings;
		private ToolStripMenuItem helpToolStripMenuItem;
		private ToolStripMenuItem _menuAbout;
		private ToolStripMenuItem _menuCloseProject;
		private ToolStripMenuItem _menuRefresh;
		private ContextMenuStrip _fileQueueMenu;
		private ToolStripMenuItem _menuQueueClear;
		private ToolStripButton _btnQueueSinceLastDeploy;
		private ContextMenuStrip _fileListMenu;
		private ToolStripMenuItem _menuQueueFiles;
		private ToolStripSeparator toolStripMenuItem5;
		private ToolStripMenuItem _menuSelectAll;
		private ToolStripMenuItem _menuUnselectAll;
		private ToolStripSeparator toolStripMenuItem6;
		private ToolStripMenuItem _menuEditFiles;
		private ToolStripSeparator toolStripMenuItem7;
		private ToolStripMenuItem _menuInclude;
		private ToolStripMenuItem _menuExclude;
		private ToolStripMenuItem _menuQueueLastDeployment;
		private ToolStripMenuItem _menuQueueDeploy;
		private ToolStripSeparator toolStripSeparator5;
		private ToolStripMenuItem _menuQueueRemove;
		private ToolStripSeparator toolStripSeparator6;
		private ToolStripMenuItem _menuQueueSelectAll;
		private ToolStripMenuItem _menuQueueUnselectAll;
		private ToolStripSeparator toolStripSeparator7;
		private ToolStrip _toolstripBottom;
		private ToolStripButton _queueAutoRemove;
		private ContextMenuStrip _folderTreeMenu;
		private ToolStripMenuItem _menuQueueFolder;
		private ToolStripMenuItem _menuIncludeSingleFile;
		private ToolStripMenuItem _menuIncludeAllInFolder;
		private ToolStripMenuItem _menuIncludeAllInSubfolders;
		private ToolStripMenuItem _menuIncludeAll;
		private ToolStripMenuItem _menuQueueRemoveOther;
		private StatusStrip _statusStrip;
		private ToolStripStatusLabel _statusLabel;
		private ToolStripDropDownButton _activeConfiguration;
		private ToolStripStatusLabel _statusLabel2;
		private ToolStripProgressBar _statusProgressBar;
		private ToolStripMenuItem _menuConfiguration;
	}
}