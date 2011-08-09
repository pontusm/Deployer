using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Deployer {
	public partial class AboutForm : Form {
		public AboutForm() {
			InitializeComponent();
		}

		private void AboutForm_Load(object sender, EventArgs e) {
			_apptitle.Text = string.Format("Deployer v{0}", Application.ProductVersion);
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start("http://www.stendahls.net");
		}
	}
}