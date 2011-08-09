using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Deployer {
	public partial class ProgressForm : Form {
		private bool _isCancelled = false;
		
		public ProgressForm() {
			InitializeComponent();
			
			PersistWindowState.HandlePersistance(this, RegistryHandler.RegistryPath, false);
		}

		/// <summary>
		/// If this is set, the user wants to cancel the current process.
		/// </summary>
		public bool IsCancelled {
			get { return _isCancelled; }
			set { _isCancelled = value; }
		}

		public ProgressBar ItemProgress {
			get { return _itemprogress; }
		}

		public ProgressBar TotalProgress {
			get { return _totalprogress; }
		}

		public string ItemText {
			get { return _itemlabel.Text; }
			set { _itemlabel.Text = value; }
		}

		public string TotalText {
			get {
				return _totallabel.Text;
			}
			set {
				_totallabel.Text = value;
			}
		}

		public void ResetDialog(string title) {
			ItemText = null;
			TotalText = null;
			
			_itemprogress.Value = 0;
			_totalprogress.Value = 0;

			Text = title;

			_isCancelled = false;
			_cancel.Enabled = true;
		}

		private void _cancel_Click(object sender, EventArgs e) {
			_isCancelled = true;
			_cancel.Enabled = false;
		}

	}
}