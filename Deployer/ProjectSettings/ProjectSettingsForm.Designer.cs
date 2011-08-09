using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DeployerEngine;
using DeployerEngine.Project;
using DeployerPluginInterfaces;
using GlacialComponents.Controls;

namespace Deployer
{
	/// <summary>
	/// Summary description for ProjectSettings.
	/// </summary>
	partial class ProjectSettingsForm
	{
		private TabControl tabControl1;
		private Button _cancel;
		private Button _ok;
		private GroupBox groupBox2;
		private TextBox _localpath;
		private GroupBox groupBox3;
		private Label label2;
		private GlacialList _pluginlist;
		private TabPage General;
		private ToolTip _tooltip;
		private Button _browse;
		private CheckBox _useProjectFilter;
		private System.Windows.Forms.TabPage Database;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox _sourcedbname;
		private System.Windows.Forms.TextBox _sourcedburl;
		private System.Windows.Forms.TextBox _destinationdbname;
		private System.Windows.Forms.TextBox _destinationdburl;
		private System.Windows.Forms.TabPage StoredProcedures;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.DataGrid _excludeProcedures;
		private System.Windows.Forms.Label label8;
		private TabPage Timestamp;
		private IContainer components;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
			GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.General = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this._pluginlist = new GlacialComponents.Controls.GlacialList();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this._useProjectFilter = new System.Windows.Forms.CheckBox();
			this._browse = new System.Windows.Forms.Button();
			this._localpath = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.Destinations = new System.Windows.Forms.TabPage();
			this.Filters = new System.Windows.Forms.TabPage();
			this.Hooks = new System.Windows.Forms.TabPage();
			this.Timestamp = new System.Windows.Forms.TabPage();
			this.Database = new System.Windows.Forms.TabPage();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this._destinationdbname = new System.Windows.Forms.TextBox();
			this._destinationdburl = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this._sourcedbname = new System.Windows.Forms.TextBox();
			this._sourcedburl = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.StoredProcedures = new System.Windows.Forms.TabPage();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this._excludeProcedures = new System.Windows.Forms.DataGrid();
			this._cancel = new System.Windows.Forms.Button();
			this._ok = new System.Windows.Forms.Button();
			this._tooltip = new System.Windows.Forms.ToolTip(this.components);
			this._destinationsView = new Deployer.ProjectSettings.Views.DestinationsView();
			this._filtersView = new Deployer.ProjectSettings.Views.FiltersView();
			this._timestampView = new Deployer.ProjectSettings.Views.TimestampView();
			this._hooksView = new Deployer.ProjectSettings.Views.HooksView();
			this.tabControl1.SuspendLayout();
			this.General.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.Destinations.SuspendLayout();
			this.Filters.SuspendLayout();
			this.Hooks.SuspendLayout();
			this.Timestamp.SuspendLayout();
			this.Database.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.StoredProcedures.SuspendLayout();
			this.groupBox7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._excludeProcedures)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.General);
			this.tabControl1.Controls.Add(this.Destinations);
			this.tabControl1.Controls.Add(this.Filters);
			this.tabControl1.Controls.Add(this.Hooks);
			this.tabControl1.Controls.Add(this.Timestamp);
			this.tabControl1.Controls.Add(this.Database);
			this.tabControl1.Controls.Add(this.StoredProcedures);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(770, 515);
			this.tabControl1.TabIndex = 0;
			// 
			// General
			// 
			this.General.Controls.Add(this.groupBox3);
			this.General.Controls.Add(this.groupBox2);
			this.General.Location = new System.Drawing.Point(4, 22);
			this.General.Name = "General";
			this.General.Size = new System.Drawing.Size(762, 489);
			this.General.TabIndex = 0;
			this.General.Text = "General";
			this.General.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this._pluginlist);
			this.groupBox3.Location = new System.Drawing.Point(8, 96);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(464, 181);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Available deployment plugins";
			// 
			// _pluginlist
			// 
			this._pluginlist.BackColor = System.Drawing.SystemColors.Window;
			glColumn1.Name = "Name";
			glColumn1.Text = "Name";
			glColumn1.Width = 150;
			glColumn2.Name = "Identifier";
			glColumn2.Text = "Identifier";
			glColumn3.Name = "Version";
			glColumn3.Text = "Version";
			glColumn3.Width = 80;
			this._pluginlist.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3});
			this._pluginlist.HotTrackingColor = System.Drawing.SystemColors.HotTrack;
			this._pluginlist.ImageList = null;
			this._pluginlist.Location = new System.Drawing.Point(8, 24);
			this._pluginlist.MultiSelect = true;
			this._pluginlist.Name = "_pluginlist";
			this._pluginlist.SelectedTextColor = System.Drawing.SystemColors.HighlightText;
			this._pluginlist.SelectionColor = System.Drawing.SystemColors.Highlight;
			this._pluginlist.Size = new System.Drawing.Size(448, 136);
			this._pluginlist.TabIndex = 4;
			this._pluginlist.Text = "glacialList1";
			this._pluginlist.UnfocusedSelectionColor = System.Drawing.SystemColors.Highlight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this._useProjectFilter);
			this.groupBox2.Controls.Add(this._browse);
			this.groupBox2.Controls.Add(this._localpath);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(8, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(464, 80);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Source directory";
			// 
			// _useProjectFilter
			// 
			this._useProjectFilter.Location = new System.Drawing.Point(72, 48);
			this._useProjectFilter.Name = "_useProjectFilter";
			this._useProjectFilter.Size = new System.Drawing.Size(248, 24);
			this._useProjectFilter.TabIndex = 2;
			this._useProjectFilter.Text = "Scan from projectfile (VS2003 or VS2005)";
			// 
			// _browse
			// 
			this._browse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._browse.Location = new System.Drawing.Point(376, 24);
			this._browse.Name = "_browse";
			this._browse.Size = new System.Drawing.Size(75, 23);
			this._browse.TabIndex = 1;
			this._browse.Text = "Browse...";
			this._browse.Click += new System.EventHandler(this._browse_Click);
			// 
			// _localpath
			// 
			this._localpath.Location = new System.Drawing.Point(72, 24);
			this._localpath.Name = "_localpath";
			this._localpath.Size = new System.Drawing.Size(296, 21);
			this._localpath.TabIndex = 0;
			this._tooltip.SetToolTip(this._localpath, "The path where the files to deploy are stored. Relative paths will be calculated " +
					"from the location of the project file.");
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Local path";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// Destinations
			// 
			this.Destinations.Controls.Add(this._destinationsView);
			this.Destinations.Location = new System.Drawing.Point(4, 22);
			this.Destinations.Name = "Destinations";
			this.Destinations.Size = new System.Drawing.Size(762, 489);
			this.Destinations.TabIndex = 6;
			this.Destinations.Text = "Destinations";
			this.Destinations.UseVisualStyleBackColor = true;
			// 
			// Filters
			// 
			this.Filters.Controls.Add(this._filtersView);
			this.Filters.Location = new System.Drawing.Point(4, 22);
			this.Filters.Name = "Filters";
			this.Filters.Size = new System.Drawing.Size(762, 489);
			this.Filters.TabIndex = 7;
			this.Filters.Text = "Filters";
			this.Filters.UseVisualStyleBackColor = true;
			// 
			// Hooks
			// 
			this.Hooks.Controls.Add(this._hooksView);
			this.Hooks.Location = new System.Drawing.Point(4, 22);
			this.Hooks.Name = "Hooks";
			this.Hooks.Size = new System.Drawing.Size(762, 489);
			this.Hooks.TabIndex = 8;
			this.Hooks.Text = "Hooks";
			this.Hooks.UseVisualStyleBackColor = true;
			// 
			// Timestamp
			// 
			this.Timestamp.Controls.Add(this._timestampView);
			this.Timestamp.Location = new System.Drawing.Point(4, 22);
			this.Timestamp.Name = "Timestamp";
			this.Timestamp.Size = new System.Drawing.Size(762, 489);
			this.Timestamp.TabIndex = 5;
			this.Timestamp.Text = "Timestamp";
			this.Timestamp.UseVisualStyleBackColor = true;
			// 
			// Database
			// 
			this.Database.Controls.Add(this.groupBox6);
			this.Database.Controls.Add(this.groupBox5);
			this.Database.Location = new System.Drawing.Point(4, 22);
			this.Database.Name = "Database";
			this.Database.Size = new System.Drawing.Size(762, 489);
			this.Database.TabIndex = 3;
			this.Database.Text = "Database";
			this.Database.UseVisualStyleBackColor = true;
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.label6);
			this.groupBox6.Controls.Add(this._destinationdbname);
			this.groupBox6.Controls.Add(this._destinationdburl);
			this.groupBox6.Controls.Add(this.label7);
			this.groupBox6.Location = new System.Drawing.Point(8, 104);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(464, 88);
			this.groupBox6.TabIndex = 1;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Destination database";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 24);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(88, 23);
			this.label6.TabIndex = 5;
			this.label6.Text = "Name";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// _destinationdbname
			// 
			this._destinationdbname.Location = new System.Drawing.Point(104, 24);
			this._destinationdbname.Name = "_destinationdbname";
			this._destinationdbname.Size = new System.Drawing.Size(352, 21);
			this._destinationdbname.TabIndex = 3;
			// 
			// _destinationdburl
			// 
			this._destinationdburl.Location = new System.Drawing.Point(104, 48);
			this._destinationdburl.Name = "_destinationdburl";
			this._destinationdburl.Size = new System.Drawing.Size(352, 21);
			this._destinationdburl.TabIndex = 2;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 48);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(88, 23);
			this.label7.TabIndex = 4;
			this.label7.Text = "Webservice URL";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.label4);
			this.groupBox5.Controls.Add(this._sourcedbname);
			this.groupBox5.Controls.Add(this._sourcedburl);
			this.groupBox5.Controls.Add(this.label5);
			this.groupBox5.Location = new System.Drawing.Point(8, 8);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(464, 88);
			this.groupBox5.TabIndex = 0;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Source database";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 23);
			this.label4.TabIndex = 1;
			this.label4.Text = "Name";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// _sourcedbname
			// 
			this._sourcedbname.Location = new System.Drawing.Point(104, 24);
			this._sourcedbname.Name = "_sourcedbname";
			this._sourcedbname.Size = new System.Drawing.Size(352, 21);
			this._sourcedbname.TabIndex = 0;
			// 
			// _sourcedburl
			// 
			this._sourcedburl.Location = new System.Drawing.Point(104, 48);
			this._sourcedburl.Name = "_sourcedburl";
			this._sourcedburl.Size = new System.Drawing.Size(352, 21);
			this._sourcedburl.TabIndex = 0;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 23);
			this.label5.TabIndex = 1;
			this.label5.Text = "Webservice URL";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// StoredProcedures
			// 
			this.StoredProcedures.Controls.Add(this.groupBox7);
			this.StoredProcedures.Location = new System.Drawing.Point(4, 22);
			this.StoredProcedures.Name = "StoredProcedures";
			this.StoredProcedures.Size = new System.Drawing.Size(762, 489);
			this.StoredProcedures.TabIndex = 4;
			this.StoredProcedures.Text = "Stored procedures";
			this.StoredProcedures.UseVisualStyleBackColor = true;
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.label8);
			this.groupBox7.Controls.Add(this._excludeProcedures);
			this.groupBox7.Location = new System.Drawing.Point(8, 8);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(464, 288);
			this.groupBox7.TabIndex = 3;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Exclude stored procedures";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 24);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(448, 32);
			this.label8.TabIndex = 3;
			this.label8.Text = "Stored procedures matching any expression in the list below will not be included " +
				"in deployment.";
			// 
			// _excludeProcedures
			// 
			this._excludeProcedures.CaptionVisible = false;
			this._excludeProcedures.DataMember = "";
			this._excludeProcedures.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this._excludeProcedures.Location = new System.Drawing.Point(8, 56);
			this._excludeProcedures.Name = "_excludeProcedures";
			this._excludeProcedures.Size = new System.Drawing.Size(448, 224);
			this._excludeProcedures.TabIndex = 0;
			// 
			// _cancel
			// 
			this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._cancel.Location = new System.Drawing.Point(707, 533);
			this._cancel.Name = "_cancel";
			this._cancel.Size = new System.Drawing.Size(75, 23);
			this._cancel.TabIndex = 1;
			this._cancel.Text = "Cancel";
			this._cancel.Click += new System.EventHandler(this._cancel_Click);
			// 
			// _ok
			// 
			this._ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._ok.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._ok.Location = new System.Drawing.Point(627, 533);
			this._ok.Name = "_ok";
			this._ok.Size = new System.Drawing.Size(75, 23);
			this._ok.TabIndex = 1;
			this._ok.Text = "OK";
			this._ok.Click += new System.EventHandler(this._ok_Click);
			// 
			// _tooltip
			// 
			this._tooltip.AutoPopDelay = 10000;
			this._tooltip.InitialDelay = 200;
			this._tooltip.ReshowDelay = 100;
			// 
			// _destinationsView
			// 
			this._destinationsView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._destinationsView.Location = new System.Drawing.Point(8, 8);
			this._destinationsView.Name = "_destinationsView";
			this._destinationsView.Project = null;
			this._destinationsView.Size = new System.Drawing.Size(746, 472);
			this._destinationsView.TabIndex = 0;
			// 
			// _filtersView
			// 
			this._filtersView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._filtersView.Location = new System.Drawing.Point(8, 8);
			this._filtersView.Name = "_filtersView";
			this._filtersView.Project = null;
			this._filtersView.Size = new System.Drawing.Size(746, 472);
			this._filtersView.TabIndex = 0;
			// 
			// _timestampView
			// 
			this._timestampView.Location = new System.Drawing.Point(8, 8);
			this._timestampView.Name = "_timestampView";
			this._timestampView.Project = null;
			this._timestampView.Size = new System.Drawing.Size(746, 472);
			this._timestampView.TabIndex = 0;
			// 
			// _hooksView
			// 
			this._hooksView.Location = new System.Drawing.Point(8, 8);
			this._hooksView.Name = "_hooksView";
			this._hooksView.Size = new System.Drawing.Size(746, 472);
			this._hooksView.TabIndex = 0;
			// 
			// ProjectSettingsForm
			// 
			this.AcceptButton = this._ok;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this._cancel;
			this.ClientSize = new System.Drawing.Size(794, 568);
			this.ControlBox = false;
			this.Controls.Add(this._cancel);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this._ok);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ProjectSettingsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Project Settings";
			this.Load += new System.EventHandler(this.ProjectSettingsForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.General.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.Destinations.ResumeLayout(false);
			this.Filters.ResumeLayout(false);
			this.Hooks.ResumeLayout(false);
			this.Timestamp.ResumeLayout(false);
			this.Database.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.StoredProcedures.ResumeLayout(false);
			this.groupBox7.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._excludeProcedures)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private TabPage Destinations;
		private Deployer.ProjectSettings.Views.DestinationsView _destinationsView;
		private TabPage Filters;
		private Deployer.ProjectSettings.Views.FiltersView _filtersView;
		private Deployer.ProjectSettings.Views.TimestampView _timestampView;
		private TabPage Hooks;
		private ProjectSettings.Views.HooksView _hooksView;

	}

}
