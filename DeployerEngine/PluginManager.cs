using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml;
using DeployerEngine.Project;
using DeployerPluginInterfaces;

namespace DeployerEngine
{
	/// <summary>
	/// Handles the plugins for the engine.
	/// </summary>
	/// <remarks>
	/// 2005-01-14	POMU: Class Created
	/// 2006-01-16	POMU: Plugins are now loaded in a separate app domain
	/// 2006-01-30	POMU: Class is now static
	/// 2006-10-12	POMU: More than one plugin of the same type can now be loaded
	/// </remarks>
	public static class PluginManager
	{
		#region Private Members

		private static readonly Dictionary<string, PluginDescriptor> _pluginDescriptors = new Dictionary<string, PluginDescriptor>();
		
		private static readonly Dictionary<string, IDeployerPlugin> _plugins = new Dictionary<string, IDeployerPlugin>();

		private static AppDomain _pluginAppDomain;

		private static string _pluginPath;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the collection of loaded plugins.
		/// </summary>
		public static IEnumerable<IDeployerPlugin> Plugins {
			get { return _plugins.Values; }
		}

		#endregion

		/// <summary>
		/// Gets the descriptors for the available plugins.
		/// </summary>
		/// <returns></returns>
		public static PluginDescriptor[] GetPluginDescriptors() {
			List<PluginDescriptor> list = new List<PluginDescriptor>(_pluginDescriptors.Values);
			return list.ToArray();
		}

		/// <summary>
		/// Gets the plugin with the specified identifier. The plugin must be loaded
		/// or this will return null.
		/// </summary>
		public static IDeployerPlugin GetPlugin(string identifier)
		{
			IDeployerPlugin plugin;
			_plugins.TryGetValue(identifier, out plugin);
			return plugin;
		}

		/// <summary>
		/// Gets the plugin descriptors matching the supplied filter.
		/// </summary>
		public static IEnumerable<PluginDescriptor> GetPluginDescriptors(Predicate<PluginDescriptor> filter)
		{
			foreach (var pluginDescriptor in _pluginDescriptors.Values)
			{
				if (filter(pluginDescriptor))
					yield return pluginDescriptor;
			}
		}

		/// <summary>
		/// This method locates the descriptor for a certain plugin identifier. This is used
		/// during migration to convert from the old identifier system.
		/// </summary>
		public static PluginDescriptor GetPluginDescriptorForIdentifier(string pluginIdentifier) {
			foreach (PluginDescriptor descriptor in _pluginDescriptors.Values) {
				if (descriptor.Identifier == pluginIdentifier)
					return descriptor;
			}
			return null;
		}

		/// <summary>
		/// Retrieves the plugin for the specified destination. It will be automatically
		/// loaded if required.
		/// </summary>
		public static IDeployerPlugin GetPluginForDestination(DeployDestination destination) {
			IDeployerPlugin plugin;
			if (!_plugins.TryGetValue(destination.Identifier, out plugin))
			{
				// We need to load the plugin
				plugin = LoadPlugin(destination);
			}

			return plugin;
		}

		/// <summary>
		/// Retrieves the plugin used for hooking into deployment. The plugin will be automatically
		/// loaded if required.
		/// </summary>
		public static IDeployerHook GetPluginForHook(DeployHookSettings hook)
		{
			IDeployerPlugin plugin;
			if(!_plugins.TryGetValue(hook.Identifier, out plugin))
			{
				// Attempt to load it
				plugin = LoadPlugin(hook);
			}

			return (IDeployerHook) plugin;
		}

		/// <summary>
		/// Unloads the plugin for a specified destination.
		/// </summary>
		public static void UnloadPluginForDestination(DeployDestination destination) {
			_plugins.Remove(destination.Identifier);
		}

		#region Scan plugins

