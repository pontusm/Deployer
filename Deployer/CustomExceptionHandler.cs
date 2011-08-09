using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Deployer {
	/// <summary>
	/// Handles untrapped exceptions
	/// </summary>
	public class CustomExceptionHandler {

		/// <summary>
		/// Traps all unhandled exceptions for an application
		/// </summary>
		public static void HandleApplicationExceptions() {
			CustomExceptionHandler eh = new CustomExceptionHandler();
			Application.ThreadException += new ThreadExceptionEventHandler(eh.OnThreadException);
		}

		// Handles the exception event.
		private void OnThreadException(object sender, ThreadExceptionEventArgs t) {
			DialogResult result = DialogResult.Cancel;
			try {
				result = this.ShowThreadExceptionDialog(t.Exception);
			}
			catch(Exception ex) {
				try {
					string err = string.Format("Fatal error. The application needs to be closed.\n\nError: " + ex.Message);
					MessageBox.Show(err, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
				finally {
					Application.Exit();
				}
			}

			// Exits the program when the user clicks Abort.
			if (result == DialogResult.Abort)
				Application.Exit();
		}

		// Creates the error message and displays it.
		private DialogResult ShowThreadExceptionDialog(Exception e) {
			Debug.WriteLine(e.ToString());
			//string errorMsg = "An error occurred please contact the adminstrator with the following information (press CTRL-C to copy to clipboard):";
			//errorMsg = String.Format("{0}\n\nError: {1}\n\nException type: {2}\nSource: {3}\nVersion: {4}\n\nStack:\n{5}", errorMsg, e.Message, e.GetType().FullName, e.Source, Application.ProductVersion, e.StackTrace);
			//return MessageBox.Show(errorMsg, "LinkMaster - Fatal Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
			if (e is ApplicationException)
				return MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			else
				return ErrorForm.ShowErrorDialog(e);
		}
	}
}
