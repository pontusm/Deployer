using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DeployerEngine.Database;
using DeployerEngine.Project;
using DeployerEngine.Timestamp;
using DeployerPluginInterfaces;

namespace DeployerEngine
{
	/// <summary>
	/// Manages deployment tasks. Delegates the actual deployment work to plugins.
	/// </summary>
	public class DeploymentManager : MarshalByRefObject, IDeployerEventSink
	{
		#region Singleton instance

		// Singleton
		public static readonly DeploymentManager Instance = new DeploymentManager();

		#endregion

		#region Constructor

		private DeploymentManager() {}

		#endregion

		#region Public methods

		/// <summary>
		/// Scans databases for differences.
		/// </summary>
		public DatabaseComparison CompareDatabases(DatabasePair databases) {
			try {
				DatabaseDeployer ds = new DatabaseDeployer();
				return ds.CompareDatabases(databases);
			}
			catch (DeployCancelException ex) {
				// Deployment has been cancelled
				EventManager.OnNotificationMessage("*** Verification stopped! " + ex.Message);
			}
			return null;
		}

		/// <summary>
		/// Deploys a database.
		/// </summary>
		public void DeployDatabase(DatabaseDeploymentStructure structure) {
			try {
				DatabaseDeployer ds = new DatabaseDeployer();
				ds.DeployDatabase(structure);
			}
			catch (DeployCancelException ex) {
				// Deployment has been cancelled
				EventManager.OnNotificationMessage("*** Deployment stopped! " + ex.Message);
			}
		}

		/// <summary>
		/// Retrieves the local cached timestamp or null if not found.
		/// </summary>
		/// <param name="project"></param>
		/// <returns></returns>
		public TimestampFile GetLocalTimestampFile(DeploymentProject project) {
			string fname = GetTimestampFilename(project, false);
			if (!File.Exists(fname))
				return null;

			return TimestampFile.Load(fname);
		}

		/// <summary>
		/// Retrieves the remote timestamp information. Returns null if no timestamp was found.
		/// </summary>
		/// <param name="project">The current deployment project.</param>
		/// <returns></returns>
		public TimestampFile GetRemoteTimestampFile(DeploymentProject project) {
			try {
				ITimestampControl plugin = GetTimestampPlugin(project);
				if (plugin == null)
					return null;

				// Download timestamp file
				EventManager.OnNotificationMessage("Checking timestamp...");
				string fname = GetTimestampFilename(project, true);
				plugin.Connect();
				bool retval = plugin.DownloadTimestampFile(fname, project.RemoteTimestampFilename);
				plugin.Disconnect();
				if (!retval) {
					EventManager.OnNotificationMessage("No timestamp found.");
					return null;
				}

				EventManager.OnNotificationMessage("Timestamp retrieved.");

				return TimestampFile.Load(fname);
			}
			catch (FileLoadException ex) {
				EventManager.OnNotificationMessage("Timestamp not found. " + ex.Message);
				return null;
			}
		}

		/// <summary>
		/// Determines if a plugin has timestamp support.
		/// </summary>
		/// <param name="pluginIdentifier"></param>
		//public bool HasTimestampSupport(string pluginIdentifier) {
		//    ITimestampControl ts = PluginManager.Plugins.Get(pluginIdentifier) as ITimestampControl;
		//    if (ts == null)
		//        return false;
		//    return true;
		//}

