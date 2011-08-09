using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace DeployerEngine.Project
{
	/// <summary>
	/// Contains settings for a  deployment project.
	/// </summary>
	[Serializable]
	public class DeploymentProject
	{
		public const string DefaultConfigName = "Default";
		public const int CurrentFileFormatVersion = 2;

		#region Private members
		private FilterSettings _filters;
		private DatabaseSettings _databasesettings;
		private PluginSettingsCollection _pluginsettings;
		private DeployConfigCollection _deployconfigs = new DeployConfigCollection();
		private int _fileFormatVersion;
		private string _localPath;
		//private string _defaultDeploymentPluginIdentifier = "FTP";

		private int _currentDeployConfigIndex;

		private string _projectFileName;			// The location where the project file was loaded from or saved to

		[NonSerialized]
		private string _cachedstate;			// Used for checking if any settings have changed
		#endregion

		#region Constructor

		public DeploymentProject()
		{
			//_deployconfigs.Add(new DeployTarget(DefaultConfigName));
		}

		#endregion

		#region Properties

		[XmlAttribute]
		public int FileFormatVersion {
			get { return _fileFormatVersion; }
			set { _fileFormatVersion = value; }
		}

		/// <summary>
		/// Gets the filename that the project was last saved or loaded from. Contains null if the project
		/// has not yet been saved.
		/// </summary>
		[XmlIgnore]
		public string FileName {
			get { return _projectFileName; }
		}

		/// <summary>
		/// Gets the name of the project. Currently this is just the filename without the extension.
		/// </summary>
		[XmlIgnore]
		public string Name {
			get {
				if (_projectFileName == null)
					return "<New project>";
				return Path.GetFileNameWithoutExtension(_projectFileName);
			}
		}

		/// <summary>
		/// Gets or sets the local path.
		/// </summary>
		public string LocalPath {
			get { return _localPath; }
			set {
				if(_localPath != value) {
					_localPath = value;
				}
			}
		}


		/// <summary>
		/// Gets the absolute location for the local path. This is determined from the location of the project file,
		/// or the current directory if the file has not yet been saved.
		/// </summary>
		[XmlIgnore]
		public string LocalPathAbsolute {
			get {
				if(string.IsNullOrEmpty(_localPath))
					return null;
				if(_projectFileName == null)
					return Path.GetFullPath( Path.Combine(Environment.CurrentDirectory, _localPath) );
				else
					return Path.GetFullPath( Path.Combine(Path.GetDirectoryName(_projectFileName), _localPath) );
			}
		}


		/// <summary>
		/// Contains the currently active config.
		/// </summary>
		[XmlIgnore]
		public DeployConfig ActiveDeployConfig {
			get {
				if (_deployconfigs.Count == 0)
					return null;
				return _deployconfigs[_currentDeployConfigIndex];
			}
		}

		/// <summary>
		/// Contains the name of the current deploy config.
		/// </summary>
		[XmlIgnore]
		public string ActiveDeployConfigName {
			get {
				if (_deployconfigs.Count == 0)
					return null;
				return _deployconfigs[_currentDeployConfigIndex].Name;
			}
			set {
				for (int i = 0; i < _deployconfigs.Count; i++) {
					if (string.Compare(_deployconfigs[i].Name, value, true) == 0) {
						_currentDeployConfigIndex = i;

						// Ensure plugins are discarded to reload settings from configuration
						PluginManager.UnloadPlugins();
						break;
					}
				}
			}
		}
		
		/// <summary>
		/// Contains the deploy targets for the project.
		/// </summary>
		[XmlArray("Configurations")]
		[XmlArrayItem("Configuration")]
		public DeployConfigCollection DeployConfigurations {
			get { return _deployconfigs; }
			set { _deployconfigs = value; }
		}

		/// <summary>
		/// Gets the plugins.
		/// </summary>
		[XmlArray("Plugins")]
		[XmlArrayItem("Plugin")]
		public PluginSettingsCollection PluginSettings {
			get { return _pluginsettings; }
			set {
				if(_pluginsettings != value) {
					_pluginsettings = value;
				}
			}
		}

		/// <summary>
		/// Gets the databases to deploy.
		/// </summary>
		public DatabaseSettings DatabaseSettings {
			get { return _databasesettings; }
			set {
				if(_databasesettings != value) {
					_databasesettings = value;
				}
			}
		}

		/// <summary>
		/// Gets the filter settings.
		/// </summary>
		[XmlElement("FileFilter")]
		public FilterSettings Filter {
			get { return _filters; }
			set {
				if(_filters != value) {
					_filters = value;
				}
			}
		}

		/// <summary>
		/// Contains the filename of the timestamp file.
		/// </summary>
		[XmlIgnore]
		public string RemoteTimestampFilename {
			get {
				// This should maybe be configurable in the future?
				return string.Format("{0}_{1}.{2}", Path.GetFileNameWithoutExtension(this.FileName), ActiveDeployConfigName, "timestamp");
			}
		}
		#endregion

		#region Public methods

		/// <summary>
		/// Gets the identifier for the plugin to use when deploying files that matches the specified filter. If there is
		/// no identifier set, the default identifier is returned.
		/// </summary>
		//public string GetDeployPluginIdentifier(Filter filter) {
		//    return filter.DeployPluginIdentifier == null ? _defaultDeploymentPluginIdentifier : filter.DeployPluginIdentifier;			
		//}

		/// <summary>
		/// Gets the identifier for the plugin to use when deploying the specified file. If there is
		/// no identifier set, the default identifier is returned.
		/// </summary>
		//public string GetDeployPluginIdentifier(DeploymentFile file) {
		//    return file.DeployDestinationIdentifier == null ? _defaultDeploymentPluginIdentifier : file.DeployDestinationIdentifier;			
		//}

		/// <summary>
		/// Tries to locate a Visual Studio project file name in the root directory of the deployment path.
		/// (Ideally this should be configured in the settings instead)
		/// </summary>
		/// <returns></returns>
		public string GetVisualStudioProjectFileName() {
			string[] files = Directory.GetFiles(LocalPathAbsolute, "*.csproj", SearchOption.TopDirectoryOnly);
			if (files.Length == 0)
				return null;

			return files[0];	// Return first one found
		}

		#endregion

		/// <summary>
		/// Initializes a new project with default settings.
		/// </summary>
		public void InitNewProject() {
			_fileFormatVersion = 1;

			DeployConfig config = new DeployConfig(DefaultConfigName);
			_deployconfigs.Add(config);

			_localPath = ".";

			FilterSettings fs = new FilterSettings();
			ActiveDeployConfig.FilterSettings.Add(fs);

			fs.IncludeFiles.Add(new Filter(FilterExpressionType.Wildcard, @"*.asax"));
			fs.IncludeFiles.Add(new Filter(FilterExpressionType.Wildcard, @"*.ascx"));
			fs.IncludeFiles.Add(new Filter(FilterExpressionType.Wildcard, @"*.asp"));
			fs.IncludeFiles.Add(new Filter(FilterExpressionType.Wildcard, @"*.aspx"));
			fs.IncludeFiles.Add(new Filter(FilterExpressionType.Wildcard, @"*.html"));
			fs.IncludeFiles.Add(new Filter(FilterExpressionType.Wildcard, @"*.dll"));
			fs.IncludeFiles.Add(new Filter(FilterExpressionType.Wildcard, @"*.pdb"));
			fs.IncludeFiles.Add(new Filter(FilterExpressionType.Wildcard, @"*.png"));
			fs.IncludeFiles.Add(new Filter(FilterExpressionType.Wildcard, @"*.gif"));
			fs.IncludeFiles.Add(new Filter(FilterExpressionType.Wildcard, @"*.jpg"));
			fs.IncludeFiles.Add(new Filter(FilterExpressionType.Wildcard, @"*.css"));
			fs.IncludeFiles.Add(new Filter(FilterExpressionType.Wildcard, @"*.xslt"));
			fs.IncludeFiles.Add(new Filter(FilterExpressionType.Wildcard, @"*.js"));
			fs.IncludeFiles.Add(new Filter(FilterExpressionType.Wildcard, @"*.swf"));
			fs.IncludeFiles.Add(new Filter(FilterExpressionType.Wildcard, @"web.config"));

			PluginManager.UnloadPlugins();
		}		

		#region Save/load

		#region Loading

		/// <summary>
		/// Loads a deployment project file.
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static DeploymentProject Load(string filename) {
			PluginManager.UnloadPlugins();

			XmlSerializer xs = new XmlSerializer(typeof(DeploymentProject));
			FileStream fs = File.OpenRead(filename);
			DeploymentProject project = xs.Deserialize(fs) as DeploymentProject;
			fs.Close();
			if (project == null)
				throw new FileLoadException("Unable to load project file.", filename);

			// Verify that the file format version is ok
			if (project._fileFormatVersion > CurrentFileFormatVersion)
				throw new FileLoadException("The project file is not supported by this version of the Deployer. You need to upgrade.");

			// Update local path variable (make it absolute)
			//			project._localPath = Path.Combine(Path.GetDirectoryName(filename), project._localPath);
			project._projectFileName = filename;

			// Perform migration if needed
			project.MigrateProject();

			// Load the plugins used in active deploy target
			//PluginManager.LoadPlugins(project);

			project.UpdateCachedState();

			return project;
		}

		/// <summary>
		/// Checks if the project file needs to be migrated.
		/// This is performed automatically while loading a project.
		/// </summary>
		private void MigrateProject() {

			if (_fileFormatVersion >= CurrentFileFormatVersion)
				return;

			bool migrated = false;

			// Verify and migrate plugins
			if(_pluginsettings != null && _pluginsettings.Count > 0) {
				// Ensure there is a default config
				if (_deployconfigs.Count == 0) {
					_deployconfigs.Add(new DeployConfig(DefaultConfigName));
				}

				// Update and move plugins to the correct destination
				DeployConfig config = _deployconfigs[0];
				foreach(PluginSettings pluginsettings in _pluginsettings) {
					PluginDescriptor descriptor = PluginManager.GetPluginDescriptorForIdentifier(pluginsettings.Identifier);
					pluginsettings.Type = descriptor.PluginTypeFullName;

					DeployDestination destination = new DeployDestination(pluginsettings.Identifier, pluginsettings.Identifier);
					pluginsettings.Identifier = null;
					
					destination.PluginSettings = pluginsettings;
					config.Destinations.Add(destination);
				}
				_pluginsettings = null;
				migrated = true;
			}

			// Migrate plugin types automatically
			if (_fileFormatVersion < 2) {
				foreach (DeployConfig deployconfig in _deployconfigs) {
					foreach (DeployDestination destination in deployconfig.Destinations) {
						// Ensure that the plugin type is ok
						PluginSettings settings = destination.PluginSettings;
						switch (settings.Type) {
							case "DeployerPlugins.Wcm.WcmDeployer":
								settings.Type = "Stendahls.DeployerPlugins.Wcm.WcmDeployer";
								migrated = true;
								break;
							case "DeployerPlugins.GwsDeployer":
								settings.Type = "Stendahls.DeployerPlugins.Gws.GwsDeployer";
								migrated = true;
								break;
						}
					}
				}
				migrated = true;
			}

			// Move database settings
			if (_databasesettings != null) {
				// Ensure there is a default config
				if (_deployconfigs.Count == 0) {
					_deployconfigs.Add(new DeployConfig(DefaultConfigName));
				}

				DeployConfig config = _deployconfigs[0];
				config.DatabaseSettings = _databasesettings;
				_databasesettings = null;

				migrated = true;
			}

			
			// Verify and migrate filters
			if (_filters != null) {
				// Ensure there is a default config
				if (_deployconfigs.Count == 0) {
					_deployconfigs.Add(new DeployConfig(DefaultConfigName));
				}

				// Upgrade filters and move them to default target
				DeployConfig config = _deployconfigs[0];
				//_filters.ExcludeDirectories.Migrate(config);
				//_filters.ExcludeFiles.Migrate(config);
				//_filters.ExcludeProcedures.Migrate(config);
				//_filters.IncludeFiles.Migrate(config);
				config.FilterSettings.Add(_filters);

				// Move database filter
				if (_filters.ExcludeProcedures != null) {
					config.DatabaseSettings.ExcludeProcedures = _filters.ExcludeProcedures;
					_filters.ExcludeProcedures = null;
				}

				// Move use project flag, and clear it from the old place
				if(!string.IsNullOrEmpty(_filters.UseProjectFilter)) {
					bool useProjectFilter;
					if (bool.TryParse(_filters.UseProjectFilter, out useProjectFilter))
						config.UseProjectFilter = useProjectFilter;
					_filters.UseProjectFilter = null;
				}

				_filters = null;
				migrated = true;
			}
			
			if (migrated) {
				_fileFormatVersion = CurrentFileFormatVersion;

				// Project has been upgraded
				EventManager.OnNotificationMessage("Project has been successfully migrated to the new file format.");
			}
		}

		#endregion

		#region Saving

		/// <summary>
		/// Saves the project.
		/// </summary>
		/// <param name="filename"></param>
		public void Save(string filename) {
			// Make sure that settings from plugins are stored in the project as well
			PluginManager.SavePluginSettings(this);

			_projectFileName = filename;
			XmlSerializer xs = new XmlSerializer(typeof(DeploymentProject));
			StreamWriter sw = new StreamWriter(filename);
			xs.Serialize(sw, this);
			sw.Close();

			UpdateCachedState();
		}

		#endregion

		#endregion

		#region Dirty state handling

		/// <summary>
		/// Checks if the project settings are dirty and need to be saved.
		/// </summary>
		public bool IsDirty() {
			string currentstate = SaveToString();
			return currentstate != _cachedstate;
		}

		/// <summary>
		/// Updates the cached state of the project settings. This is used
		/// for checking if the settings need to be saved or not.
		/// </summary>
		private void UpdateCachedState() {
			_cachedstate = SaveToString();
		}

		private string SaveToString() {
			using(MemoryStream ms = new MemoryStream()) {
				XmlSerializer xs = new XmlSerializer(typeof(DeploymentProject));
				xs.Serialize(ms, this);
				return Encoding.UTF8.GetString(ms.ToArray());
			}
		}

		#endregion

		#region Load/Save to string

		/// <summary>
		/// Recreates a project that was previously saved to string.
		/// </summary>
		public static DeploymentProject LoadState(byte[] projectstate) {
			using(MemoryStream ms = new MemoryStream(projectstate)) {
				BinaryFormatter formatter = new BinaryFormatter();
				var project = (DeploymentProject)formatter.Deserialize(ms);
				project.UpdateCachedState();
				return project;
			}
		}

		/// <summary>
		/// Saves the state of the project so that it can be reloaded later.
		/// This is used when editing settings to be able to cancel all changes.
		/// </summary>
		public byte[] SaveState() {
			// Make sure that settings from plugins are stored in the project as well
			PluginManager.SavePluginSettings(this);

			using(MemoryStream ms = new MemoryStream()) {
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(ms, this);
				return ms.ToArray();
			}
		}

		#endregion
	}
}
