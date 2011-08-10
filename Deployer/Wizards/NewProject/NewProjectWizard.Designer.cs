namespace Deployer.Wizards.NewProject {
	partial class NewProjectWizard {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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
            this._wizard = new CristiPotlog.Controls.Wizard();
            this._pageSourcePath = new CristiPotlog.Controls.WizardPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this._useProjectFilter = new System.Windows.Forms.CheckBox();
            this._browse = new System.Windows.Forms.Button();
            this._localpath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._pageDestination = new CristiPotlog.Controls.WizardPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._destinationName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._editPluginSettings = new System.Windows.Forms.Button();
            this._plugin = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this._pageFileTypes = new CristiPotlog.Controls.WizardPage();
            this._filtersView = new Deployer.ProjectSettings.Views.FiltersView();
            this._pageFinish = new CristiPotlog.Controls.WizardPage();
            this._wizard.SuspendLayout();
            this._pageSourcePath.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this._pageDestination.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this._pageFileTypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // _wizard
            // 
            this._wizard.Controls.Add(this._pageSourcePath);
            this._wizard.Controls.Add(this._pageFinish);
            this._wizard.Controls.Add(this._pageFileTypes);
            this._wizard.Controls.Add(this._pageDestination);
            this._wizard.HeaderImage = global::Deployer.Properties.Resources.document_new;
            this._wizard.Location = new System.Drawing.Point(0, 0);
            this._wizard.Name = "_wizard";
            this._wizard.Pages.AddRange(new CristiPotlog.Controls.WizardPage[] {
            this._pageSourcePath,
            this._pageDestination,
            this._pageFileTypes,
            this._pageFinish});
            this._wizard.Size = new System.Drawing.Size(761, 556);
            this._wizard.TabIndex = 0;
            this._wizard.BeforeSwitchPages += new CristiPotlog.Controls.Wizard.BeforeSwitchPagesEventHandler(this._wizard_BeforeSwitchPages);
            this._wizard.AfterSwitchPages += new CristiPotlog.Controls.Wizard.AfterSwitchPagesEventHandler(this._wizard_AfterSwitchPages);
            this._wizard.Finish += new System.EventHandler(this._wizard_Finish);
            // 
            // _pageSourcePath
            // 
            this._pageSourcePath.Controls.Add(this.groupBox2);
            this._pageSourcePath.Description = "Specify the location of the files you want to deploy.";
            this._pageSourcePath.Location = new System.Drawing.Point(0, 0);
            this._pageSourcePath.Name = "_pageSourcePath";
            this._pageSourcePath.Size = new System.Drawing.Size(761, 508);
            this._pageSourcePath.TabIndex = 10;
            this._pageSourcePath.Title = "Source path";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this._useProjectFilter);
            this.groupBox2.Controls.Add(this._browse);
            this.groupBox2.Controls.Add(this._localpath);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(737, 80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Source directory";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(457, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(264, 48);
            this.label5.TabIndex = 3;
            this.label5.Text = "This is the root folder for the files you wish to deploy. The default behavior is" +
    " to mirror the directory structure in the destination folder.";
            // 
            // _useProjectFilter
            // 
            this._useProjectFilter.Enabled = false;
            this._useProjectFilter.Location = new System.Drawing.Point(72, 48);
            this._useProjectFilter.Name = "_useProjectFilter";
            this._useProjectFilter.Size = new System.Drawing.Size(248, 24);
            this._useProjectFilter.TabIndex = 2;
            this._useProjectFilter.Text = "Scan from Visual Studio projectfile (*.csproj)";
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
            this._localpath.TextChanged += new System.EventHandler(this._localpath_TextChanged);
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
            // _pageDestination
            // 
            this._pageDestination.Controls.Add(this.groupBox1);
            this._pageDestination.Description = "Please specify where you would like to deploy your files.";
            this._pageDestination.Location = new System.Drawing.Point(0, 0);
            this._pageDestination.Name = "_pageDestination";
            this._pageDestination.Size = new System.Drawing.Size(428, 208);
            this._pageDestination.TabIndex = 11;
            this._pageDestination.Title = "Destination";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this._destinationName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this._editPluginSettings);
            this.groupBox1.Controls.Add(this._plugin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(737, 95);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Destination";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(414, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(293, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = "Remember to edit the settings!";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(414, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(293, 45);
            this.label4.TabIndex = 10;
            this.label4.Text = "This will be the default destination for your project. You can add any number of " +
    "additional destinations in the project settings later.";
            // 
            // _destinationName
            // 
            this._destinationName.Location = new System.Drawing.Point(93, 20);
            this._destinationName.Name = "_destinationName";
            this._destinationName.Size = new System.Drawing.Size(200, 21);
            this._destinationName.TabIndex = 9;
            this._destinationName.TextChanged += new System.EventHandler(this._destinationName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name";
            // 
            // _editPluginSettings
            // 
            this._editPluginSettings.Location = new System.Drawing.Point(298, 46);
            this._editPluginSettings.Name = "_editPluginSettings";
            this._editPluginSettings.Size = new System.Drawing.Size(100, 23);
            this._editPluginSettings.TabIndex = 7;
            this._editPluginSettings.Text = "Edit settings...";
            this._editPluginSettings.UseVisualStyleBackColor = true;
            this._editPluginSettings.Click += new System.EventHandler(this._editPluginSettings_Click);
            // 
            // _plugin
            // 
            this._plugin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._plugin.FormattingEnabled = true;
            this._plugin.Location = new System.Drawing.Point(93, 47);
            this._plugin.Name = "_plugin";
            this._plugin.Size = new System.Drawing.Size(200, 21);
            this._plugin.TabIndex = 6;
            this._plugin.SelectionChangeCommitted += new System.EventHandler(this._plugin_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Deploy method";
            // 
            // _pageFileTypes
            // 
            this._pageFileTypes.Controls.Add(this._filtersView);
            this._pageFileTypes.Description = "Please specify the types of files to deploy.";
            this._pageFileTypes.Location = new System.Drawing.Point(0, 0);
            this._pageFileTypes.Name = "_pageFileTypes";
            this._pageFileTypes.Size = new System.Drawing.Size(428, 208);
            this._pageFileTypes.TabIndex = 12;
            this._pageFileTypes.Title = "File types";
            // 
            // _filtersView
            // 
            this._filtersView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._filtersView.Location = new System.Drawing.Point(12, 74);
            this._filtersView.Name = "_filtersView";
            this._filtersView.Project = null;
            this._filtersView.Size = new System.Drawing.Size(737, 414);
            this._filtersView.TabIndex = 5;
            // 
            // _pageFinish
            // 
            this._pageFinish.Description = "The new project has now been created. Ensure you configure the file filters to de" +
    "ploy the correct files.";
            this._pageFinish.Location = new System.Drawing.Point(0, 0);
            this._pageFinish.Name = "_pageFinish";
            this._pageFinish.Size = new System.Drawing.Size(761, 508);
            this._pageFinish.Style = CristiPotlog.Controls.WizardPageStyle.Finish;
            this._pageFinish.TabIndex = 13;
            this._pageFinish.Title = "Finished";
            // 
            // NewProjectWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 556);
            this.Controls.Add(this._wizard);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProjectWizard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New project";
            this.Load += new System.EventHandler(this.NewProjectWizard_Load);
            this._wizard.ResumeLayout(false);
            this._pageSourcePath.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this._pageDestination.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this._pageFileTypes.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private CristiPotlog.Controls.Wizard _wizard;
		private CristiPotlog.Controls.WizardPage _pageSourcePath;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox _useProjectFilter;
		private System.Windows.Forms.Button _browse;
		private System.Windows.Forms.TextBox _localpath;
		private System.Windows.Forms.Label label2;
		private CristiPotlog.Controls.WizardPage _pageDestination;
		private CristiPotlog.Controls.WizardPage _pageFileTypes;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox _destinationName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button _editPluginSettings;
		private System.Windows.Forms.ComboBox _plugin;
        private System.Windows.Forms.Label label3;
		private CristiPotlog.Controls.WizardPage _pageFinish;
        private System.Windows.Forms.Label label4;
        private ProjectSettings.Views.FiltersView _filtersView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
	}
}