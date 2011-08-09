using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using DeployerPluginInterfaces;
using EnterpriseDT.Net.Ftp;

namespace DeployerPlugins
{
	/// <summary>
	/// Deploys files over FTP.
	/// </summary>
	/// <remarks>
	/// 2005-01-14	POMU: Class Created
	/// </remarks>
	public class FtpDeployer : DeployerPlugin, IFileDeployer, ITimestampControl
	{
		#region Private members
		private FtpDeployerSettings _settings = new FtpDeployerSettings();
		private FTPClient _ftp;
		private string _currentRemotePath;
		private string _remoteRootPath;
		private IDeployerEventSink _host;
		private DeployerPluginInterfaces.TransferEventArgs _currentTransferEventArgs;

		#endregion

		#region Constructors
		public FtpDeployer()
		{
		}
		#endregion

		#region IDeployerPlugin interface

		/// <summary>
		/// An identifier for the plugin.
		/// </summary>
		public string PluginIdentifier {
			get { return "FTP"; }
		}

		/// <summary>
		/// A friendly name for the plugin that is shown to the user.
		/// </summary>
		public string Name {
			get { return "FTP Transfer"; }
		}

		/// <summary>
		/// Begins the deployment operation.
		/// </summary>
		public void DeployStart(IDeployerEventSink host) {
			_host = host;

			Connect();
		}

		/// <summary>
		/// Deploys a single file.
		/// </summary>
		/// <param name="localFilePath">The local path to the file.</param>
		/// <param name="remoteDirectoryPath">The remote directory to deploy the file to.</param>
		/// <param name="remoteFilename">The name that the file will have in the remote path.</param>
		public void DeployFile(string localFilePath, string remoteDirectoryPath, string remoteFilename) {
			bool retry;

			// Retry loop
			do {
				retry = false;			// Assume we won't retry
				try {
					// Verify remote path
					if(_currentRemotePath != remoteDirectoryPath) {
						ChangeRemoteDirectory(Path.Combine(_remoteRootPath, remoteDirectoryPath));		// Will create the directory if not existing
						_currentRemotePath = remoteDirectoryPath;
					}

					// Deploy file
					_currentTransferEventArgs = new DeployerPluginInterfaces.TransferEventArgs(localFilePath);
					//string fname = Path.GetFileName(localFilePath);
					_ftp.Put(localFilePath, remoteFilename);
				}
				catch (Exception ex) {
					FtpErrorForm dlg = new FtpErrorForm();
					dlg.ErrorMessage = ex.Message;
					dlg.LocalFile = localFilePath;
					dlg.RemotePath = remoteDirectoryPath;
					DialogResult result = dlg.ShowDialog();
					switch(result) {
						case DialogResult.Retry:
							retry = true;
							break;
						case DialogResult.Ignore:
							break;
						case DialogResult.Cancel:
							throw new DeployCancelException("User cancelled.");
					}
				}
			} while (retry);
		}

		/// <summary>
		/// Indicates that the deployment operation is completed.
		/// </summary>
		public void DeployEnd() {
			Disconnect();

			_host = null;
		}

		/// <summary>
		/// Loads settings for the plugin.
		/// </summary>
		/// <param name="settings">A string containing the settings.</param>
		public void LoadSettings(string settings) {
			XmlDocument xd = new XmlDocument();
			xd.LoadXml(settings);
			_settings.LoadFromXml(xd.DocumentElement);
		}

		/// <summary>
		/// Save the settings for the plugin in a string.
		/// </summary>
		public string SaveSettings() {
			XmlDocument xd = new XmlDocument();
			xd.LoadXml("<Settings/>");
			_settings.SaveToXml(xd.DocumentElement);
			return xd.OuterXml;
		}
		
		/// <summary>
		/// Show the dialog used to configure the plugin settings.
		/// </summary>
		/// <param name="owner">The owner window to show the dialog in front of.</param>
		public DialogResult ShowConfigureSettingsDialog(IWin32Window owner) {
			FtpDeployerForm dlg = new FtpDeployerForm();
			dlg.PluginSettings = _settings;
			return dlg.ShowDialog(owner);
		}

		#endregion

		#region ITimestampControl interface

		/// <summary>
		/// Attempts to connect to the remote location.
		/// </summary>
		public void Connect() {
			// Init FTP client
			_ftp = new FTPClient(_settings.Address, _settings.Port);
			_ftp.CommandSent += new FTPMessageHandler(Ftp_CommandSent);
			_ftp.ReplyReceived += new FTPMessageHandler(Ftp_ReplyReceived);
			_ftp.BytesTransferred += new BytesTransferredHandler(Ftp_BytesTransferred);
			_ftp.Login(_settings.Login, _settings.Password);
			_ftp.TransferType = FTPTransferType.BINARY;

			// Setup root deploy path
			string rootpath = _ftp.Pwd();
			_remoteRootPath = Path.Combine(rootpath, _settings.Path.TrimStart('\\', '/'));

			_currentRemotePath = null;
		}

