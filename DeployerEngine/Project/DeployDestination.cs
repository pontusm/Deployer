using System;
using System.Xml.Serialization;

namespace DeployerEngine.Project {
	/// <summary>
	/// Contains a destination to deploy to.
	/// </summary>
	/// <remarks>
	/// 2006-10-12	POMU: Class created 
	/// </remarks>
	[Serializable]
	public class DeployDestination {
		private string _identifier;
		private string _name;
		private PluginSettings _pluginsettings = new PluginSettings();

		public DeployDestination() {}

		public DeployDestination(string identifier, string name) {
			_identifier = identifier;
			_name = name;
		}

		public DeployDestination(string identifier) {
			_identifier = identifier;
		}

		[XmlAttribute]
		public string Identifier {
			get { return _identifier; }
			set { _identifier = value; }
		}

		[XmlAttribute]
		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		[XmlElement("Plugin")]
		public PluginSettings PluginSettings {
			get { return _pluginsettings; }
			set { _pluginsettings = value; }
		}

		/// <summary>
		/// Changes the plugin to use for the destination.
		/// </summary>
		/// <param name="descriptor">A descriptor for the plugin.</param>
		public void ChangePlugin(PluginDescriptor descriptor) {
			if (descriptor == null) throw new ArgumentNullException();

			if(descriptor.PluginTypeFullName == _pluginsettings.Type)
				return;

			// Unload old plugin and create empty settings for the new one
			PluginManager.UnloadPluginForDestination(this);
			_pluginsettings = new PluginSettings(descriptor);

		}
	}
}