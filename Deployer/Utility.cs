using System;
using System.Collections.Generic;
using System.Text;

namespace Deployer {
	/// <summary>
	/// Contains utility methods.
	/// </summary>
	internal static class Utility {
		/// <summary>
		/// Formats the supplied file size as a string.
		/// </summary>
		/// <param name="filesize"></param>
		/// <returns></returns>
		internal static string FormatFileSize(long filesize) {
			return string.Format("{0:0} KB", Math.Ceiling((double)filesize / 1024.0));
		}
	}
}
