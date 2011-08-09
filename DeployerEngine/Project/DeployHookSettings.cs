using System;
using System.Xml.Serialization;

namespace DeployerEngine.Project
{
	/// <summary>
	/// Contains settings for a deployment hook.
	/// </summary>
	/// <remarks>
	/// 2010-07-06  POMU: Class created
	/// </remarks>
	[Serializable]
	public class DeployHookSettings
	{
		private PluginSettings _pluginsettings = new PluginSettings();

		[XmlAttribute]
		public string Identifier { get; set; }

		[XmlElement("Plugin")]
		public PluginSettings Pluginsettings
		{
			get { return _pluginsettings; }
			set { _pluginsettings = value; }
		}
	}
}