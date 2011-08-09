namespace Deployer.ProjectSettings.Views {
	partial class FiltersView {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FiltersView));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this._addFilter = new System.Windows.Forms.Button();
			this._removeFilter = new System.Windows.Forms.Button();
			this._filters = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.Include = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this._remove = new System.Windows.Forms.Button();
			this._moveDown = new System.Windows.Forms.Button();
			this._moveUp = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this._includeFiles = new System.Windows.Forms.DataGridView();
			this.includeFileDestination = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.expressionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.includeFileExpressionType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.remotePathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.RemoteFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._includeFilesSource = new System.Windows.Forms.BindingSource(this.components);
			this.Exclude = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this._excludeFiles = new System.Windows.Forms.DataGridView();
			this.expressionDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.excludeFileExpressionType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this._excludeFilesSource = new System.Windows.Forms.BindingSource(this.components);
			this.label9 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this._excludeDirectories = new System.Windows.Forms.DataGridView();
			this.expressionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.excludeDirectoryExpressionType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this._excludeDirectoriesSource = new System.Windows.Forms.BindingSource(this.components);
			this.label3 = new System.Windows.Forms.Label();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.Include.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._includeFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._includeFilesSource)).BeginInit();
			this.Exclude.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.groupBox9.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._excludeFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._excludeFilesSource)).BeginInit();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._excludeDirectories)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._excludeDirectoriesSource)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
			this.splitContainer1.Size = new System.Drawing.Size(746, 472);
			this.splitContainer1.SplitterDistance = 179;
			this.splitContainer1.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.tableLayoutPanel1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(179, 472);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Filters";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this._filters, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(173, 452);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this._addFilter);
			this.flowLayoutPanel1.Controls.Add(this._removeFilter);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 420);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(167, 29);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// _addFilter
			// 
			this._addFilter.Enabled = false;
			this._addFilter.Location = new System.Drawing.Point(3, 3);
			this._addFilter.Name = "_addFilter";
			this._addFilter.Size = new System.Drawing.Size(75, 23);
			this._addFilter.TabIndex = 0;
			this._addFilter.Text = "Add";
			this._addFilter.UseVisualStyleBackColor = true;
			// 
			// _removeFilter
			// 
			this._removeFilter.Enabled = false;
			this._removeFilter.Location = new System.Drawing.Point(84, 3);
			this._removeFilter.Name = "_removeFilter";
			this._removeFilter.Size = new System.Drawing.Size(75, 23);
			this._removeFilter.TabIndex = 0;
			this._removeFilter.Text = "Remove";
			this._removeFilter.UseVisualStyleBackColor = true;
			// 
			// _filters
			// 
			this._filters.Dock = System.Windows.Forms.DockStyle.Fill;
			this._filters.FormattingEnabled = true;
			this._filters.Location = new System.Drawing.Point(3, 3);
			this._filters.Name = "_filters";
			this._filters.Size = new System.Drawing.Size(167, 407);
			this._filters.TabIndex = 1;
			this._filters.SelectedIndexChanged += new System.EventHandler(this._filters_SelectedIndexChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.tabControl1);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(563, 472);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Filter settings";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.Include);
			this.tabControl1.Controls.Add(this.Exclude);
			this.tabControl1.Location = new System.Drawing.Point(6, 20);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(551, 446);
			this.tabControl1.TabIndex = 0;
			// 
			// Include
			// 
			this.Include.Controls.Add(this.groupBox3);
			this.Include.Location = new System.Drawing.Point(4, 22);
			this.Include.Name = "Include";
			this.Include.Padding = new System.Windows.Forms.Padding(3);
			this.Include.Size = new System.Drawing.Size(543, 420);
			this.Include.TabIndex = 0;
			this.Include.Text = "Include";
			this.Include.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this._remove);
			this.groupBox3.Controls.Add(this._moveDown);
			this.groupBox3.Controls.Add(this._moveUp);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this._includeFiles);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Location = new System.Drawing.Point(3, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(537, 414);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Include files";
			// 
			// _remove
			// 
			this._remove.Location = new System.Drawing.Point(171, 389);
			this._remove.Name = "_remove";
			this._remove.Size = new System.Drawing.Size(75, 23);
			this._remove.TabIndex = 5;
			this._remove.Text = "Remove";
			this._remove.UseVisualStyleBackColor = true;
			this._remove.Click += new System.EventHandler(this._remove_Click);
			// 
			// _moveDown
			// 
			this._moveDown.Location = new System.Drawing.Point(90, 389);
			this._moveDown.Name = "_moveDown";
			this._moveDown.Size = new System.Drawing.Size(75, 23);
			this._moveDown.TabIndex = 4;
			this._moveDown.Text = "Move Down";
			this._moveDown.UseVisualStyleBackColor = true;
			this._moveDown.Click += new System.EventHandler(this._moveDown_Click);
			// 
			// _moveUp
			// 
			this._moveUp.Location = new System.Drawing.Point(9, 389);
			this._moveUp.Name = "_moveUp";
			this._moveUp.Size = new System.Drawing.Size(75, 23);
			this._moveUp.TabIndex = 4;
			this._moveUp.Text = "Move Up";
			this._moveUp.UseVisualStyleBackColor = true;
			this._moveUp.Click += new System.EventHandler(this._moveUp_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(525, 36);
			this.label1.TabIndex = 2;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// _includeFiles
			// 
			this._includeFiles.AllowUserToResizeRows = false;
			this._includeFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._includeFiles.AutoGenerateColumns = false;
			this._includeFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this._includeFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this._includeFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.includeFileDestination,
            this.expressionDataGridViewTextBoxColumn,
            this.includeFileExpressionType,
            this.remotePathDataGridViewTextBoxColumn,
            this.RemoteFileName});
			this._includeFiles.DataSource = this._includeFilesSource;
			this._includeFiles.Location = new System.Drawing.Point(6, 56);
			this._includeFiles.Name = "_includeFiles";
			this._includeFiles.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this._includeFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._includeFiles.Size = new System.Drawing.Size(525, 326);
			this._includeFiles.TabIndex = 3;
			this._includeFiles.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this._includeFiles_DataError);
			this._includeFiles.SelectionChanged += new System.EventHandler(this._includeFiles_SelectionChanged);
			// 
			// includeFileDestination
			// 
			this.includeFileDestination.DataPropertyName = "DeployDestinationIdentifier";
			this.includeFileDestination.HeaderText = "Destination";
			this.includeFileDestination.Name = "includeFileDestination";
			this.includeFileDestination.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.includeFileDestination.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.includeFileDestination.Width = 86;
			// 
			// expressionDataGridViewTextBoxColumn
			// 
			this.expressionDataGridViewTextBoxColumn.DataPropertyName = "Expression";
			this.expressionDataGridViewTextBoxColumn.HeaderText = "Expression";
			this.expressionDataGridViewTextBoxColumn.Name = "expressionDataGridViewTextBoxColumn";
			this.expressionDataGridViewTextBoxColumn.Width = 84;
			// 
			// includeFileExpressionType
			// 
			this.includeFileExpressionType.DataPropertyName = "ExpressionType";
			this.includeFileExpressionType.HeaderText = "Type";
			this.includeFileExpressionType.Name = "includeFileExpressionType";
			this.includeFileExpressionType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.includeFileExpressionType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.includeFileExpressionType.Width = 56;
			// 
			// remotePathDataGridViewTextBoxColumn
			// 
			this.remotePathDataGridViewTextBoxColumn.DataPropertyName = "RemotePath";
			this.remotePathDataGridViewTextBoxColumn.HeaderText = "RemotePath";
			this.remotePathDataGridViewTextBoxColumn.Name = "remotePathDataGridViewTextBoxColumn";
			this.remotePathDataGridViewTextBoxColumn.Width = 91;
			// 
			// RemoteFileName
			// 
			this.RemoteFileName.DataPropertyName = "RemoteFileName";
			this.RemoteFileName.HeaderText = "Remote Filename";
			this.RemoteFileName.Name = "RemoteFileName";
			this.RemoteFileName.Width = 114;
			// 
			// _includeFilesSource
			// 
			this._includeFilesSource.DataSource = typeof(DeployerEngine.Project.FilterCollection);
			this._includeFilesSource.AddingNew += new System.ComponentModel.AddingNewEventHandler(this._includeFilesSource_AddingNew);
			this._includeFilesSource.CurrentItemChanged += new System.EventHandler(this._includeFilesSource_CurrentItemChanged);
			// 
			// Exclude
			// 
			this.Exclude.Controls.Add(this.tableLayoutPanel2);
			this.Exclude.Location = new System.Drawing.Point(4, 22);
			this.Exclude.Name = "Exclude";
			this.Exclude.Padding = new System.Windows.Forms.Padding(3);
			this.Exclude.Size = new System.Drawing.Size(543, 420);
			this.Exclude.TabIndex = 1;
			this.Exclude.Text = "Exclude";
			this.Exclude.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.groupBox9, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.groupBox4, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(537, 414);
			this.tableLayoutPanel2.TabIndex = 4;
			// 
			// groupBox9
			// 
			this.groupBox9.Controls.Add(this._excludeFiles);
			this.groupBox9.Controls.Add(this.label9);
			this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox9.Location = new System.Drawing.Point(3, 210);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(531, 201);
			this.groupBox9.TabIndex = 3;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "Exclude files";
			// 
			// _excludeFiles
			// 
			this._excludeFiles.AllowUserToResizeRows = false;
			this._excludeFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._excludeFiles.AutoGenerateColumns = false;
			this._excludeFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this._excludeFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this._excludeFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.expressionDataGridViewTextBoxColumn2,
            this.excludeFileExpressionType});
			this._excludeFiles.DataSource = this._excludeFilesSource;
			this._excludeFiles.Location = new System.Drawing.Point(8, 45);
			this._excludeFiles.Name = "_excludeFiles";
			this._excludeFiles.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this._excludeFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._excludeFiles.Size = new System.Drawing.Size(517, 150);
			this._excludeFiles.TabIndex = 1;
			// 
			// expressionDataGridViewTextBoxColumn2
			// 
			this.expressionDataGridViewTextBoxColumn2.DataPropertyName = "Expression";
			this.expressionDataGridViewTextBoxColumn2.HeaderText = "Expression";
			this.expressionDataGridViewTextBoxColumn2.Name = "expressionDataGridViewTextBoxColumn2";
			this.expressionDataGridViewTextBoxColumn2.Width = 84;
			// 
			// excludeFileExpressionType
			// 
			this.excludeFileExpressionType.DataPropertyName = "ExpressionType";
			this.excludeFileExpressionType.HeaderText = "Type";
			this.excludeFileExpressionType.Name = "excludeFileExpressionType";
			this.excludeFileExpressionType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.excludeFileExpressionType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.excludeFileExpressionType.Width = 56;
			// 
			// _excludeFilesSource
			// 
			this._excludeFilesSource.DataSource = typeof(DeployerEngine.Project.FilterCollection);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 24);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(448, 21);
			this.label9.TabIndex = 0;
			this.label9.Text = "Files matching any expression in the list below will not be included in deploymen" +
				"t.";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this._excludeDirectories);
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox4.Location = new System.Drawing.Point(3, 3);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(531, 201);
			this.groupBox4.TabIndex = 2;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Exclude directories";
			// 
			// _excludeDirectories
			// 
			this._excludeDirectories.AllowUserToResizeRows = false;
			this._excludeDirectories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._excludeDirectories.AutoGenerateColumns = false;
			this._excludeDirectories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this._excludeDirectories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this._excludeDirectories.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.expressionDataGridViewTextBoxColumn1,
            this.excludeDirectoryExpressionType});
			this._excludeDirectories.DataSource = this._excludeDirectoriesSource;
			this._excludeDirectories.Location = new System.Drawing.Point(8, 45);
			this._excludeDirectories.Name = "_excludeDirectories";
			this._excludeDirectories.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this._excludeDirectories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._excludeDirectories.Size = new System.Drawing.Size(517, 150);
			this._excludeDirectories.TabIndex = 3;
			// 
			// expressionDataGridViewTextBoxColumn1
			// 
			this.expressionDataGridViewTextBoxColumn1.DataPropertyName = "Expression";
			this.expressionDataGridViewTextBoxColumn1.HeaderText = "Expression";
			this.expressionDataGridViewTextBoxColumn1.Name = "expressionDataGridViewTextBoxColumn1";
			this.expressionDataGridViewTextBoxColumn1.Width = 84;
			// 
			// excludeDirectoryExpressionType
			// 
			this.excludeDirectoryExpressionType.DataPropertyName = "ExpressionType";
			this.excludeDirectoryExpressionType.HeaderText = "Type";
			this.excludeDirectoryExpressionType.Name = "excludeDirectoryExpressionType";
			this.excludeDirectoryExpressionType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.excludeDirectoryExpressionType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.excludeDirectoryExpressionType.Width = 56;
			// 
			// _excludeDirectoriesSource
			// 
			this._excludeDirectoriesSource.DataSource = typeof(DeployerEngine.Project.FilterCollection);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(448, 18);
			this.label3.TabIndex = 2;
			this.label3.Text = "Directories matching any expression in the list below will not be included in dep" +
				"loyment.";
			// 
			// FiltersView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "FiltersView";
			this.Size = new System.Drawing.Size(746, 472);
			this.Load += new System.EventHandler(this.FiltersView_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.Include.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._includeFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._includeFilesSource)).EndInit();
			this.Exclude.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.groupBox9.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._excludeFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._excludeFilesSource)).EndInit();
			this.groupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._excludeDirectories)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._excludeDirectoriesSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button _addFilter;
		private System.Windows.Forms.Button _removeFilter;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListBox _filters;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage Include;
		private System.Windows.Forms.TabPage Exclude;
		private System.Windows.Forms.DataGridView _includeFiles;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.DataGridView _excludeFiles;
		private System.Windows.Forms.DataGridView _excludeDirectories;
		private System.Windows.Forms.BindingSource _includeFilesSource;
		private System.Windows.Forms.BindingSource _excludeDirectoriesSource;
		private System.Windows.Forms.BindingSource _excludeFilesSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn expressionDataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewComboBoxColumn excludeDirectoryExpressionType;
		private System.Windows.Forms.DataGridViewTextBoxColumn expressionDataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewComboBoxColumn excludeFileExpressionType;
		private System.Windows.Forms.DataGridViewComboBoxColumn includeFileDestination;
		private System.Windows.Forms.DataGridViewTextBoxColumn expressionDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn includeFileExpressionType;
		private System.Windows.Forms.DataGridViewTextBoxColumn remotePathDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn RemoteFileName;
		private System.Windows.Forms.Button _moveUp;
		private System.Windows.Forms.Button _moveDown;
		private System.Windows.Forms.Button _remove;
	}
}