		/// <summary>
		/// Scans for all plugins in the specified directory.
		/// </summary>
		/// <param name="path">The path to scan for plugins.</param>
		public static void ScanPlugins(string path) {
			try {
				_pluginPath = path;

				// Unload previous plugins if necessary
				UnloadPlugins();

				// This method does the following:
				// 1. Creates a temp appdomain
				// 2. Loads all assemblies in the plugin directory to find available plugins
				// 3. Saves descriptors of the discovered plugins
				// 4. Unloads the temp appdomain (to unload unused types)

				// Create temp app domain and instantiate a plugin searcher in it that will locate plugins
				AppDomain tmpdomain = CreatePluginDomain(path);
				Type type = typeof(PluginSearcher);
				//PluginSearcher searcher = (PluginSearcher) tmpdomain.CreateInstanceAndUnwrap(searchertype.Assembly.FullName, searchertype.FullName);
				PluginSearcher searcher = (PluginSearcher)Activator.CreateInstanceFrom(tmpdomain, type.Assembly.Location, type.FullName).Unwrap();
				PluginDescriptor[] pluginDescriptors = searcher.FindPlugins(path);

				// Report any loading errors
				if (searcher.Errors.Count > 0) {
					EventManager.OnNotificationMessage("*** One or more plugins failed to load:");
					foreach (var err in searcher.Errors) {
						EventManager.OnNotificationMessage("    " + err);
					}
				}

				// Populate the dictionary we will use when creating plugins
				foreach(PluginDescriptor descriptor in pluginDescriptors)
					_pluginDescriptors.Add(descriptor.PluginTypeFullName, descriptor);

				// Unload temp appdomain
				AppDomain.Unload(tmpdomain);

				// Create new app domain for plugins and create an instance of each plugin
				//_pluginAppDomain = CreatePluginDomain(path);
				//foreach(PluginDescriptor descriptor in _pluginDescriptors) {
				//    LoadPlugin(descriptor);
				//}
			}
			catch (Exception ex) {
				EventManager.OnNotificationMessage("Failed to load plugins. " + ex.Message);
				Debug.WriteLine(ex.ToString());
			}
		}
		
		#endregion

		#region Load plugins

		/// <summary>
		/// Loads all plugins for the currently active deploy target.
		/// Previously loaded plugins are unloaded first.
		/// </summary>
		public static void LoadPlugins(DeploymentProject project) {
			UnloadPlugins();

			DeployConfig config = project.ActiveDeployConfig;
			if (config == null)
				return;
			
			// Load settings for plugins
			foreach(DeployDestination dest in config.Destinations) {
				LoadPlugin(dest);
			}
		}

		/// <summary>
		/// Loads a plugin for the specified deploy destination.
		/// </summary>
		private static IDeployerPlugin LoadPlugin(DeployDestination destination) {
			string pluginType = destination.PluginSettings.Type;
			//if(string.IsNullOrEmpty(pluginType)) {
			//    // Plugin type not found. Look up plugin type using the identifier instead
			//    // TODO: Remove this when no old deploy files remain
			//    PluginDescriptor descriptor = GetPluginDescriptorForIdentifier(destination.PluginSettings.Identifier);
			//    if(descriptor == null) {
			//        throw new FileLoadException(
			//            string.Format("Unable to load plugin for destination '{0}'. Plugin type not found.", destination.Name));
			//    }
			//    pluginType = descriptor.PluginTypeFullName;
			//}

			// Load plugin
			EventManager.OnNotificationMessage(string.Format("Loading plugin for destination '{0}'...", destination.Name));
			return LoadPlugin(destination.Identifier, pluginType, destination.PluginSettings.Settings.OuterXml);
		}

		/// <summary>
		/// Loads a plugin for the specified hook.
		/// </summary>
		private static IDeployerPlugin LoadPlugin(DeployHookSettings hook)
		{
			string pluginType = hook.Pluginsettings.Type;
			EventManager.OnNotificationMessage(string.Format("Loading plugin for hook '{0}'...", hook.Identifier));
			return LoadPlugin(hook.Identifier, pluginType, hook.Pluginsettings.Settings.OuterXml);
		}

		/// <summary>
		/// Loads a plugin and adds it to the list of loaded plugins.
		/// </summary>
		/// <param name="identifier">The destination that the plugin is used in.</param>
		/// <param name="pluginTypeFullName">The full type name for the plugin.</param>
		/// <param name="settings">The settings for the plugin.</param>
		private static IDeployerPlugin LoadPlugin(string identifier, string pluginTypeFullName, string settings) {
			if (!_pluginDescriptors.ContainsKey(pluginTypeFullName))
				throw new ArgumentException(string.Format("Unknown plugin type '{0}'. Could not load plugin.", pluginTypeFullName));

			IDeployerPlugin plugin = LoadPlugin(identifier, _pluginDescriptors[pluginTypeFullName]);
			if (plugin != null)
				plugin.LoadSettings(settings);
			
			return plugin;
		}

		private static IDeployerPlugin LoadPlugin(string identifier, PluginDescriptor descriptor) {
			try {
				// Ensure that the plugin domain is initialized
				if (_pluginAppDomain == null)
					_pluginAppDomain = CreatePluginDomain(_pluginPath);

				// Load plugin and add it to the plugin collection
				var plugin = (IDeployerPlugin) _pluginAppDomain.CreateInstanceAndUnwrap(descriptor.AssemblyFullName, descriptor.PluginTypeFullName);
				//Type t = typeof (PluginWrapper);
				//PluginWrapper plugin = (PluginWrapper) _pluginAppDomain.CreateInstanceAndUnwrap(t.Assembly.FullName, t.FullName);
				//plugin.Create(descriptor);
				_plugins.Add(identifier, plugin);
				EventManager.OnNotificationMessage(string.Format("Plugin '{0}' loaded.", plugin.Name));
				return plugin;
			}
			catch (Exception ex) {
				EventManager.OnNotificationMessage(string.Format("Could not create plugin '{0}'. Exception: {1}", descriptor.PluginName, ex.Message));
				return null;
			}
		}

