using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DeployerEngine
{
	/// <summary>
	/// Summary description for DeployErrorForm.
	/// </summary>
	public class DeployErrorForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button _cancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label _locallabel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label _remotelabel;
		private System.Windows.Forms.Button _skip;
		private System.Windows.Forms.Button _retry;
		private System.Windows.Forms.TextBox _error;
		private System.Windows.Forms.TextBox _localtext;
		private System.Windows.Forms.TextBox _remotetext;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DeployErrorForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		#region Properties

		public string ErrorMessage {
			get { return _error.Text; }
			set { _error.Text = value; }
		}

		#endregion

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
			this._cancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._localtext = new System.Windows.Forms.TextBox();
			this._error = new System.Windows.Forms.TextBox();
			this._locallabel = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this._remotelabel = new System.Windows.Forms.Label();
			this._remotetext = new System.Windows.Forms.TextBox();
			this._skip = new System.Windows.Forms.Button();
			this._retry = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _cancel
			// 
			this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancel.Location = new System.Drawing.Point(336, 152);
			this._cancel.Name = "_cancel";
			this._cancel.Size = new System.Drawing.Size(75, 23);
			this._cancel.TabIndex = 2;
			this._cancel.Text = "Cancel";
			this._cancel.Click += new System.EventHandler(this._cancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this._localtext);
			this.groupBox1.Controls.Add(this._error);
			this.groupBox1.Controls.Add(this._locallabel);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this._remotelabel);
			this.groupBox1.Controls.Add(this._remotetext);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(552, 136);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			// 
			// _localtext
			// 
			this._localtext.Location = new System.Drawing.Point(80, 72);
			this._localtext.Name = "_localtext";
			this._localtext.ReadOnly = true;
			this._localtext.Size = new System.Drawing.Size(464, 21);
			this._localtext.TabIndex = 1;
			// 
			// _error
			// 
			this._error.AcceptsReturn = true;
			this._error.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._error.Location = new System.Drawing.Point(80, 16);
			this._error.Multiline = true;
			this._error.Name = "_error";
			this._error.ReadOnly = true;
			this._error.Size = new System.Drawing.Size(464, 48);
			this._error.TabIndex = 0;
			// 
			// _locallabel
			// 
			this._locallabel.Location = new System.Drawing.Point(0, 72);
			this._locallabel.Name = "_locallabel";
			this._locallabel.Size = new System.Drawing.Size(80, 23);
			this._locallabel.TabIndex = 1;
			this._locallabel.Text = "local";
			this._locallabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
			// _remotelabel
			// 
			this._remotelabel.Location = new System.Drawing.Point(0, 104);
			this._remotelabel.Name = "_remotelabel";
			this._remotelabel.Size = new System.Drawing.Size(80, 23);
			this._remotelabel.TabIndex = 1;
			this._remotelabel.Text = "remote";
			this._remotelabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// _remotetext
			// 
			this._remotetext.Location = new System.Drawing.Point(80, 104);
			this._remotetext.Name = "_remotetext";
			this._remotetext.ReadOnly = true;
			this._remotetext.Size = new System.Drawing.Size(464, 21);
			this._remotetext.TabIndex = 2;
			// 
			// _skip
			// 
			this._skip.Location = new System.Drawing.Point(256, 152);
			this._skip.Name = "_skip";
			this._skip.Size = new System.Drawing.Size(75, 23);
			this._skip.TabIndex = 1;
			this._skip.Text = "Skip";
			this._skip.Click += new System.EventHandler(this._skip_Click);
			// 
			// _retry
			// 
			this._retry.Location = new System.Drawing.Point(176, 152);
			this._retry.Name = "_retry";
			this._retry.Size = new System.Drawing.Size(75, 23);
			this._retry.TabIndex = 0;
			this._retry.Text = "Retry";
			this._retry.Click += new System.EventHandler(this._retry_Click);
			// 
			// DeployErrorForm
			// 
			this.AcceptButton = this._retry;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this._cancel;
			this.ClientSize = new System.Drawing.Size(570, 184);
			this.ControlBox = false;
			this.Controls.Add(this._cancel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this._skip);
			this.Controls.Add(this._retry);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "DeployErrorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Deploy Error";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		#region Setup text fields

		public void SetLocalInfo(string label, string text) {
			_locallabel.Text = label;
			_localtext.Text = text;
		}
		
		public void SetRemoteInfo(string label, string text) {
			_remotelabel.Text = label;
			_remotetext.Text = text;
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
