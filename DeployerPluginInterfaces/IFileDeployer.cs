namespace DeployerPluginInterfaces
{
	/// <summary>
	/// Common interface for plugins that deploy single files.
	/// </summary>
	public interface IFileDeployer : IDeployerPlugin
	{
		/// <summary>
		/// Begins the deployment operation.
		/// </summary>
		void DeployStart(IDeployerEventSink host);

		/// <summary>
		/// Indicates that the deployment operation is completed.
		/// </summary>
		void DeployEnd();

		/// <summary>
		/// Deploys a single file.
		/// </summary>
		/// <param name="localFilePath">The local path to the file.</param>
		/// <param name="remoteDirectoryPath">The remote directory to deploy the file to.</param>
		/// <param name="remoteFilename">The name that the file will have in the remote path.</param>
		void DeployFile(string localFilePath, string remoteDirectoryPath, string remoteFilename);
		
	}
}