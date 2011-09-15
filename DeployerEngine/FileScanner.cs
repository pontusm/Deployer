using System;
using System.IO;
using System.Web;
using System.Xml;
using DeployerEngine.Project;
using DeployerEngine.Util;

namespace DeployerEngine
{
	/// <summary>
	/// Scans files that match a certain filter.
	/// </summary>
	internal class FileScanner
	{
		#region Private members

		//private FilterSettings _filtersettings;
		private DeploymentProject _project;
		private DeployConfig _config;

		/// <summary>
		/// Contains the current structure that is being built.
		/// </summary>
		private DeploymentStructure _structure;
		
		#endregion

		#region Scan directories and files

		/// <summary>
		/// Scans a directory and all subdirectory and returns all the files found that match the scan filter.
		/// If we are to use project filter, files are read from project instead of directory.
		/// </summary>
		/// <param name="project">The current project settings.</param>
		/// <param name="modifiedSince">Only return files modified after this date.</param>
		/// <returns>A structure with the files that were found.</returns>
		internal DeploymentStructure FindFiles(DeploymentProject project, DateTime modifiedSince) {
			// These members are used for easy access during scanning
			_project = project;
			_config = project.ActiveDeployConfig;
			if (_config == null)
				throw new ApplicationException("The project has no active deploy configuration.");
			
			// Build structure of matching files
			_structure = new DeploymentStructure();

			if (_config.UseProjectFilter) {
				string projectFilename = project.GetVisualStudioProjectFileName();
				if (projectFilename == null)
					throw new ApplicationException("Could not find a Visual Studio project file in the root directory.");

				ScanProjectFile(projectFilename, project.LocalPathAbsolute, modifiedSince);
			}
			else
				ScanDirectoryRecurse(project.LocalPathAbsolute, string.Empty, modifiedSince);

			return _structure;
		}

		/// <summary>
		/// Scans a directory and all subdirectories and fills an arraylist with all files matching the current filter.
		/// </summary>
		private void ScanDirectoryRecurse(string localPath, string virtualPath, DateTime modifiedSince) {

			// Find files matching the pattern
			string[] files = Directory.GetFiles(localPath);
			foreach (string file in files) {
				AddFile(file, virtualPath, modifiedSince);
			}

			// Scan sub directories
			string[] dirs = Directory.GetDirectories(localPath);
			foreach (string dir in dirs) {
				DirectoryInfo dirinfo = new DirectoryInfo(dir);
				string virtualSubPath = Path.Combine(virtualPath, dirinfo.Name);

				// Ensure dir is not in exclude list
				if (CanIncludeDirectory(virtualSubPath, false)) {
					// Scan and add files in the sub directory
					ScanDirectoryRecurse(dir, virtualSubPath, modifiedSince);
				}
			}
		}
		
		#endregion

		#region Deployment structure building

		/// <summary>
		/// Tries to add a file to the specified deploymentstructure...
		/// </summary>
		private void AddFile(string localFilePath, string virtualPath, DateTime modifiedSince) {
			if (!File.Exists(localFilePath))
				return;

			if (File.GetLastWriteTime(localFilePath) >= modifiedSince) {
				virtualPath = virtualPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);		// Remove trailing slash

				// Check if file matches any of the filters
				//string filePath = Path.Combine(virtualPath, Path.GetFileName(localFilePath));
				//Filter filter = _filtersettings.IncludeFiles.GetMatchingFilter(filePath);
				FilterSettings fs = _config.FindFilterSettings(virtualPath);
				if (fs == null)
					throw new ApplicationException(string.Format("No filter settings found for path '{0}'", virtualPath));

				// This is the path that will be matched
				string filePath = Path.Combine(virtualPath, Path.GetFileName(localFilePath));

				// Ensure that we are matching relative to the virtual path of the current filter settings
				string pathToMatch = filePath.Substring(fs.VirtualPath.Length).TrimStart('\\');

				// Is file in the exclude list?
				if (fs.ExcludeFiles.IsMatching(pathToMatch))
					return;
				
				// Try to find a matching filter
				Filter filter = fs.IncludeFiles.GetMatchingFilter(pathToMatch);
				if (filter != null) {
					// A matching filter was found, which means the file should be included
					if (filter.RemotePath != null)
						virtualPath = filter.RemotePath;		// Filter overrides remote path
					else
						virtualPath = fs.BuildRelativeRemotePath(virtualPath);

					DeploymentFile df = new DeploymentFile(localFilePath, virtualPath, filter);
					df.DeployDestinationIdentifier = filter.DeployDestinationIdentifier;

					// Does filter want a specific remote file name? (Only supported for exact matches)
					if (!string.IsNullOrEmpty(filter.RemoteFileName) && filter.ExpressionType == FilterExpressionType.ExactFileName)
						df.RemoteName = filter.RemoteFileName;

					_structure.Add(df);
				}
			}
		}

