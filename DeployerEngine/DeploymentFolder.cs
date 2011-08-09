using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DeployerEngine {

	[Flags]
	public enum FolderFlags {
		None = 0,
		HasDeployFiles = 1,
		HasModifiedFiles = 2
	}

	/// <summary>
	/// Contains information about a folder in the deployment structure.
	/// </summary>
	/// <remarks>
	/// 2007-04-11 POMU: Class created
	/// </remarks>
	public class DeploymentFolder {
		#region Private members

		private string _fullpath;

		//private SortedList<string, DeploymentFolder> _childfolders = new SortedList<string, DeploymentFolder>();

		private List<DeploymentFile> _files = new List<DeploymentFile>();

		#endregion

		#region Constructor

		public DeploymentFolder(string fullpath) {
			_fullpath = fullpath;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the sub folders.
		/// </summary>
		//public IDictionary<string, DeploymentFolder> ChildFolders {
		//    get { return _childfolders; }
		//}

		/// <summary>
		/// Gets the name of the folder.
		/// </summary>
		public string Name {
			get {
				return Path.GetDirectoryName(_fullpath);
			}
		}

		/// <summary>
		/// Gets a collection of DeployFile entries in the folder.
		/// </summary>
		public IList<DeploymentFile> Files {
			get { return _files; }
		}

		/// <summary>
		/// Gets the full path for the folder.
		/// </summary>
		public string FullPath {
			get { return _fullpath; }
		}

		#endregion

		/// <summary>
		/// Returns the status of the folder and its sub folders.
		/// </summary>
		public FolderFlags GetFolderStatus() {
			return FolderFlags.None;
		}
	}
}