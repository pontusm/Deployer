

using System;
using System.IO;

namespace DeployerEngine.Util {
	/// <summary>
	/// Contains some helpful path methods.
	/// </summary>
	/// <remarks>
	/// 2006-10-12	POMU: Class created 
	/// </remarks>
	public static class PathHelper {

		/// <summary>
		/// Gets the path for the parent directory or null if there is no parent.
		/// Trailing slash will be removed from the return path.
		/// </summary>
		public static string GetParentDirectoryPath(string path) {
			if (string.IsNullOrEmpty(path))
				return null;

			path = path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);		// Remove trailing slash
			int pos = path.LastIndexOfAny(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
			if (pos == -1)
				return string.Empty;	// Root dir

			return path.Substring(0, pos);
		}
		
		/// <summary>
		/// Retrieves the relative path to a location.
		/// </summary>
		/// <param name="currentpath">The path to go from.</param>
		/// <param name="destinationpath">The path that the relative path should point to.</param>
		public static string GetRelativePath(string currentpath, string destinationpath) {
			Uri currpath = new Uri(currentpath);
			Uri destpath = new Uri(destinationpath);
			return currpath.MakeRelativeUri(destpath).ToString();
		}

		/// <summary>
		/// Retrieves the virtual path to a file from a specific root path.
		/// The root path combined with the virtual path is the full path to the file.
		/// </summary>
		/// <param name="filepath">The path to a file to examine.</param>
		/// <param name="rootpath">The root path to remove from the file path.</param>
		public static string GetVirtualPath(string filepath, string rootpath) {
			if (filepath.IndexOf(rootpath, StringComparison.InvariantCultureIgnoreCase) != 0)
				throw new ApplicationException("Unable to extract virtual path (bug?)");

			return filepath.Substring(rootpath.Length);
		}
	}
}