		#endregion

		#region Unload plugins

		/// <summary>
		/// Unloads all currently loaded plugins and destroys
		/// the plugin domain.
		/// </summary>
		public static void UnloadPlugins() {
			if (_pluginAppDomain != null) {
				AppDomain.Unload(_pluginAppDomain);
				_plugins.Clear();
				_pluginAppDomain = null;
				EventManager.OnNotificationMessage("All plugins unloaded.");
			}
		}

		#endregion

		#region Save/load plugin settings

		/// <summary>
		/// Loads the plugin settings for the currently active deploy target.
		/// </summary>
		public static void LoadPluginSettings(DeploymentProject project) {
			DeployConfig config = project.ActiveDeployConfig;
			if (config == null)
				return;

			// Load settings for plugins
			foreach (DeployDestination dest in config.Destinations) {
				IDeployerPlugin plugin;
				if (_plugins.TryGetValue(dest.Identifier, out plugin))
				{
					plugin.LoadSettings(dest.PluginSettings.Settings.OuterXml);
				}

			}
		}

		/// <summary>
		/// Saves the settings for the currently active deployment target.
		/// (Only those plugins are loaded at the same time).
		/// </summary>
		public static void SavePluginSettings(DeploymentProject project) {
			DeployConfig config = project.ActiveDeployConfig;
			if (config == null)
				return;
			
			foreach(DeployDestination dest in config.Destinations) {
				IDeployerPlugin plugin;
				if (_plugins.TryGetValue(dest.Identifier, out plugin))
				{
					string settings = plugin.SaveSettings();

					// TODO This is a hack to replace the settings (need to implement a better way to handle plugin settings)
					int TODO;
					XmlDocument xd = new XmlDocument();
					xd.LoadXml(settings);
					XmlElement oldsettings = dest.PluginSettings.Settings;
					XmlElement newsettings = (XmlElement)oldsettings.OwnerDocument.ImportNode(xd.DocumentElement, true);
					//oldsettings.ParentNode.ReplaceChild(newsettings, oldsettings);
					dest.PluginSettings.Settings = newsettings;
					//dest.PluginSettings.OldSettings = null;
				}
			}
		}

		#endregion
		
		#region Private methods

		/// <summary>
		/// Creates a new app domain for plugins.
		/// </summary>
		/// <param name="pluginpath">The path to where plugins will be loaded from.</param>
		private static AppDomain CreatePluginDomain(string pluginpath) {
			// Setup plugin permissions
			PermissionSet permissions = new PermissionSet(PermissionState.None);
			permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution | SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.Infrastructure | SecurityPermissionFlag.RemotingConfiguration));
			//permissions.AddPermission(new SecurityPermission(PermissionState.Unrestricted));
			permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			permissions.AddPermission(new UIPermission(UIPermissionWindow.AllWindows));
			permissions.AddPermission(new DnsPermission(PermissionState.Unrestricted));
			permissions.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			permissions.AddPermission(new SocketPermission(PermissionState.Unrestricted));
			permissions.AddPermission(new WebPermission(PermissionState.Unrestricted));
			permissions.Demand();

			// Get the strong name for the deployer assembly so it can be fully trusted in the new domain.
			StrongName deployerEngineStrongName = GetStrongName(Assembly.GetExecutingAssembly());

			// Create new appdomain with the specified permission set
			AppDomainSetup domaininfo = new AppDomainSetup();
			domaininfo.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
			domaininfo.PrivateBinPath = pluginpath;
			AppDomain appdomain = AppDomain.CreateDomain("Plugins", AppDomain.CurrentDomain.Evidence, domaininfo, permissions, deployerEngineStrongName);

