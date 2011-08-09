using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DeployerPlugins
{
	/// <summary>
	/// Summary description for FileDeployerForm.
	/// </summary>
	public class FileDeployerForm : System.Windows.Forms.Form
	{
		private string _destinationPath;

		private System.Windows.Forms.Button _cancel;
		private System.Windows.Forms.Button _ok;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _destpath;
		private System.Windows.Forms.Button _browse;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FileDeployerForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public string DestinationPath {
			get { return _destinationPath; }
			set { _destinationPath = value; }
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
			this._cancel = new System.Windows.Forms.Button();
			this._ok = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this._destpath = new System.Windows.Forms.TextBox();
			this._browse = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _cancel
			// 
			this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._cancel.Location = new System.Drawing.Point(311, 80);
			this._cancel.Name = "_cancel";
			this._cancel.TabIndex = 7;
			this._cancel.Text = "Cancel";
			// 
			// _ok
			// 
			this._ok.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._ok.Location = new System.Drawing.Point(231, 80);
			this._ok.Name = "_ok";
			this._ok.TabIndex = 6;
			this._ok.Text = "OK";
			this._ok.Click += new System.EventHandler(this._ok_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this._browse);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this._destpath);
			this.groupBox1.Location = new System.Drawing.Point(7, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(376, 64);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Destination path";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// _destpath
			// 
			this._destpath.Location = new System.Drawing.Point(104, 24);
			this._destpath.Name = "_destpath";
			this._destpath.Size = new System.Drawing.Size(192, 21);
			this._destpath.TabIndex = 0;
			this._destpath.Text = "";
			// 
			// _browse
			// 
			this._browse.Location = new System.Drawing.Point(304, 24);
			this._browse.Name = "_browse";
			this._browse.Size = new System.Drawing.Size(64, 23);
			this._browse.TabIndex = 2;
			this._browse.Text = "Browse...";
			this._browse.Click += new System.EventHandler(this._browse_Click);
			// 
			// FileDeployerForm
			// 
			this.AcceptButton = this._ok;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this._cancel;
			this.ClientSize = new System.Drawing.Size(392, 110);
			this.ControlBox = false;
			this.Controls.Add(this._cancel);
			this.Controls.Add(this._ok);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FileDeployerForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "File Copy Settings";
			this.Load += new System.EventHandler(this.FileDeployerForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FileDeployerForm_Load(object sender, System.EventArgs e) {
			_destpath.Text = _destinationPath;
		}

		private void _browse_Click(object sender, System.EventArgs e) {
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.Description = "Please select a directory";
			if(_destpath.Text.Length > 0)
				dlg.SelectedPath = _destpath.Text;
			
			if(dlg.ShowDialog(this) == DialogResult.Cancel)
				return;

			_destpath.Text = dlg.SelectedPath;
		}

		private void _ok_Click(object sender, System.EventArgs e) {
			_destinationPath = _destpath.Text;

			this.DialogResult = DialogResult.OK;
		}

	}
}
