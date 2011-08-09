namespace Deployer {
	partial class ProgressForm {
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
			System.Windows.Forms.GroupBox groupBox1;
			this._totalprogress = new System.Windows.Forms.ProgressBar();
			this._itemprogress = new System.Windows.Forms.ProgressBar();
			this._totallabel = new System.Windows.Forms.Label();
			this._itemlabel = new System.Windows.Forms.Label();
			this._cancel = new System.Windows.Forms.Button();
			groupBox1 = new System.Windows.Forms.GroupBox();
			groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(this._totalprogress);
			groupBox1.Controls.Add(this._itemprogress);
			groupBox1.Controls.Add(this._totallabel);
			groupBox1.Controls.Add(this._itemlabel);
			groupBox1.Location = new System.Drawing.Point(12, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(553, 127);
			groupBox1.TabIndex = 3;
			groupBox1.TabStop = false;
			groupBox1.Text = "Progress";
			// 
			// _totalprogress
			// 
			this._totalprogress.Location = new System.Drawing.Point(6, 87);
			this._totalprogress.Name = "_totalprogress";
			this._totalprogress.Size = new System.Drawing.Size(541, 22);
			this._totalprogress.TabIndex = 0;
			// 
			// _itemprogress
			// 
			this._itemprogress.Location = new System.Drawing.Point(6, 33);
			this._itemprogress.Name = "_itemprogress";
			this._itemprogress.Size = new System.Drawing.Size(541, 22);
			this._itemprogress.TabIndex = 0;
			// 
			// _totallabel
			// 
			this._totallabel.Location = new System.Drawing.Point(6, 71);
			this._totallabel.Name = "_totallabel";
			this._totallabel.Size = new System.Drawing.Size(541, 13);
			this._totallabel.TabIndex = 1;
			this._totallabel.Text = "Total files: 2/15";
			// 
			// _itemlabel
			// 
			this._itemlabel.Location = new System.Drawing.Point(6, 17);
			this._itemlabel.Name = "_itemlabel";
			this._itemlabel.Size = new System.Drawing.Size(541, 13);
			this._itemlabel.TabIndex = 1;
			this._itemlabel.Text = "Transfering: c.\\test.txt (45%)";
			// 
			// _cancel
			// 
			this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancel.Location = new System.Drawing.Point(490, 145);
			this._cancel.Name = "_cancel";
			this._cancel.Size = new System.Drawing.Size(75, 23);
			this._cancel.TabIndex = 2;
			this._cancel.Text = "Cancel";
			this._cancel.UseVisualStyleBackColor = true;
			this._cancel.Click += new System.EventHandler(this._cancel_Click);
			// 
			// ProgressForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._cancel;
			this.ClientSize = new System.Drawing.Size(579, 180);
			this.ControlBox = false;
			this.Controls.Add(groupBox1);
			this.Controls.Add(this._cancel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ProgressForm";
			this.Opacity = 0.9;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ProgressForm";
			groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ProgressBar _itemprogress;
		private System.Windows.Forms.ProgressBar _totalprogress;
		private System.Windows.Forms.Label _itemlabel;
		private System.Windows.Forms.Label _totallabel;
		private System.Windows.Forms.Button _cancel;

	}
}