			return appdomain;
		}

		/// <summary>
		/// Retrieves the strong name for an assembly.
		/// </summary>
		private static StrongName GetStrongName(Assembly assembly) {
			if (assembly == null)
				throw new ArgumentNullException("assembly");

			AssemblyName assemblyName = assembly.GetName();

			// Get the public key blob
			byte[] publicKey = assemblyName.GetPublicKey();
			if (publicKey == null || publicKey.Length == 0)
				throw new InvalidOperationException(String.Format("{0} is not strongly named", assembly));

			StrongNamePublicKeyBlob keyBlob = new StrongNamePublicKeyBlob(publicKey);

			// create the StrongName
			return new StrongName(keyBlob, assemblyName.Name, assemblyName.Version);
		}


		#endregion
	}


	#region PluginSearcher

	/// <summary>
	/// Searches a directory for available plugins.
	/// </summary>
	public class PluginSearcher : MarshalByRefObject {

		private readonly List<string> _errors = new List<string>();

		/// <summary>
		/// Gets the errors that occured during discovery (if any).
		/// </summary>
		public List<string> Errors {
			get { return _errors; }
		}

		/// <summary>
		/// Searches the specified directory for plugins.
		/// </summary>
		/// <param name="path"></param>
		public PluginDescriptor[] FindPlugins(string path) {
			var plugindescriptors = new List<PluginDescriptor>();

			// Scan directory for plugin DLLs
			var files = Directory.GetFiles(path, "*.dll");
			foreach (var file in files)
			{
				var assembly = LoadAssembly(file);
				if (assembly != null) {
					try {
						var pluginDescriptors = DiscoverPlugins(assembly);
						plugindescriptors.AddRange(pluginDescriptors);
					}
					catch(ReflectionTypeLoadException ex) {
						Debug.WriteLine(ex);
						Errors.Add(string.Format("Skipping assembly '{0}'. Loader exceptions:", assembly.FullName));
						foreach (var loaderException in ex.LoaderExceptions) {
							Errors.Add(string.Format("    {0}", loaderException.Message));
						}
					}
					catch (Exception ex) {
						Debug.WriteLine(ex);
						Errors.Add(string.Format("Skipping assembly '{0}'. ({1})", assembly.FullName, ex.Message));
					}
				}
			}

			return plugindescriptors.ToArray();
		}

		/// <summary>
		/// Discovers all plugins that are found in the specified assembly.
		/// </summary>
		private IEnumerable<PluginDescriptor> DiscoverPlugins(Assembly assembly) {
			foreach (Type type in assembly.GetTypes()) {
				if (typeof(IDeployerPlugin).IsAssignableFrom(type)) {
					IDeployerPlugin plugin = null;
					try {
						// Create instance to retrieve identifier
						// TODO: This should be removed once identifiers are no longer needed
						plugin = (IDeployerPlugin) Activator.CreateInstance(type);
					}
					catch (Exception ex) {
						Debug.WriteLine(ex);
						Errors.Add(string.Format("Skipping plugin '{0}'. Error: Failed to create type '{1}' in assembly '{2}'. ({3})", type.Name, type.FullName, assembly.FullName, ex.Message));
					}

					if (plugin != null)
						yield return new PluginDescriptor(type, plugin.PluginIdentifier, plugin.Name, plugin.Version);
				}
			}
		}

		private Assembly LoadAssembly(string assemblyFileName)
		{
			try
			{
				return Assembly.LoadFile(assemblyFileName);
			}
			catch (BadImageFormatException)
			{
				// Not a valid assembly, so just skip it
			}
			catch (Exception ex) {
				Debug.WriteLine(ex);
				Errors.Add(string.Format("Plugin could not be loaded: '{0}' ({1})", assemblyFileName, ex.Message));
				//EventManager.OnNotificationMessage(string.Format("Plugin could not be loaded: '{0}'", assemblyFileName));
			}
			return null;
		}
	}

	#endregion


	#region PluginDescriptor

	/// <summary>
	/// Contains information about a plugin.
	/// </summary>
	[Serializable]
	public class PluginDescriptor {
		#region Private members

		//private Type _pluginType;			// Cannot store type here because Deployer will then try to load assembly in wrong appdomain
		private string _assemblyFullName;
		private string _pluginTypeFullName;
		private string _pluginName;
		private string _identifier;			// Multiple plugins with the same identifier may now be loaded at the same time
		private Version _version;
		#endregion

		#region Constructor

		internal PluginDescriptor(Type pluginType, string identifier, string name, Version version) {
			//_pluginType = pluginType;
			_assemblyFullName = pluginType.Assembly.FullName;
			_pluginTypeFullName = pluginType.FullName;
			_pluginName = name;
			_version = version;

			_identifier = identifier;

			SupportsFileDeploy = typeof(IFileDeployer).IsAssignableFrom(pluginType);
			SupportsDeployHook = typeof (IDeployerHook).IsAssignableFrom(pluginType);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the name of the plugin.
		/// </summary>
		public string PluginName {
			get {
//				return _pluginType.Name;
				return _pluginName;
			}
		}

		/// <summary>
		/// Gets the version of the plugin.
		/// </summary>
		public Version PluginVersion {
			get { return _version; }
		}

		public string AssemblyFullName {
			get { return _assemblyFullName; }
		}

		public string Identifier {
			get { return _identifier; }
		}

		public string PluginTypeFullName {
			get { return _pluginTypeFullName; }
		}

		public bool SupportsFileDeploy { get; private set; }
		public bool SupportsDeployHook { get; private set; }
		#endregion
	}

	#endregion
	
}