		#endregion
		
		#region Private methods

		/// <summary>
		/// Checks if a directory should be included in deployment.
		/// </summary>
		/// <param name="virtualPath">The virtual path of the directory (relative to the root path).</param>
		private bool CanIncludeDirectory(string virtualPath, bool checkParentDirectories) {

			while(virtualPath.Length > 0) {
				virtualPath = virtualPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);		// Remove trailing slash

				if (!_config.CanIncludeDirectory(virtualPath))
					return false;

				if(!checkParentDirectories)
					break;

				// Step up to parent
				virtualPath = PathHelper.GetParentDirectoryPath(virtualPath);
			}

			return true;
		}


		/// <summary>
		/// Scans files from projectfile instead of directory.
		/// </summary>
		private void ScanProjectFile(string projectFilename, string localPath, DateTime modifiedSince) {

			XmlTextReader reader = null;

			try { //Load files from project file
				reader = new XmlTextReader(projectFilename);

				bool once = true;
				
				while(reader.Read()) {
					if(reader.NodeType == XmlNodeType.Element) {
						switch(reader.Name) {
							//VS2005 element
							case "OutputPath":
								//Little ugly solution with the once flag, but it works for now.
								if(once) {
									string relativePath0 = reader.ReadElementContentAsString();
									AddFolderFromRelativePath(_structure, relativePath0, localPath, modifiedSince);
									once = false;
								}
								
								break;
							
							//VS2005 element
							case "None":
							case "Content":
								string relativePath1 = HttpUtility.UrlDecode(reader.GetAttribute("Include"));
								AddFileFromRelativePath(_structure, relativePath1, localPath, modifiedSince);
								break;
							
							//VS2003 element
							case "File":
								string relativePath2 = reader.GetAttribute("RelPath");
								AddFileFromRelativePath(_structure, relativePath2, localPath, modifiedSince);
								break;
							
							//VS2003 element
							case "Config":
								//Try to get all files in bin/ folder
								if(reader.GetAttribute("Name").ToLower().CompareTo("release") == 0) {
									string relativePath3 = reader.GetAttribute("OutputPath");
									AddFolderFromRelativePath(_structure, relativePath3, localPath, modifiedSince);
								}
								break;
						}
					}
				}
			}
			finally {
				if(reader != null)
					reader.Close();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="currentStructure"></param>
		/// <param name="relativePath"></param>
		/// <param name="localPath"></param>
		/// <param name="modifiedSince"></param>
		private void AddFileFromRelativePath(DeploymentStructure currentStructure, string relativePath, string localPath, DateTime modifiedSince) {
			
			string virtualPath = string.Empty;

			try {
				int endpos = relativePath.LastIndexOf('\\');

				if(endpos != -1)
					virtualPath	= relativePath.Substring(0, endpos);	
			} 
			catch {}
									
			string filePath = Path.Combine(localPath, relativePath);
								
			if(CanIncludeDirectory(virtualPath, true)) {

				//Try to add file
				AddFile(filePath, virtualPath, modifiedSince);
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="currentStructure"></param>
		/// <param name="relativePath"></param>
		/// <param name="localPath"></param>
		/// <param name="modifiedSince"></param>
		private void AddFolderFromRelativePath(DeploymentStructure currentStructure, string relativePath, string localPath, DateTime modifiedSince) {
			
			string folderPath = Path.Combine(localPath, relativePath);

			if(CanIncludeDirectory(relativePath, true) && Directory.Exists(folderPath)) {

				string[] files = Directory.GetFiles(folderPath);

				foreach(string file in files) {
					//Try to add file
					AddFile(file, relativePath, modifiedSince);
				}	
			}
		}

		#endregion
	}
}
