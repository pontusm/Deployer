namespace DeployerPluginInterfaces
{
	/// <summary>
	/// Plugins can implement this to hook into the deployment process.
	/// </summary>
	public interface IDeployerHook : IDeployerPlugin
	{
		/// <summary>
		/// This is called before deployment starts.
		/// </summary>
		void BeforeDeploy(IDeployerEventSink host);

		/// <summary>
		/// This is called after deployment ends.
		/// </summary>
		void AfterDeploy(IDeployerEventSink host);

		// Hooks called for each deployed file
		//void BeforeDeployFile();
		//void AfterDeployFile();
	}
}