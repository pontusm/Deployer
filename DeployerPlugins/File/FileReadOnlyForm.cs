using System;
using System.Windows.Forms;

namespace DeployerPlugins {
	public enum FileReadOnlyResult
	{
		Retry,
		Skip,
		SkipAll,
		Overwrite,
		OverwriteAll,
		Cancel
	}

	public partial class FileReadOnlyForm : Form {
		private FileReadOnlyResult _result = FileReadOnlyResult.Cancel;

		public FileReadOnlyForm() {
			InitializeComponent();
		}

		public FileReadOnlyResult Result {
			get {
				return _result;
			}
		}
		
		public string LocalFile {
			get { return _localfile.Text; }
			set { _localfile.Text = value; }
		}

		public string RemotePath {
			get { return _remotepath.Text; }
			set { _remotepath.Text = value; }
		}

		private void _retry_Click(object sender, EventArgs e) {
			_result = FileReadOnlyResult.Retry;
			this.Close();
		}

		private void _skip_Click(object sender, EventArgs e) {
			_result = FileReadOnlyResult.Skip;
			this.Close();
		}

		private void _overwrite_Click(object sender, EventArgs e) {
			_result = FileReadOnlyResult.Overwrite;
			this.Close();
		}

		private void _overwriteall_Click(object sender, EventArgs e) {
			_result = FileReadOnlyResult.OverwriteAll;
			this.Close();
		}

		private void _cancel_Click(object sender, EventArgs e) {
			_result = FileReadOnlyResult.Cancel;
			this.Close();
		}

		private void _skipall_Click(object sender, EventArgs e) {
			_result = FileReadOnlyResult.SkipAll;
			this.Close();
		}
	}
}