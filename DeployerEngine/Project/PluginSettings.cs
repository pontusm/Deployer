using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace DeployerEngine.Project
{
	/// <summary>
	/// Contains settings for a plugin.
	/// </summary>
	/// <remarks>
	/// 2005-01-17	POMU: Class Created
	/// </remarks>
	[Serializable]
	public class PluginSettings : ISerializable
	{
		#region Private members
		private string _identifier;
		private string _type;

		private XmlElement _settings;
		#endregion

		#region Constructors

		public PluginSettings() {
			LoadSettings("<Settings/>");
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="PluginSettings"/> class.
		/// </summary>
		/// <param name="descriptor">Describes the plugin to store settings for.</param>
		public PluginSettings(PluginDescriptor descriptor) : this() {
			_type = descriptor.PluginTypeFullName;
		}
		
		//public PluginSettings(string identifier) : this() {
		//    _identifier = identifier;
		//}

		#region Serialization

		// Serialization constructor
		protected PluginSettings(SerializationInfo info, StreamingContext context) {
			_identifier = info.GetString("id");
			_type = info.GetString("type");
			LoadSettings(info.GetString("settings"));
		}

		private void LoadSettings(string settings) {
			XmlDocument xd = new XmlDocument();
			xd.LoadXml(settings);
			_settings = xd.DocumentElement;
		}
		
		///<summary>
		///Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> with the data needed to serialize the target object.
		///</summary>
		///
		///<param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"></see>) for this serialization. </param>
		///<param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> to populate with data. </param>
		///<exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("id", _identifier);
			info.AddValue("type", _type);
			info.AddValue("settings", _settings.OuterXml);
		}

		#endregion
		
		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the identifier for the plugin which owns the settings.
		/// OBSOLETE! Use Type instead.
		/// </summary>
		[XmlAttribute("ID")]
		public string Identifier {
			get { return _identifier; }
			set {
				_identifier = value;
			}
		}

		/// <summary>
		/// Contains the settings for the plugin.
		/// </summary>
		[XmlElement("Configuration")]
		public XmlElement Settings {
			get { return _settings; }
			set { _settings = value; }
		}
		
		/// <summary>
		/// The full type name for the plugin.
		/// </summary>
		[XmlAttribute("Type")]
		public string Type {
			get { return _type; }
			set { _type = value; }
		}

		#endregion

		
	}
}