		/// <summary>
		/// Deploys files.
		/// </summary>
		/// <param name="project">Contains information about what and where to deploy.</param>
		/// <param name="files">The files to deploy.</param>
		public void DeployFiles(DeploymentProject project, IList<DeploymentFile> files) {
			// Nothing to deploy?
			if(files.Count == 0)
				return;

			EventManager.OnNotificationMessage("Updating timestamp...");
			UpdateTimestamp(project);

			EventManager.OnNotificationMessage("Starting deployment...");
			

			try {
				// Initiate all deployment hooks
				foreach (var hook in project.ActiveDeployConfig.HookSettings)
				{
					IDeployerHook hookPlugin = PluginManager.GetPluginForHook(hook);
					hookPlugin.BeforeDeploy(this);
				}

				// Signal to all plugins in the project that deployment is starting
				foreach (DeployDestination dest in project.ActiveDeployConfig.Destinations)
				{
					var plugin = PluginManager.GetPluginForDestination(dest) as IFileDeployer;
					if(plugin != null)
						plugin.DeployStart(this);
				}

				// Deploy files
				foreach(DeploymentFile file in files) {
					if(file.IncludeInDeployment) {
						EventManager.OnNotificationMessage(string.Format("Deploying \"{0}\"...", file.Name));

						// Signal that transfer started
						TransferEventArgs args = new TransferEventArgs(file.LocalPath);
						EventManager.OnTransferBegin(args);

						// Start transferring
						string destid = file.DeployDestinationIdentifier;
						IDeployerPlugin plugin = PluginManager.GetPlugin(destid);
						if(plugin == null)
							throw new ApplicationException(string.Format("Unable to deploy to destination '{0}'. The destination has not been configured.", destid));
						
						var fileDeployer = plugin as IFileDeployer;
						if(fileDeployer == null)
							throw new InvalidOperationException(string.Format("Plugin '{0}' does not support file deployment.", plugin.Name));

						// Transfer retry loop
						bool retry;
						do {
							retry = false;		// Assume we won't retry operation
							try {
								string remotename = string.IsNullOrEmpty(file.RemoteName) ? file.Name : file.RemoteName;

								// Deploy file using plugin
								fileDeployer.DeployFile(file.LocalPath, file.RemotePath, remotename);

								// All ok, update event args
								args.BytesSent = args.TotalBytes;
							}
							catch (DeployCancelException) {
								throw;			// Rethrow exception to cancel deployment loop
							}
							catch(DeploySkipException) {
								EventManager.OnNotificationMessage("Skipped file " + file.LocalPath);
							}
							catch(Exception ex) {
								DeployErrorForm dlg = new DeployErrorForm();
								dlg.ErrorMessage = ex.Message;
								dlg.SetLocalInfo("Local File:", file.LocalPath);
								dlg.SetRemoteInfo("Remote Path:", file.RemotePath);
								dlg.Plugin = fileDeployer.Name;
								DialogResult result = dlg.ShowDialog();
								switch(result) {
									case DialogResult.Retry:
										retry = true;
										break;
									case DialogResult.Ignore:
										break;		// Skip to next file
									case DialogResult.Cancel:
										throw new DeployCancelException("User cancelled.");
								}
							}
						} while (retry);

						// Signal that transfer is complete
						EventManager.OnTransferComplete(args);
					}
				}

				// Signal to all plugins in the project that deployment is complete
				foreach (DeployDestination dest in project.ActiveDeployConfig.Destinations) {
					var plugin = PluginManager.GetPluginForDestination(dest) as IFileDeployer;
					if(plugin != null)
						plugin.DeployEnd();
				}

				// Signal all deployment hooks
				foreach (var hook in project.ActiveDeployConfig.HookSettings)
				{
					IDeployerHook hookPlugin = PluginManager.GetPluginForHook(hook);
					hookPlugin.AfterDeploy(this);
				}

				EventManager.OnNotificationMessage("Deployment completed.");
			}
			catch (DeployCancelException ex) {
				// Deployment has been cancelled
				EventManager.OnNotificationMessage("*** Deployment stopped! " + ex.Message);
			}
			finally
			{
				// Signal to all plugins in the project that deployment is complete
				foreach (DeployDestination dest in project.ActiveDeployConfig.Destinations)
				{
					var plugin = PluginManager.GetPluginForDestination(dest) as IFileDeployer;
					if (plugin != null)
						plugin.DeployEnd();
				}
			}

		}

