using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DeployerPlugins
{
	/// <summary>
	/// Summary description for FtpErrorForm.
	/// </summary>
	public class FtpErrorForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button _cancel;
		private System.Windows.Forms.Button _skip;
		private System.Windows.Forms.Button _retry;
		private System.Windows.Forms.TextBox _error;
		private System.Windows.Forms.TextBox _localfile;
		private System.Windows.Forms.TextBox _remotepath;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FtpErrorForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public string ErrorMessage {
			get { return _error.Text; }
			set { _error.Text = value; }
		}

		public string LocalFile {
			get { return _localfile.Text; }
			set { _localfile.Text = value; }
		}

		public string RemotePath {
			get { return _remotepath.Text; }
			set { _remotepath.Text = value; }
		}

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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._localfile = new System.Windows.Forms.TextBox();
			this._error = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this._remotepath = new System.Windows.Forms.TextBox();
			this._cancel = new System.Windows.Forms.Button();
			this._skip = new System.Windows.Forms.Button();
			this._retry = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this._localfile);
			this.groupBox1.Controls.Add(this._error);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this._remotepath);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(464, 120);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			// 
			// _localfile
			// 
			this._localfile.Location = new System.Drawing.Point(80, 48);
			this._localfile.Name = "_localfile";
			this._localfile.ReadOnly = true;
			this._localfile.Size = new System.Drawing.Size(376, 21);
			this._localfile.TabIndex = 1;
			// 
			// _error
			// 
			this._error.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._error.Location = new System.Drawing.Point(80, 16);
			this._error.Name = "_error";
			this._error.ReadOnly = true;
			this._error.Size = new System.Drawing.Size(376, 21);
			this._error.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Local File:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 23);
			this.label4.TabIndex = 1;
			this.label4.Text = "Error:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 80);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 23);
			this.label6.TabIndex = 1;
			this.label6.Text = "Remote Path:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// _remotepath
			// 
			this._remotepath.Location = new System.Drawing.Point(80, 80);
			this._remotepath.Name = "_remotepath";
			this._remotepath.ReadOnly = true;
			this._remotepath.Size = new System.Drawing.Size(376, 21);
			this._remotepath.TabIndex = 2;
			// 
			// _cancel
			// 
			this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._cancel.Location = new System.Drawing.Point(288, 136);
			this._cancel.Name = "_cancel";
			this._cancel.Size = new System.Drawing.Size(75, 23);
			this._cancel.TabIndex = 2;
			this._cancel.Text = "Cancel";
			this._cancel.Click += new System.EventHandler(this._cancel_Click);
			// 
			// _skip
			// 
			this._skip.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._skip.Location = new System.Drawing.Point(208, 136);
			this._skip.Name = "_skip";
			this._skip.Size = new System.Drawing.Size(75, 23);
			this._skip.TabIndex = 1;
			this._skip.Text = "Skip";
			this._skip.Click += new System.EventHandler(this._skip_Click);
			// 
			// _retry
			// 
			this._retry.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._retry.Location = new System.Drawing.Point(128, 136);
			this._retry.Name = "_retry";
			this._retry.Size = new System.Drawing.Size(75, 23);
			this._retry.TabIndex = 0;
			this._retry.Text = "Retry";
			this._retry.Click += new System.EventHandler(this._retry_Click);
			// 
			// FtpErrorForm
			// 
			this.AcceptButton = this._retry;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this._cancel;
			this.ClientSize = new System.Drawing.Size(482, 168);
			this.ControlBox = false;
			this.Controls.Add(this._cancel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this._skip);
			this.Controls.Add(this._retry);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FtpErrorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FTP Error";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		#region Event handlers

		private void _retry_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Retry;
			Close();
		}

		private void _skip_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Ignore;
			Close();
		}

		private void _cancel_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
			Close();
		}

		#endregion
	}
}
