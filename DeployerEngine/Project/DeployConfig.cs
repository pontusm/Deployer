using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using DeployerEngine.Util;

namespace DeployerEngine.Project {
	/// <summary>
	/// Contains a group of deploy destinations that are included in a
	/// complete deployment for a specific target.
	/// </summary>
	/// <remarks>
	/// 2006-10-12	POMU: Class created 
	/// </remarks>
	[Serializable]
	public class DeployConfig {
		#region Private members

		private string _name;
		private string _displayName;

		private readonly Collection<DeployHookSettings> _hookSettings = new Collection<DeployHookSettings>();

		private DeployDestinationCollection _destinations = new DeployDestinationCollection();
		//private FilterSettings _filters = new FilterSettings();
		private FilterSettingsCollection _filtersettings = new FilterSettingsCollection();
		private DatabaseSettings _databasesettings = new DatabaseSettings();

		private string _timestampDestinationIdentifier;
		private bool _useProjectFilter;

		#endregion

		#region Constructor

		public DeployConfig() {}

		public DeployConfig(string name) {
			_name = name;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Contains the name of the deploy target.
		/// </summary>
		[XmlAttribute]
		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// Gets or sets the display name of the config.
		/// </summary>
		/// <value>The display name.</value>
		public string DisplayName {
			get {
				if (string.IsNullOrEmpty(_displayName))
					return Name;
				return _displayName;
			}
			set {
				_displayName = value;
			}
		}

		/// <summary>
		/// Whether to filter against project file or not.
		/// </summary>
		public bool UseProjectFilter {
			get { return _useProjectFilter; }
			set { _useProjectFilter = value; }
		}

		/// <summary>
		/// Gets or sets the identifier for the plugin to use for timestamping.
		/// </summary>
		[XmlElement("TimestampDestination")]
		public string TimestampDestinationIdentifier {
			get { return _timestampDestinationIdentifier; }
			set { _timestampDestinationIdentifier = value; }
		}

		/// <summary>
		/// Contains the deploy destinations for the target.
		/// </summary>
		[XmlArray("Destinations")]
		[XmlArrayItem("Destination")]
		public DeployDestinationCollection Destinations {
			get { return _destinations; }
			set { _destinations = value; }
		}

		/// <summary>
		/// Gets the collection of filter settings.
		/// </summary>
		[XmlArray("FileFilters")]
		[XmlArrayItem("FileFilter")]
		public FilterSettingsCollection FilterSettings {
			get { return _filtersettings; }
		}

		/// <summary>
		/// Gets the collection of deploy hooks.
		/// </summary>
		[XmlArray("Hooks")]
		[XmlArrayItem("Hook")]
		public Collection<DeployHookSettings> HookSettings
		{
			get { return _hookSettings; }
		}

		/// <summary>
		/// Gets the databases to deploy.
		/// </summary>
		public DatabaseSettings DatabaseSettings {
			get { return _databasesettings; }
			set {
				_databasesettings = value;
			}
		}

		#endregion

		#region Filter matching

		/// <summary>
		/// Checks if the directory can be included in a deployment or if it is excluded by a filter.
		/// </summary>
		/// <param name="virtualPath">The virtual path to the directory.</param>
		internal bool CanIncludeDirectory(string virtualPath) {

			// Check all filter settings
			string currentpath = virtualPath;
			while (currentpath != null) {
				// Look up filter settings for the virtual path
				FilterSettings fs = _filtersettings.Get(currentpath);
				if (fs != null) {
					// Is the virtual path among the excluded directories?
					if (fs.ExcludeDirectories.IsMatching(virtualPath) ||
					    fs.ExcludeDirectories.IsMatching(@"\" + virtualPath))
						return false;
				}

				// Nothing found, so we step up in the virtual path
				currentpath = PathHelper.GetParentDirectoryPath(currentpath);
			}

			// Directory can be included
			return true;
		}

		/// <summary>
		/// Tries to locate a matching file filter.
		/// </summary>
		/// <param name="localFilePath">The full path to the local file.</param>
		/// <param name="virtualPath">The virtual path of the file.</param>
		//internal Filter FindMatchingFileFilter(string localFilePath, string virtualPath) {
		//    // This is the path that will be matched
		//    string filePath = Path.Combine(virtualPath, Path.GetFileName(localFilePath));

		//    while(virtualPath != null) {
		//        // Look up filter settings for the virtual path
		//        FilterSettings fs = _filtersettings.Get(virtualPath);
		//        if (fs != null) {
		//            Filter filter = fs.IncludeFiles.GetMatchingFilter(filePath);

		//            // If a filter was found ensure that the file is not in the exclude list
		//            if (fs.ExcludeFiles.IsMatching(filePath))
		//                return null; // File should be excluded

		//            // If we found a filter we should return it
		//            if (filter != null)
		//                return filter;
		//        }

		//        // Nothing found, so we step up in the virtual path
		//        virtualPath = PathHelper.GetParentDirectoryPath(virtualPath);
		//    }

		//    return null;		// No filter found
		//}

		/// <summary>
		/// Finds the filter settings to use for the supplied virtual path.
		/// </summary>
		/// <param name="virtualPath">The virtual path of the file.</param>
		internal FilterSettings FindFilterSettings(string virtualPath) {

			while (virtualPath != null) {
				// Look up filter settings for the virtual path
				FilterSettings fs = _filtersettings.Get(virtualPath);
				if (fs != null)
					return fs;

				// Nothing found, so we step up in the virtual path
				virtualPath = PathHelper.GetParentDirectoryPath(virtualPath);
			}

			return null;		// No filter settings found
		}
		
		#endregion

		
	}
}