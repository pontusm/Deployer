using System;
using System.Windows.Forms;

namespace DeployerPluginInterfaces
{
	/// <summary>
	/// Common interface for plugins to the Deployer engine.
	/// </summary>
	public interface IDeployerPlugin
	{
		/// <summary>
		/// An identifier for the plugin.
		/// </summary>
		string PluginIdentifier { get; }

		/// <summary>
		/// A friendly name for the plugin that is shown to the user.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The current version of the plugin.
		/// </summary>
		Version Version { get; }

		/// <summary>
		/// Loads settings for the plugin.
		/// </summary>
		/// <param name="settings">A string containing the settings.</param>
		void LoadSettings(string settings);

		/// <summary>
		/// Save the settings for the plugin in a string.
		/// </summary>
		string SaveSettings();

		/// <summary>
		/// Show the dialog used to configure the plugin settings. The current settings are passed in to
		/// the method, and should be updated with any changes that the user makes in the dialog.
		/// </summary>
		/// <param name="owner">The owner window to show the dialog in front of.</param>
		DialogResult ShowConfigureSettingsDialog(IWin32Window owner);
	}
}