		/// <summary>
		/// Disconnects from the remote location.
		/// </summary>
		public void Disconnect() {
			_ftp.Quit();
			_ftp = null;
		}
		
		/// <summary>
		/// Retrieves the timestamp file from the remote location and stores it locally.
		/// </summary>
		/// <param name="localfilepath">The name and path of the local file.</param>
		public bool DownloadTimestampFile(string localfilepath, string remotefilename) {
			try {
				string remotefilepath = GetTimestampFilePath(remotefilename);
				_ftp.Get(localfilepath, remotefilepath);
				return true;
			}
			catch (FTPException) {
				return false;
			}
		}

		/// <summary>
		/// Uploads the timestamp file to the remote server.
		/// </summary>
		/// <param name="localfilepath">The name and path of the local file.</param>
		public void UploadTimestampFile(string localfilepath, string remotefilename) {
			try
			{
				string remotefilepath = GetTimestampFilePath(remotefilename);
				string remotepath = FixRemotePath(Path.GetDirectoryName(remotefilepath));
				_ftp.ChDir(remotepath);
				_ftp.Put(localfilepath, Path.GetFileName(remotefilename));
			}
			catch (FTPException e)
			{
				throw new InvalidOperationException("Failed to update timestamp file. " + e.Message);
			}
		}

		#endregion
		
		#region Private methods

		/// <summary>
		/// Tries to change the current remote directory.
		/// </summary>
		/// <param name="remotePath"></param>
		private void ChangeRemoteDirectory(string remotePath) {
			try {
				remotePath = FixRemotePath(remotePath);
				_ftp.ChDir(remotePath);
			}
			catch(FTPException ex) {
				// Directory not found?
				if(ex.ReplyCode == 550) {
					try {
						// Jump to the parent dir
						string parentDirPath = GetParentDirectoryPath(remotePath);
						ChangeRemoteDirectory(parentDirPath);

						// Try to create the dir
						string dirName = GetDirectoryName(remotePath);
						_ftp.MkDir(dirName);

						// Now change to the new dir that we created
						_ftp.ChDir(remotePath);
					}
					catch (Exception ex2) {
						throw new ApplicationException(string.Format("Unable to create remote directory \"{0}\".", remotePath), ex2);
					}
				} else {
					throw new ApplicationException(string.Format("Unable to change remote directory to \"{0}\".", remotePath), ex);
				}
			}
		}

		/// <summary>
		/// Format the remote path according to unix style.
		/// </summary>
		/// <param name="remotePath"></param>
		/// <returns></returns>
		private string FixRemotePath(string remotePath) {
			remotePath = remotePath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);		// Remove trailing slash
			return remotePath.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
		}

		/// <summary>
		/// Retrieves the full path and filename of the timestamp file.
		/// </summary>
		/// <returns></returns>
		private string GetTimestampFilePath(string remotefilename) {
			string path = Path.Combine(_remoteRootPath, remotefilename);
			return path;
		}

		/// <summary>
		/// Gets the path to the parent directory for the specified remote path.
		/// </summary>
		/// <param name="remotePath"></param>
		/// <returns></returns>
		private string GetParentDirectoryPath(string remotePath) {
			remotePath = remotePath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);		// Remove trailing slash
			int pos = remotePath.LastIndexOfAny(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
			if(pos == -1)
				return "/";

			return remotePath.Substring(0, pos + 1);
		}

		/// <summary>
		/// Gets the name of the directory for the specified remote path.
		/// </summary>
		/// <param name="remotePath"></param>
		/// <returns></returns>
		private string GetDirectoryName(string remotePath) {
			if(remotePath.Length > 1)
				remotePath = remotePath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);		// Remove any trailing slash
			int pos = remotePath.LastIndexOfAny(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
			if(pos == -1)
				return remotePath;

			return remotePath.Substring(pos + 1);
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Occurs during transfer of a file.
		/// </summary>
		/// <param name="ftpClient"></param>
		/// <param name="bytesTransferred"></param>
		private void Ftp_BytesTransferred(object ftpClient, BytesTransferredEventArgs bytesTransferred) {
			if (_host != null) {
				_currentTransferEventArgs.BytesSent = bytesTransferred.ByteCount;
				_host.OnTransferProgress(this, _currentTransferEventArgs);
			}
		}

		/// <summary>
		/// Occurs when a command has been sent to the FTP server.
		/// </summary>
		/// <param name="ftpClient"></param>
		/// <param name="message"></param>
		private void Ftp_CommandSent(object ftpClient, FTPMessageEventArgs message) {
			if (_host != null)
				_host.OnNotificationMessage(this, message.Message);
		}

		/// <summary>
		/// Occurs when a reply has been received from the FTP server.
		/// </summary>
		/// <param name="ftpClient"></param>
		/// <param name="message"></param>
		private void Ftp_ReplyReceived(object ftpClient, FTPMessageEventArgs message) {
			if (_host != null)
				_host.OnNotificationMessage(this, message.Message);
		}
		#endregion

	}
}
