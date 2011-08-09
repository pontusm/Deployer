using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Deployer
{
	/// <summary>
	/// Summary description for LastModifiedForm.
	/// </summary>
	public class LastModifiedForm : Form
	{
		private GroupBox groupBox1;
		private Label label1;
		private DateTimePicker _modifiedSince;
		private Button _cancel;
		private Button _ok;

		private DateTime _selectedDate = DateTime.Now;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public LastModifiedForm()
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
		/// Gets or sets the date to show.
		/// </summary>
		public DateTime SelectedDate {
			get { return _selectedDate; }
			set { _selectedDate = value; }
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
			this._modifiedSince = new System.Windows.Forms.DateTimePicker();
			this._cancel = new System.Windows.Forms.Button();
			this._ok = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this._modifiedSince);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(224, 96);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(208, 32);
			this.label1.TabIndex = 0;
			this.label1.Text = "Find files that have been modified since (yyyy-mm-dd hh:mm:ss)";
			// 
			// _modifiedSince
			// 
			this._modifiedSince.CustomFormat = "yyyy-MM-dd HH:mm:ss";
			this._modifiedSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this._modifiedSince.Location = new System.Drawing.Point(8, 56);
			this._modifiedSince.Name = "_modifiedSince";
			this._modifiedSince.TabIndex = 0;
			// 
			// _cancel
			// 
			this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancel.Location = new System.Drawing.Point(160, 112);
			this._cancel.Name = "_cancel";
			this._cancel.TabIndex = 2;
			this._cancel.Text = "Cancel";
			// 
			// _ok
			// 
			this._ok.Location = new System.Drawing.Point(80, 112);
			this._ok.Name = "_ok";
			this._ok.TabIndex = 1;
			this._ok.Text = "OK";
			this._ok.Click += new System.EventHandler(this._ok_Click);
			// 
			// LastModifiedForm
			// 
			this.AcceptButton = this._ok;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this._cancel;
			this.ClientSize = new System.Drawing.Size(242, 144);
			this.Controls.Add(this._cancel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this._ok);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LastModifiedForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Scan last modified files";
			this.Load += new System.EventHandler(this.LastModifiedForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Event handlers

		private void _ok_Click(object sender, EventArgs e) {
			_selectedDate = _modifiedSince.Value;
			this.DialogResult = DialogResult.OK;
			Close();
		}

		private void LastModifiedForm_Load(object sender, EventArgs e) {
			_modifiedSince.Value = _selectedDate;
		}
		#endregion

	}
}
