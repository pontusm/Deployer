namespace DeployerPlugins {
	partial class FileReadOnlyForm {
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
			this._cancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this._localfile = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this._remotepath = new System.Windows.Forms.TextBox();
			this._skip = new System.Windows.Forms.Button();
			this._retry = new System.Windows.Forms.Button();
			this._overwrite = new System.Windows.Forms.Button();
			this._overwriteall = new System.Windows.Forms.Button();
			this._skipall = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _cancel
			// 
			this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._cancel.Location = new System.Drawing.Point(417, 136);
			this._cancel.Name = "_cancel";
			this._cancel.Size = new System.Drawing.Size(75, 33);
			this._cancel.TabIndex = 6;
			this._cancel.Text = "Cancel";
			this._cancel.Click += new System.EventHandler(this._cancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this._localfile);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this._remotepath);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(480, 115);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(7, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(410, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "File already exists and is marked as read only. What do you want to do?";
			// 
			// _localfile
			// 
			this._localfile.Location = new System.Drawing.Point(94, 47);
			this._localfile.Name = "_localfile";
			this._localfile.ReadOnly = true;
			this._localfile.Size = new System.Drawing.Size(368, 21);
			this._localfile.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Local File:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6, 77);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(82, 23);
			this.label6.TabIndex = 1;
			this.label6.Text = "Remote Path:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// _remotepath
			// 
			this._remotepath.Location = new System.Drawing.Point(94, 74);
			this._remotepath.Name = "_remotepath";
			this._remotepath.ReadOnly = true;
			this._remotepath.Size = new System.Drawing.Size(368, 21);
			this._remotepath.TabIndex = 2;
			// 
			// _skip
			// 
			this._skip.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._skip.Location = new System.Drawing.Point(93, 136);
			this._skip.Name = "_skip";
			this._skip.Size = new System.Drawing.Size(75, 33);
			this._skip.TabIndex = 5;
			this._skip.Text = "Skip";
			this._skip.Click += new System.EventHandler(this._skip_Click);
			// 
			// _retry
			// 
			this._retry.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._retry.Location = new System.Drawing.Point(12, 136);
			this._retry.Name = "_retry";
			this._retry.Size = new System.Drawing.Size(75, 33);
			this._retry.TabIndex = 4;
			this._retry.Text = "Retry";
			this._retry.Click += new System.EventHandler(this._retry_Click);
			// 
			// _overwrite
			// 
			this._overwrite.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._overwrite.Location = new System.Drawing.Point(255, 136);
			this._overwrite.Name = "_overwrite";
			this._overwrite.Size = new System.Drawing.Size(75, 33);
			this._overwrite.TabIndex = 8;
			this._overwrite.Text = "Overwrite";
			this._overwrite.UseVisualStyleBackColor = true;
			this._overwrite.Click += new System.EventHandler(this._overwrite_Click);
			// 
			// _overwriteall
			// 
			this._overwriteall.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._overwriteall.Location = new System.Drawing.Point(336, 136);
			this._overwriteall.Name = "_overwriteall";
			this._overwriteall.Size = new System.Drawing.Size(75, 33);
			this._overwriteall.TabIndex = 8;
			this._overwriteall.Text = "Overwrite All";
			this._overwriteall.UseVisualStyleBackColor = true;
			this._overwriteall.Click += new System.EventHandler(this._overwriteall_Click);
			// 
			// _skipall
			// 
			this._skipall.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._skipall.Location = new System.Drawing.Point(174, 136);
			this._skipall.Name = "_skipall";
			this._skipall.Size = new System.Drawing.Size(75, 33);
			this._skipall.TabIndex = 5;
			this._skipall.Text = "Skip All";
			this._skipall.Click += new System.EventHandler(this._skipall_Click);
			// 
			// FileReadOnlyForm
			// 
			this.AcceptButton = this._retry;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._cancel;
			this.ClientSize = new System.Drawing.Size(502, 181);
			this.ControlBox = false;
			this.Controls.Add(this._overwriteall);
			this.Controls.Add(this._overwrite);
			this.Controls.Add(this._cancel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this._skipall);
			this.Controls.Add(this._skip);
			this.Controls.Add(this._retry);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FileReadOnlyForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "File copy error";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button _cancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox _localfile;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox _remotepath;
		private System.Windows.Forms.Button _skip;
		private System.Windows.Forms.Button _retry;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button _overwrite;
		private System.Windows.Forms.Button _overwriteall;
		private System.Windows.Forms.Button _skipall;
	}
}