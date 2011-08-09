using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using DeployerPluginInterfaces;

namespace DeployerPlugins
{
	/// <summary>
	/// Deploys files via file copy.
	/// </summary>
	/// <remarks>
	/// 2005-08-25	POMU: Class Created
	/// </remarks>
	public class FileDeployer : DeployerPlugin, IFileDeployer, ITimestampControl
	{
		#region Private members
		private bool _overwriteall;
		private bool _skipall;
		private string _destinationPath;
		private string _realDestinationPath;


		#endregion

		#region Constructors
		public FileDeployer()
		{
		}
		#endregion

		#region Properties
		#endregion

		#region IDeployerPlugin interface

		/// <summary>
		/// An identifier for the plugin.
		/// </summary>
		public string PluginIdentifier {
			get { return "File"; }
		}

		/// <summary>
		/// A friendly name for the plugin that is shown to the user.
		/// </summary>
		public string Name {
			get { return "File copy"; }
		}

		/// <summary>
		/// Begins the deployment operation.
		/// </summary>
		public void DeployStart(IDeployerEventSink host) {
			// Clear flags
			_overwriteall = false;
			_skipall = false;

			// Verify that a destination has been set
			if(string.IsNullOrEmpty(_destinationPath))
			{
				var dialogResult = MessageBox.Show("No destination path has been setup. Would you like to set it now?", "File copy",
				                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if(dialogResult == DialogResult.Yes)
					ShowConfigureSettingsDialog(null);

				// If no path has been set here we will abort
				if(string.IsNullOrEmpty(_destinationPath))
					throw new DeployCancelException("No destination path configured.");
			}
		}

		/// <summary>
		/// Deploys a single file.
		/// </summary>
		/// <param name="localFilePath">The local path to the file.</param>
		/// <param name="remoteDirectoryPath">The remote directory to deploy the file to.</param>
		/// <param name="remoteFilename">The name that the file will have in the remote path.</param>
		public void DeployFile(string localFilePath, string remoteDirectoryPath, string remoteFilename) {
			string destpath = Path.Combine(_realDestinationPath, remoteDirectoryPath);
			string destfname = Path.Combine(destpath, remoteFilename);

			if(!Directory.Exists(destpath))
				Directory.CreateDirectory(destpath);

			bool retry;
			do {
				retry = false;			// Assume we won't retry operation

			
				// Check file flags if file exists
				if (File.Exists(destfname)) {
					FileInfo fi = new FileInfo(destfname);

					// Is file read only?
					if ((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) {
						// Skip all readonly files?
						if (_skipall)
							throw new DeploySkipException();

						bool overwrite = _overwriteall;		// Overwrite this file?

						// File is read only. If not overwriting all files, ask user what to do.
						if (!_overwriteall) {
							FileReadOnlyForm dlg = new FileReadOnlyForm();
							dlg.LocalFile = localFilePath;
							dlg.RemotePath = remoteDirectoryPath;
							dlg.ShowDialog();
							switch (dlg.Result) {
								case FileReadOnlyResult.Retry:
									retry = true;							// User tells us file is ok, so retry again
									break;
								case FileReadOnlyResult.SkipAll:
									_skipall = true;						// From now on, skip all readonly files
									goto case FileReadOnlyResult.Skip;
								case FileReadOnlyResult.Skip:
									throw new DeploySkipException();		// Skip file and just return
								case FileReadOnlyResult.OverwriteAll:
									_overwriteall = true;					// From now on, overwrite all readonly files
									goto case FileReadOnlyResult.Overwrite;
								case FileReadOnlyResult.Overwrite:
									overwrite = true;
									break;
								case FileReadOnlyResult.Cancel:
									throw new DeployCancelException("User cancelled.");
							}
						}
						
						if(overwrite)
							File.SetAttributes(destfname, fi.Attributes & ~FileAttributes.ReadOnly);
					}
				}
			} while (retry);

			File.Copy(localFilePath, destfname, true);

			// Clear read only flag on copied flag if necessary
			FileInfo fileinfo = new FileInfo(destfname);
			if ((fileinfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
				File.SetAttributes(destfname, fileinfo.Attributes & ~FileAttributes.ReadOnly);
		}

		/// <summary>
		/// Indicates that the deployment operation is completed.
		/// </summary>
		public void DeployEnd() {
		}

		/// <summary>
		/// Loads settings for the plugin.
		/// </summary>
		/// <param name="settings">A string containing the settings.</param>
		public void LoadSettings(string settings) {
			XmlDocument xd = new XmlDocument();
			xd.LoadXml(settings);
			_destinationPath = xd.DocumentElement.GetAttribute("DestinationPath");
			_realDestinationPath = EnvironmentVariableHelper.ResolveVariables(_destinationPath);
		}

		/// <summary>
		/// Save the settings for the plugin in a string.
		/// </summary>
		public string SaveSettings() {
			XmlDocument xd = new XmlDocument();
			xd.LoadXml("<Settings/>");
			xd.DocumentElement.SetAttribute("DestinationPath", _destinationPath);
			return xd.OuterXml;
		}
		
		/// <summary>
		/// Show the dialog used to configure the plugin settings. The current settings are passed in to
		/// the method, and should be updated with any changes that the user makes in the dialog.
		/// </summary>
		/// <param name="owner">The owner window to show the dialog in front of.</param>
		public DialogResult ShowConfigureSettingsDialog(IWin32Window owner) {
			FileDeployerForm dlg = new FileDeployerForm();
			dlg.DestinationPath = _destinationPath;
			DialogResult result = dlg.ShowDialog(owner);
			if (result == DialogResult.OK) {
				_destinationPath = dlg.DestinationPath;
				_realDestinationPath = EnvironmentVariableHelper.ResolveVariables(_destinationPath);
			}

			return result;
		}

		#endregion

		#region ITimestampControl interface

		/// <summary>
		/// Attempts to connect to the remote server.
		/// </summary>
		public void Connect() {
		}

		/// <summary>
		/// Disconnects from the remote server.
		/// </summary>
		public void Disconnect() {
		}

		/// <summary>
		/// Retrieves the timestamp file from the remote server and stores it locally.
		/// </summary>
		/// <param name="localfilepath">The name and path of the local file.</param>
		/// <param name="remotefilename">The name of the remote timestamp file.</param>
		public bool DownloadTimestampFile(string localfilepath, string remotefilename) {
			string remotepath = Path.Combine(_realDestinationPath, remotefilename);
			if (!File.Exists(remotepath))
				return false;

			try {
				File.Copy(remotepath, localfilepath, true);
			}
			catch (IOException ex) {
				throw new IOException(string.Format("Failed to download timestamp file '{0}'. {1}", remotepath, ex.Message), ex);
			}
			return true;
		}

		/// <summary>
		/// Uploads the timestamp file to the remote server.
		/// </summary>
		/// <param name="localfilepath">The name and path of the local file.</param>
		/// <param name="remotefilename">The name of the remote timestamp file.</param>
		public void UploadTimestampFile(string localfilepath, string remotefilename) {
			string remotepath = Path.Combine(_realDestinationPath, remotefilename);
			try {
				File.Copy(localfilepath, remotepath, true);
			}
			catch (IOException ex) {
				throw new IOException(string.Format("Failed to upload timestamp file '{0}'. {1}", remotepath, ex.Message), ex);
			}
		}

		#endregion
	}
}
