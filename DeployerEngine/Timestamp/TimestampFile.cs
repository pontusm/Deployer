using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DeployerEngine.Timestamp {
	/// <summary>
	/// Contains settings in a timestamp file.
	/// </summary>
	[XmlRoot("Timestamp")]
	public class TimestampFile {

		private DateTime _lastdeployment;
		private string _deployedby;

		/// <summary>
		/// Contains the time when a deployment was last made.
		/// </summary>
		[XmlAttribute]
		public DateTime LastDeployment {
			get { return _lastdeployment; }
			set { _lastdeployment = value; }
		}

		/// <summary>
		/// Contains the username of the user who last deployed.
		/// </summary>
		[XmlAttribute]
		public string DeployedBy {
			get { return _deployedby; }
			set { _deployedby = value; }
		}

		/// <summary>
		/// Load a timestamp file.
		/// </summary>
		/// <param name="filename">The name and path of the file to load from.</param>
		public static TimestampFile Load(string filename) {
			try {
				XmlSerializer xs = new XmlSerializer(typeof(TimestampFile));
				FileStream fs = File.OpenRead(filename);
				TimestampFile file = xs.Deserialize(fs) as TimestampFile;
				fs.Close();
				if (file == null)
					throw new FileLoadException();

				return file;
			}
			catch (Exception ex) {
				throw new FileLoadException("Unable to load timestamp file. The file may be corrupt.", filename, ex);
			}
		}

		/// <summary>
		/// Save the timestamp file.
		/// </summary>
		/// <param name="filename">The destination filename.</param>
		public void Save(string filename) {
			XmlSerializer xs = new XmlSerializer(typeof(TimestampFile));
			StreamWriter sw = new StreamWriter(filename);
			xs.Serialize(sw, this);
			sw.Close();
		}
	}
}
