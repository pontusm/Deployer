using System;
using System.IO;
using DeployerEngine.Project;

namespace DeployerEngine {
	/// <summary>
	/// Represents a file in a deploy file structure.
	/// </summary>
	public class DeploymentFile {
		#region Private members

		private string _localpath;
		private string _remotepath;
		//private string _localname;
		private string _remotename;
		private bool _includeInDeployment = true;
		private string _deployDestinationIdentifier;
		private Filter _matchingFilter;

		#endregion

		#region Constructor

		public DeploymentFile(string localpath, string remotepath, Filter matchingFilter) {
			_localpath = Path.GetFullPath(localpath);
			_remotepath = remotepath;
			_matchingFilter = matchingFilter;
			//_localname = Path.GetFileName(_localpath);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets whether to include the file in the deployment or not.
		/// </summary>
		public bool IncludeInDeployment {
			get { return _includeInDeployment; }
			set { _includeInDeployment = value; }
		}

		/// <summary>
		/// Contains the destination to deploy the file to.
		/// </summary>
		public string DeployDestinationIdentifier {
			get { return _deployDestinationIdentifier; }
			set { _deployDestinationIdentifier = value; }
		}

		/// <summary>
		/// Gets the filter that was used to find the file.
		/// </summary>
		public Filter MatchingFilter {
			get { return _matchingFilter; }
		}

		/// <summary>
		/// Gets the local filename without the path.
		/// </summary>
		public string Name {
			get { return Path.GetFileName(_localpath); }
		}


		/// <summary>
		/// Gets the local path to the file.
		/// </summary>
		public string LocalPath {
			get { return _localpath; }
		}

		/// <summary>
		/// If this is filled in the file will use this name on the remote location.
		/// </summary>
		public string RemoteName {
			get { return _remotename; }
			set { _remotename = value; }
		}

		/// <summary>
		/// Gets or sets the remote path for the file.
		/// </summary>
		public string RemotePath {
			get { return _remotepath; }
			set { _remotepath = value; }
		}

		/// <summary>
		/// Gets the size of the file in bytes.
		/// </summary>
		public long Size {
			get { return new FileInfo(_localpath).Length; }
		}

		#endregion

		/// <summary>
		/// Returns the date and time the file was last written to.
		/// </summary>
		public DateTime GetLastWriteTime() {
			return File.GetLastWriteTime(_localpath);
		}
	}
}