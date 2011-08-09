using System;
using System.Collections.Generic;
using System.Text;

namespace DeployerPluginInterfaces {
	/// <summary>
	/// Plugins can implement this interface if they support additional
	/// file control operations. This is used by the Deployer to handle
	/// deployment timestamping.
	/// </summary>
	public interface ITimestampControl {

		/// <summary>
		/// Attempts to connect to the remote server.
		/// </summary>
		void Connect();
		
		/// <summary>
		/// Disconnects from the remote server.
		/// </summary>
		void Disconnect();
		
		/// <summary>
		/// Retrieves the timestamp file from the remote server and stores it locally.
		/// </summary>
		/// <param name="localfilepath">The name and path of the local file.</param>
		/// <param name="remotefilename">The name of the remote timestamp file.</param>
		bool DownloadTimestampFile(string localfilepath, string remotefilename);

		/// <summary>
		/// Uploads the timestamp file to the remote server.
		/// </summary>
		/// <param name="localfilepath">The name and path of the local file.</param>
		/// <param name="remotefilename">The name of the remote timestamp file.</param>
		void UploadTimestampFile(string localfilepath, string remotefilename);
	}
}
