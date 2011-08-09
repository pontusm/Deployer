using System;
using System.Xml.Serialization;

namespace DeployerEngine.Project
{
	/// <summary>
	/// Describes how to connect to a database via the ServerDatabase web service.
	/// </summary>
	/// <remarks>
	/// 2005-03-30	POMU: Class Created
	/// </remarks>
	[Serializable]
	public class DatabaseDescriptor
	{
		#region Private members
		private string _name;
		private string _url;
		#endregion

		#region Constructors
		public DatabaseDescriptor()
		{
		}

		public DatabaseDescriptor(string name, string url) {
			_name = name;
			_url = url;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the name of the database.
		/// </summary>
		[XmlAttribute]
		public string Name {
			get { return _name; }
			set {
				if(_name != value) {
					_name = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the URL for the web service where the database can be found.
		/// </summary>
		[XmlAttribute]
		public string Url {
			get { return _url; }
			set {
				if(_url != value) {
					_url = value;
				}
			}
		}

		#endregion

		#region Public methods

		public override bool Equals(object obj) {
			DatabaseDescriptor descriptor = obj as DatabaseDescriptor;
			if(descriptor == null)
				return false;

			return descriptor.Name == descriptor.Name && descriptor.Url == descriptor.Url;
		}

		public override int GetHashCode() {
			string hash = _name + _url;
			return hash.GetHashCode();
		}

		#endregion
	}
}
