using System;
using System.Diagnostics;
using System.IO;
using DeployerEngine.Project;
using DeployerPluginInterfaces;
using EnterpriseDT.Net.Ftp;

namespace DeployerEngine
{
	/// <summary>
	/// Deploys files over FTP.
	/// </summary>
	internal class FtpDeployer
	{
		private FTPClient _ftp;
		private TransferEventArgs _currentTransferEventArgs;

		internal FtpDeployer()
		{
		}

		/// <summary>
		/// Deploys files to an FTP site.
		/// </summary>
		internal void Deploy(DeploymentProject project, DeploymentStructure structure) {
			// Init FTP client
//			FtpServerSettings settings = project.FtpServer;
//			_ftp = new FTPClient(settings.Address, settings.Port);
//			_ftp.CommandSent += new FTPMessageHandler(Ftp_CommandSent);
//			_ftp.ReplyReceived += new FTPMessageHandler(Ftp_ReplyReceived);
//			_ftp.BytesTransferred += new BytesTransferredHandler(Ftp_BytesTransferred);
//			_ftp.Login(settings.Login, settings.Password);
//			_ftp.TransferType = FTPTransferType.BINARY;
//
//			// Deploy the structure
//			DeployStructure(structure, settings.Path);
//
//			_ftp.Quit();
		}

		#region Private methods

		/// <summary>
		/// Tries to change the current remote directory.
		/// </summary>
		/// <param name="remotePath"></param>
		private void ChangeRemoteDirectory(string remotePath) {
			try {
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
		/// Deploys a structure recursively. Works in the current remote directory.
		/// </summary>
		/// <param name="structure"></param>
		private void DeployStructure(DeploymentStructure structure, string remoteBasePath) {
			// Deploy files
			string currentRemotePath = null;
			foreach(DeploymentFile file in structure.Files) {
				if(currentRemotePath != file.RemotePath) {
					ChangeRemoteDirectory(Path.Combine(remoteBasePath, file.RemotePath));		// Will create the directory if not existing
					currentRemotePath = file.RemotePath;
				}

//				// Get files in directory
//				string[] remoteFiles = _ftp.Dir();

				// Signal that transfer started
				_currentTransferEventArgs = new TransferEventArgs(file.LocalPath);
				EventManager.OnTransferBegin(_currentTransferEventArgs);

				// Start transferring (FTPClient will raise events about the progress)
				_ftp.Put(file.LocalPath, file.Name);

				// Signal that transfer is complete
				EventManager.OnTransferComplete(_currentTransferEventArgs);
			}


			// Verify that directories exist
//			foreach(DeploymentStructure dir in structure.Directories) {
//				// Create directory?
//				if(Array.IndexOf(remoteFiles, dir.Name) == -1)
//					_ftp.MkDir(dir.Name);
//			}

			// Deploy each directory (will change the current remote directory)
//			foreach(DeploymentStructure dir in structure.Directories) {
//				DeployStructure(dir, string.Format("{0}/{1}", remotePath, dir.Name));
//			}
		}

		#endregion

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
			remotePath = remotePath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);		// Remove trailing slash
			int pos = remotePath.LastIndexOfAny(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
			if(pos == -1)
				return remotePath;
			
			return remotePath.Substring(pos + 1);
		}

		#region Event handlers

		/// <summary>
		/// Occurs during transfer of a file.
		/// </summary>
		/// <param name="ftpClient"></param>
		/// <param name="bytesTransferred"></param>
		private void Ftp_BytesTransferred(object ftpClient, BytesTransferredEventArgs bytesTransferred) {
			_currentTransferEventArgs.BytesSent = bytesTransferred.ByteCount;
			EventManager.OnTransferProgress(_currentTransferEventArgs);
		}

		/// <summary>
		/// Occurs when a command has been sent to the FTP server.
		/// </summary>
		/// <param name="ftpClient"></param>
		/// <param name="message"></param>
		private void Ftp_CommandSent(object ftpClient, FTPMessageEventArgs message) {
			EventManager.OnCommandSent(message.Message);
		}

		/// <summary>
		/// Occurs when a reply has been received from the FTP server.
		/// </summary>
		/// <param name="ftpClient"></param>
		/// <param name="message"></param>
		private void Ftp_ReplyReceived(object ftpClient, FTPMessageEventArgs message) {
			EventManager.OnReplyReceived(message.Message);
		}
		#endregion

	}
}
