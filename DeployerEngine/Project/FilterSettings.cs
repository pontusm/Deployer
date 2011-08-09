using System;
using System.IO;
using System.Xml.Serialization;

namespace DeployerEngine.Project
{
	/// <summary>
	/// File filter settings.
	/// </summary>
	[Serializable]
	public class FilterSettings
	{
		private FilterCollection _includeFiles = new FilterCollection();
		private FilterCollection _excludeDirectories = new FilterCollection();
		private FilterCollection _excludeFiles = new FilterCollection();

		// TODO: This is only kept for backwards compatibility
		private FilterCollection _excludeProcedures;

		private string _virtualPath = string.Empty;
		
		private string _useProjectFilter;

		public FilterSettings()
		{
		}

		public FilterSettings(string virtualPath) {
			VirtualPath = virtualPath;
		}
		
		// TODO: This is only kept for backwards compatibility
		public string UseProjectFilter {
			get { return _useProjectFilter; }
			set { _useProjectFilter = value; }
		}

		/// <summary>
		/// Gets a collection of filters for the files to include.
		/// </summary>
		public FilterCollection IncludeFiles {
			get { return _includeFiles; }
		}
		
		/// <summary>
		/// Gets a collection of filters for the directories to exclude.
		/// </summary>
		public FilterCollection ExcludeDirectories {
			get { return _excludeDirectories; }
		}

		/// <summary>
		/// Gets a collection of filters for the files to exclude.
		/// </summary>
		public FilterCollection ExcludeFiles {
			get { return _excludeFiles; }
		}

		/// <summary>
		/// Gets a collection of filters for the stored procedures to exclude.
		/// </summary>
		public FilterCollection ExcludeProcedures {
			get { return _excludeProcedures; }
			set { _excludeProcedures = value; }
		}

		/// <summary>
		/// Contains the virtual path for which the filter settings should be applied.
		/// </summary>
		[XmlAttribute]
		public string VirtualPath {
			get { return _virtualPath; }
			set { _virtualPath = value.Trim('\\', '/'); }
		}

		/// <summary>
		/// Returns the display name for the filter settings.
		/// </summary>
		public override string ToString() {
			if (string.IsNullOrEmpty(_virtualPath))
				return "<Global>";
			
			return _virtualPath;
		}

		/// <summary>
		/// Creates a remote path that is relative to the virtual path
		/// for the filter settings.
		/// </summary>
		public string BuildRelativeRemotePath(string virtualPath) {
			if (_virtualPath.Length > 0 && virtualPath.StartsWith(_virtualPath, StringComparison.InvariantCultureIgnoreCase))
				virtualPath = virtualPath.Substring(_virtualPath.Length).Trim('/', '\\');
			
			return virtualPath;
		}
	}

}
