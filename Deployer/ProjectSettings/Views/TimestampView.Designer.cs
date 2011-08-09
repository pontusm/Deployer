namespace Deployer.ProjectSettings.Views {
	partial class TimestampView {
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
			this.groupBox10 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this._timestampDestination = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this._enableTimestamping = new System.Windows.Forms.CheckBox();
			this.groupBox10.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox10
			// 
			this.groupBox10.Controls.Add(this._enableTimestamping);
			this.groupBox10.Controls.Add(this.label1);
			this.groupBox10.Controls.Add(this._timestampDestination);
			this.groupBox10.Controls.Add(this.label2);
			this.groupBox10.Controls.Add(this.label10);
			this.groupBox10.Location = new System.Drawing.Point(3, 3);
			this.groupBox10.Name = "groupBox10";
			this.groupBox10.Size = new System.Drawing.Size(464, 131);
			this.groupBox10.TabIndex = 1;
			this.groupBox10.TabStop = false;
			this.groupBox10.Text = "Timestamp";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(449, 41);
			this.label1.TabIndex = 2;
			this.label1.Text = "Timestamping is used to record when someone last deployed a project. The timestam" +
				"p file will be placed in the destination specified below.";
			// 
			// _timestampDestination
			// 
			this._timestampDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._timestampDestination.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._timestampDestination.FormattingEnabled = true;
			this._timestampDestination.Location = new System.Drawing.Point(128, 81);
			this._timestampDestination.Name = "_timestampDestination";
			this._timestampDestination.Size = new System.Drawing.Size(121, 21);
			this._timestampDestination.TabIndex = 1;
			this._timestampDestination.SelectedIndexChanged += new System.EventHandler(this._timestampDestination_SelectedIndexChanged);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(8, 84);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(112, 13);
			this.label10.TabIndex = 0;
			this.label10.Text = "Timestamp destination";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 58);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Enable timestamping";
			// 
			// _enableTimestamping
			// 
			this._enableTimestamping.AutoSize = true;
			this._enableTimestamping.Location = new System.Drawing.Point(128, 58);
			this._enableTimestamping.Name = "_enableTimestamping";
			this._enableTimestamping.Size = new System.Drawing.Size(15, 14);
			this._enableTimestamping.TabIndex = 3;
			this._enableTimestamping.UseVisualStyleBackColor = true;
			this._enableTimestamping.CheckedChanged += new System.EventHandler(this._enableTimestamping_CheckedChanged);
			// 
			// TimestampView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox10);
			this.Name = "TimestampView";
			this.Size = new System.Drawing.Size(746, 472);
			this.Load += new System.EventHandler(this.TimestampView_Load);
			this.groupBox10.ResumeLayout(false);
			this.groupBox10.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox10;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox _timestampDestination;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.CheckBox _enableTimestamping;
		private System.Windows.Forms.Label label2;
	}
}
