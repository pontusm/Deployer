namespace Deployer.ProjectSettings.Views {
	partial class DestinationsView {
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this._addDestination = new System.Windows.Forms.Button();
			this._removeDestination = new System.Windows.Forms.Button();
			this._destinations = new System.Windows.Forms.ListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._destinationName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this._editPluginSettings = new System.Windows.Forms.Button();
			this._plugin = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
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
			this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
			this.splitContainer1.Size = new System.Drawing.Size(746, 472);
			this.splitContainer1.SplitterDistance = 247;
			this.splitContainer1.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.tableLayoutPanel1);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(247, 472);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Deploy destinations";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this._destinations, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(241, 452);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this._addDestination);
			this.flowLayoutPanel1.Controls.Add(this._removeDestination);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 420);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(162, 29);
			this.flowLayoutPanel1.TabIndex = 2;
			// 
			// _addDestination
			// 
			this._addDestination.Location = new System.Drawing.Point(3, 3);
			this._addDestination.Name = "_addDestination";
			this._addDestination.Size = new System.Drawing.Size(75, 23);
			this._addDestination.TabIndex = 2;
			this._addDestination.Text = "Add";
			this._addDestination.UseVisualStyleBackColor = true;
			this._addDestination.Click += new System.EventHandler(this._addDestination_Click);
			// 
			// _removeDestination
			// 
			this._removeDestination.Location = new System.Drawing.Point(84, 3);
			this._removeDestination.Name = "_removeDestination";
			this._removeDestination.Size = new System.Drawing.Size(75, 23);
			this._removeDestination.TabIndex = 2;
			this._removeDestination.Text = "Remove";
			this._removeDestination.UseVisualStyleBackColor = true;
			this._removeDestination.Click += new System.EventHandler(this._removeDestination_Click);
			// 
			// _destinations
			// 
			this._destinations.Dock = System.Windows.Forms.DockStyle.Fill;
			this._destinations.FormattingEnabled = true;
			this._destinations.Location = new System.Drawing.Point(3, 3);
			this._destinations.Name = "_destinations";
			this._destinations.Size = new System.Drawing.Size(235, 411);
			this._destinations.TabIndex = 3;
			this._destinations.SelectedIndexChanged += new System.EventHandler(this._destinations_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this._destinationName);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this._editPluginSettings);
			this.groupBox1.Controls.Add(this._plugin);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(495, 472);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Destination settings";
			// 
			// _destinationName
			// 
			this._destinationName.Location = new System.Drawing.Point(91, 33);
			this._destinationName.Name = "_destinationName";
			this._destinationName.Size = new System.Drawing.Size(200, 21);
			this._destinationName.TabIndex = 4;
			this._destinationName.Validated += new System.EventHandler(this._destinationName_Validated);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(51, 33);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Name";
			// 
			// _editPluginSettings
			// 
			this._editPluginSettings.Location = new System.Drawing.Point(297, 58);
			this._editPluginSettings.Name = "_editPluginSettings";
			this._editPluginSettings.Size = new System.Drawing.Size(100, 23);
			this._editPluginSettings.TabIndex = 2;
			this._editPluginSettings.Text = "Edit settings...";
			this._editPluginSettings.UseVisualStyleBackColor = true;
			this._editPluginSettings.Click += new System.EventHandler(this._editPluginSettings_Click);
			// 
			// _plugin
			// 
			this._plugin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._plugin.FormattingEnabled = true;
			this._plugin.Location = new System.Drawing.Point(91, 60);
			this._plugin.Name = "_plugin";
			this._plugin.Size = new System.Drawing.Size(200, 21);
			this._plugin.TabIndex = 1;
			this._plugin.SelectionChangeCommitted += new System.EventHandler(this._plugin_SelectionChangeCommitted);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 63);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Deploy method";
			// 
			// DestinationsView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "DestinationsView";
			this.Size = new System.Drawing.Size(746, 472);
			this.Load += new System.EventHandler(this.DestinationsView_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button _addDestination;
		private System.Windows.Forms.Button _removeDestination;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListBox _destinations;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox _plugin;
		private System.Windows.Forms.Button _editPluginSettings;
		private System.Windows.Forms.TextBox _destinationName;
		private System.Windows.Forms.Label label2;
	}
}
