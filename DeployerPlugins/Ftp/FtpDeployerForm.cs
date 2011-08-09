using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using EnterpriseDT.Net.Ftp;

namespace DeployerPlugins
{
	/// <summary>
	/// Summary description for FtpDeployerForm.
	/// </summary>
	internal class FtpDeployerForm : System.Windows.Forms.Form
	{
		private FtpDeployerSettings _settings;

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _address;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox _port;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox _path;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox _login;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox _password;
		private System.Windows.Forms.Button _cancel;
		private System.Windows.Forms.Button _ok;
		private Button _test;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FtpDeployerForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Gets or sets the plugin settings to configure.
		/// </summary>
		internal FtpDeployerSettings PluginSettings {
			get { return _settings; }
			set { _settings = value; }
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
			this.label1 = new System.Windows.Forms.Label();
			this._address = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this._port = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this._path = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this._login = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this._password = new System.Windows.Forms.TextBox();
			this._cancel = new System.Windows.Forms.Button();
			this._ok = new System.Windows.Forms.Button();
			this._test = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this._address);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this._port);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this._path);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this._login);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this._password);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(272, 157);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 23);
			this.label1.TabIndex = 9;
			this.label1.Text = "Address";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// _address
			// 
			this._address.Location = new System.Drawing.Point(88, 25);
			this._address.Name = "_address";
			this._address.Size = new System.Drawing.Size(168, 21);
			this._address.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 23);
			this.label2.TabIndex = 10;
			this.label2.Text = "Port";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// _port
			// 
			this._port.Location = new System.Drawing.Point(88, 49);
			this._port.Name = "_port";
			this._port.Size = new System.Drawing.Size(40, 21);
			this._port.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 73);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 23);
			this.label3.TabIndex = 11;
			this.label3.Text = "Remote path";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// _path
			// 
			this._path.Location = new System.Drawing.Point(88, 73);
			this._path.Name = "_path";
			this._path.Size = new System.Drawing.Size(168, 21);
			this._path.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 97);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 23);
			this.label4.TabIndex = 8;
			this.label4.Text = "Login";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// _login
			// 
			this._login.Location = new System.Drawing.Point(88, 97);
			this._login.Name = "_login";
			this._login.Size = new System.Drawing.Size(168, 21);
			this._login.TabIndex = 3;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 121);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 23);
			this.label5.TabIndex = 7;
			this.label5.Text = "Password";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// _password
			// 
			this._password.Location = new System.Drawing.Point(88, 121);
			this._password.Name = "_password";
			this._password.PasswordChar = '*';
			this._password.Size = new System.Drawing.Size(168, 21);
			this._password.TabIndex = 4;
			// 
			// _cancel
			// 
			this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._cancel.Location = new System.Drawing.Point(209, 171);
			this._cancel.Name = "_cancel";
			this._cancel.Size = new System.Drawing.Size(75, 23);
			this._cancel.TabIndex = 2;
			this._cancel.Text = "Cancel";
			// 
			// _ok
			// 
			this._ok.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._ok.Location = new System.Drawing.Point(128, 171);
			this._ok.Name = "_ok";
			this._ok.Size = new System.Drawing.Size(75, 23);
			this._ok.TabIndex = 1;
			this._ok.Text = "OK";
			this._ok.Click += new System.EventHandler(this._ok_Click);
			// 
			// _test
			// 
			this._test.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._test.Location = new System.Drawing.Point(8, 171);
			this._test.Name = "_test";
			this._test.Size = new System.Drawing.Size(75, 23);
			this._test.TabIndex = 12;
			this._test.Text = "Test";
			this._test.UseVisualStyleBackColor = true;
			this._test.Click += new System.EventHandler(this._test_Click);
			// 
			// FtpDeployerForm
			// 
			this.AcceptButton = this._ok;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this._cancel;
			this.ClientSize = new System.Drawing.Size(290, 204);
			this.ControlBox = false;
			this.Controls.Add(this._test);
			this.Controls.Add(this._cancel);
			this.Controls.Add(this._ok);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FtpDeployerForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FTP Settings";
			this.Load += new System.EventHandler(this.FtpDeployerForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		#region Event handlers

		private void FtpDeployerForm_Load(object sender, EventArgs e) {
			_address.Text = _settings.Address;
			_port.Text = _settings.Port.ToString();
			_path.Text = _settings.Path;
			_login.Text = _settings.Login;
			_password.Text = _settings.Password;
		}

		private void _ok_Click(object sender, EventArgs e) {
			// Copy settings
			_settings.Address = _address.Text;
			_settings.Port = int.Parse(_port.Text);
			_settings.Path = _path.Text;
			_settings.Login = _login.Text;
			_settings.Password = _password.Text;

			this.DialogResult = DialogResult.OK;
			Close();
		}

		private void _test_Click(object sender, EventArgs e) {
			try {
				this.Cursor = Cursors.WaitCursor;
				FTPClient ftp = new FTPClient(_address.Text, int.Parse(_port.Text));
				ftp.Login(_login.Text, _password.Text);
				ftp.Quit();

				MessageBox.Show("The connection was successfully established.", "Connection successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			this.Cursor = Cursors.Default;
		}

		#endregion
	}
}
