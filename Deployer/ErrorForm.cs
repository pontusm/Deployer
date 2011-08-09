using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Deployer
{
	/// <summary>
	/// Summary description for ErrorForm.
	/// </summary>
	public class ErrorForm : System.Windows.Forms.Form
	{
		private bool _showdetails = false;
		private TextBox _errormessage;
		private Exception _exception;

		/// <summary> 
		/// Used for the different beep types 
		/// </summary> 
		private enum SoundType : int { 
			/// <summary> 
			/// The OK Beep 
			/// </summary> 
			MB_OK = 0x0000, 
			/// <summary> 
			/// The Icon Hand Beep 
			/// </summary> 
			MB_ICONHAND = 0x0010, 
			/// <summary> 
			/// The Question Beep 
			/// </summary> 
			MB_ICONQUESTION = 0x0020, 
			/// <summary> 
			/// The exclamation beep 
			/// </summary> 
			MB_ICONEXCLAMATION = 0x0030, 
			/// <summary> 
			/// the asterisk beep 
			/// </summary> 
			MB_ICONASTERISK = 0x0040 
		} 

		[DllImport("user32.dll")] 
		private static extern bool MessageBeep(SoundType soundtype);

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button _close;
		private System.Windows.Forms.Button _details;
		private System.Windows.Forms.TextBox _errordetails;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private ErrorForm()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this._close = new System.Windows.Forms.Button();
			this._details = new System.Windows.Forms.Button();
			this._errordetails = new System.Windows.Forms.TextBox();
			this._errormessage = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.pictureBox1);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(360, 80);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(72, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(280, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "An unexpected error has occured.";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(72, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(280, 32);
			this.label1.TabIndex = 0;
			this.label1.Text = "The operation you were trying to perform could not be carried out. Please see bel" +
				"ow for technical details.";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(56, 50);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// _close
			// 
			this._close.Location = new System.Drawing.Point(296, 96);
			this._close.Name = "_close";
			this._close.Size = new System.Drawing.Size(75, 23);
			this._close.TabIndex = 1;
			this._close.Text = "Close";
			this._close.Click += new System.EventHandler(this._close_Click);
			// 
			// _details
			// 
			this._details.Location = new System.Drawing.Point(216, 96);
			this._details.Name = "_details";
			this._details.Size = new System.Drawing.Size(75, 23);
			this._details.TabIndex = 2;
			this._details.Text = "Details >>";
			this._details.Click += new System.EventHandler(this._details_Click);
			// 
			// _errordetails
			// 
			this._errordetails.Location = new System.Drawing.Point(8, 217);
			this._errordetails.Multiline = true;
			this._errordetails.Name = "_errordetails";
			this._errordetails.ReadOnly = true;
			this._errordetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this._errordetails.Size = new System.Drawing.Size(360, 176);
			this._errordetails.TabIndex = 3;
			// 
			// _errormessage
			// 
			this._errormessage.Location = new System.Drawing.Point(8, 131);
			this._errormessage.Multiline = true;
			this._errormessage.Name = "_errormessage";
			this._errormessage.ReadOnly = true;
			this._errormessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this._errormessage.Size = new System.Drawing.Size(360, 80);
			this._errormessage.TabIndex = 3;
			// 
			// ErrorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(378, 405);
			this.Controls.Add(this._details);
			this.Controls.Add(this._close);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this._errormessage);
			this.Controls.Add(this._errordetails);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ErrorForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Unexpected error";
			this.Load += new System.EventHandler(this.ErrorForm_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		#region Event handlers

		private void _close_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void _details_Click(object sender, System.EventArgs e) {
			_showdetails = !_showdetails;
			UpdateUI();
		}

		private void ErrorForm_Load(object sender, EventArgs e) {
			_errormessage.Text = _exception.Message;
			_errordetails.Text = _exception.ToString();

			UpdateUI();

			MessageBeep(SoundType.MB_ICONEXCLAMATION);
		}

		#endregion

		/// <summary>
		/// Shows an "unknown error" dialog that lets the user view technical details about the error.
		/// </summary>
		public static DialogResult ShowErrorDialog(IWin32Window owner, Exception ex) {
			ErrorForm dlg = new ErrorForm();
			dlg._showdetails = false;
			dlg._exception = ex;
			return dlg.ShowDialog(owner);
		}

		/// <summary>
		/// Shows an "unknown error" dialog that lets the user view technical details about the error.
		/// </summary>
		public static DialogResult ShowErrorDialog(Exception ex) {
			ErrorForm dlg = new ErrorForm();
			dlg._showdetails = false;
			dlg._exception = ex;
			return dlg.ShowDialog();
		}
		
		private void UpdateUI() {
			if(!_showdetails) {
				this.Height = 160;
				_details.Text = "Details >>";
			}
			else {
				this.Height = 437;
				_details.Text = "<< Less";
			}
		}
	}
}