		/// <summary>
		/// Updates the remote timestamp.
		/// </summary>
		private static void UpdateTimestamp(DeploymentProject project) {
			try {
				// Get timestamp plugin if available
				ITimestampControl tsplugin = GetTimestampPlugin(project);
				if (tsplugin == null)
					return;

				// Create timestamp with current time and cache it locally before uploading it to remote server
				TimestampFile timestamp = new TimestampFile();
				timestamp.DeployedBy = Environment.UserName;
				timestamp.LastDeployment = DateTime.Now;
				string fname = GetTimestampFilename(project, false);
				timestamp.Save(fname);

				tsplugin.Connect();
				tsplugin.UploadTimestampFile(fname, project.RemoteTimestampFilename);
				tsplugin.Disconnect();
			}
			catch (DirectoryNotFoundException ex) {
				EventManager.OnNotificationMessage("*** WARNING: Unable to update timestamp: " + ex.Message);
			}
			catch(IOException ex)
			{
				throw new ApplicationException(ex.Message);
			}
		}

		/// <summary>
		/// Scans a database for objects to deploy.
		/// </summary>
		public DatabaseDeploymentStructure ScanDatabase(DeploymentProject project, DatabasePair databases) {
			DatabaseDeployer ds = new DatabaseDeployer();
			return ds.ScanDatabase(project, databases);
		}

		/// <summary>
		/// Scans the files in the project and returns a structure that can be used when deploying.
		/// </summary>
		public DeploymentStructure ScanFiles(DeploymentProject project, DateTime modifiedSince) {
			// Find all files to deploy
			FileScanner fs = new FileScanner();
			return fs.FindFiles(project, modifiedSince);
		}

		#endregion

		#region IDeployerEventSink Members

		public void OnNotificationMessage(object sender, string message) {
			// Prefix message with plugin identifier
			IDeployerPlugin plugin = sender as IDeployerPlugin;
			if(plugin != null)
				message = string.Format("{0} : {1}", plugin.PluginIdentifier, message);

			EventManager.OnNotificationMessage(message);
		}

		public void OnTransferProgress(object sender, TransferEventArgs e) {
			EventManager.OnTransferProgress(e);
		}

		#endregion

		#region Timestamp handling

		/// <summary>
		/// Returns the path to the local timestamp file containing info about when the user last deployed the project.
		/// </summary>
		/// <param name="project">The current project file.</param>
		/// <param name="remote">If true it is the remote timestamp file, if false it is the local one.</param>
		private static string GetTimestampFilename(DeploymentProject project, bool remote) {
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Deployer");
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			// Use the filename to get a unique filename for the timestamp file, or the project name if not yet saved
			string fileName = !string.IsNullOrEmpty(project.FileName) ? project.FileName : project.Name;

			string filepath = Path.Combine(path, string.Format("{0}_{1}", fileName.GetHashCode().ToString("x"), project.ActiveDeployConfigName));
			if (remote)
				return filepath + ".remote";
			else
				return filepath + ".local";
		}

		/// <summary>
		/// Gets the plugin that has been configured to handle timestamping for the project.
		/// Returns null if no timestamping is configured.
		/// </summary>
		/// <param name="project">The current project settings.</param>
		private static ITimestampControl GetTimestampPlugin(DeploymentProject project) {
			// Verify timestamp plugin for the project
			string destid = project.ActiveDeployConfig.TimestampDestinationIdentifier;
			if (string.IsNullOrEmpty(destid))
				return null;

			// Lookup destination
			DeployDestination dest = project.ActiveDeployConfig.Destinations.Get(destid);
			if(dest == null)
				throw new ApplicationException(string.Format("Destination '{0}' not found.", destid));

			ITimestampControl plugin = PluginManager.GetPluginForDestination(dest) as ITimestampControl;
			if (plugin == null)
				throw new ApplicationException(string.Format("Plugin for destination '{0}' does not support timestamp control", dest.Name));

			return plugin;
		}
		
		#endregion
	}
}
