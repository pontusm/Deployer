using System;
using System.IO;

namespace DeployerPluginInterfaces
{
	/// <summary>
	/// Contains information about a file transfer.
	/// </summary>
	[Serializable]
	public class TransferEventArgs : EventArgs {
		private string _filename;
		private long _bytesSent = 0;
		private long _totalBytes;

		public TransferEventArgs(string filename) {
			_filename = filename;

			FileInfo fi = new FileInfo(filename);
			_totalBytes = fi.Length;
		}

		/// <summary>
		/// Gets the amount of bytes transferred so far.
		/// </summary>
		public long BytesSent {
			get { return _bytesSent; }
			set { _bytesSent = value; }
		}

		/// <summary>
		/// Gets the filename of the file being transferred.
		/// </summary>
		public string Filename {
			get { return _filename; }
		}

		/// <summary>
		/// Gets the total bytes in the file.
		/// </summary>
		public long TotalBytes {
			get { return _totalBytes; }
		}
	}
}
