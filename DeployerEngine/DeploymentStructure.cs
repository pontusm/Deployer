using System;
using System.Collections.Generic;
using System.IO;
using DeployerEngine.Project;

namespace DeployerEngine
{
	/// <summary>
	/// Represents a structure of files that should be deployed.
	/// </summary>
	public class DeploymentStructure {
		#region Private members

		//private ArrayList _files = new ArrayList();
		private List<DeploymentFile> _files = new List<DeploymentFile>();

		private SortedList<string, DeploymentFolder> _folders = new SortedList<string, DeploymentFolder>(StringComparer.InvariantCultureIgnoreCase);

		#endregion

		#region Constructor

		public DeploymentStructure() {
//			_localpath = localpath;

//			DirectoryInfo dirinfo = new DirectoryInfo(localpath);
//			_name = dirinfo.Name;
		}

		#endregion

		#region Properties

//		/// <summary>
//		/// Gets a collection of DeployDirectory entries for the directory.
//		/// </summary>
//		public ArrayList Directories {
//			get { return _directories; }
//		}

		/// <summary>
		/// Gets a collection of DeployFile entries in the structure.
		/// </summary>
		public IList<DeploymentFile> Files {
			get { return _files; }
		}

		/// <summary>
		/// Gets the dictionary with DeployFolder objects.
		/// </summary>
		public IDictionary<string, DeploymentFolder> Folders {
			get { return _folders; }
		}

//		/// <summary>
//		/// Gets the path to the directory.
//		/// </summary>
//		public string LocalPath {
//			get { return _localpath; }
//		}
//
//		/// <summary>
//		/// Gets the name of the directory.
//		/// </summary>
//		public string Name {
//			get { return _name; }
//		}

		#endregion

		#region Public methods

		public void Add(DeploymentFile file) {
			string folderpath = Path.GetDirectoryName(file.LocalPath);
			DeploymentFolder folder;
			if (_folders.ContainsKey(folderpath))
				folder = _folders[folderpath];
			else {
				folder = new DeploymentFolder(folderpath);
				_folders.Add(folderpath, folder);
			}

			folder.Files.Add(file);

			_files.Add(file);
		}

		//public void Add(DeploymentFolder folder) {
		//    _folders.Add(folder.FullPath, folder);
		//}

		/// <summary>
		/// Returns all deployment files for a directory.
		/// </summary>
		public IList<DeploymentFile> GetFilesInDirectory(string directoryPath) {
			if (!_folders.ContainsKey(directoryPath))
				return new List<DeploymentFile>();			// Return empty list

			return _folders[directoryPath].Files;
			//List<DeploymentFile> files = new List<DeploymentFile>(_files.Count);
			//foreach(DeploymentFile file in _files) {
			//    string path = Path.GetDirectoryName(file.LocalPath);
			//    if(string.Compare(path, directoryPath, true) == 0)
			//        files.Add(file);
			//}

			//return files.ToArray();
		}

		/// <summary>
		/// Returns all deployment files for a directory in a dictionary with the
		/// filename as the key.
		/// </summary>
		public IDictionary<string, DeploymentFile> GetFilesInDirectoryAsTable(string directoryPath) {
			IList<DeploymentFile> filelist = GetFilesInDirectory(directoryPath);
			Dictionary<string, DeploymentFile> filetable = new Dictionary<string, DeploymentFile>(filelist.Count);
			foreach (DeploymentFile file in filelist) {
				string key = file.LocalPath.ToLower();
				if (filetable.ContainsKey(key)) {
					throw new ArgumentException(string.Format("Unable to add duplicate file '{0}' to list. A file with that name already exists.", file.LocalPath));
				}
				filetable.Add(key, file);
			}
			return filetable;
		}

		/// <summary>
		/// Returns the total amount of files that should be deployed.
		/// </summary>
		/// <returns></returns>
		public int GetTotalFilesToDeploy() {
			int count = 0;
			foreach(DeploymentFile file in _files) {
				if(file.IncludeInDeployment)
					count++;
			}
			return count;
		}
		
		#endregion
	}

}
