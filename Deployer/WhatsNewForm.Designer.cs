namespace Deployer {
	partial class WhatsNewForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WhatsNewForm));
			this._richtextbox = new System.Windows.Forms.RichTextBox();
			this._close = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _richtextbox
			// 
			this._richtextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._richtextbox.Location = new System.Drawing.Point(12, 12);
			this._richtextbox.Name = "_richtextbox";
			this._richtextbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this._richtextbox.Size = new System.Drawing.Size(385, 348);
			this._richtextbox.TabIndex = 0;
			this._richtextbox.Text = "";
			// 
			// _close
			// 
			this._close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._close.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._close.Location = new System.Drawing.Point(322, 366);
			this._close.Name = "_close";
			this._close.Size = new System.Drawing.Size(75, 23);
			this._close.TabIndex = 1;
			this._close.Text = "Close";
			this._close.UseVisualStyleBackColor = true;
			// 
			// WhatsNewForm
			// 
			this.AcceptButton = this._close;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(409, 401);
			this.Controls.Add(this._close);
			this.Controls.Add(this._richtextbox);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WhatsNewForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "What\'s New";
			this.Load += new System.EventHandler(this.WhatsNewForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox _richtextbox;
		private System.Windows.Forms.Button _close;
	}
}