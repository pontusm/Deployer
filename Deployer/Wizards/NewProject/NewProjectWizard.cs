using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CristiPotlog.Controls;
using DeployerEngine;
using DeployerEngine.Project;
using DeployerEngine.Util;
using DeployerPluginInterfaces;

namespace Deployer.Wizards.NewProject {
	public partial class NewProjectWizard : Form {

		private DeploymentProject _project;

		public NewProjectWizard() {
			InitializeComponent();

			// Init new project
			_project = new DeploymentProject();
			_project.InitNewProject();
		}

		/// <summary>
		/// Gets the project being edited.
		/// </summary>
		public DeploymentProject Project {
			get { return _project; }
		}

		#region Form startup/load

		private void NewProjectWizard_Load(object sender, EventArgs e) {
			_wizard.NextEnabled = false;

			// Add an empty destination
			string id = _project.ActiveDeployConfig.Destinations.GetNextIdentifier();
			DeployDestination dest = new DeployDestination(id);
			_project.ActiveDeployConfig.Destinations.Add(dest);

			PluginDescriptor[] pluginDescriptors = PluginManager.GetPluginDescriptors();

			// Default to use the first available plugin
			if(pluginDescriptors.Length > 0)
				dest.ChangePlugin(pluginDescriptors[0]);

			// Populate plugin list
			_plugin.ValueMember = "PluginTypeFullName";
			_plugin.DisplayMember = "PluginName";
			_plugin.DataSource = pluginDescriptors;
		}

		#endregion


		#region Wizard events

		/// <summary>
		/// Occurs before the wizard switches pages.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="CristiPotlog.Controls.Wizard.BeforeSwitchPagesEventArgs"/> instance containing the event data.</param>
		private void _wizard_BeforeSwitchPages(object sender, Wizard.BeforeSwitchPagesEventArgs e) {
			WizardPage oldpage = _wizard.Pages[e.OldIndex];

			if(oldpage == _pageSourcePath) {
				if(!Directory.Exists(_localpath.Text)) {
					MessageBox.Show(this, string.Format("Directory '{0}' does not exist.", _localpath.Text), "Directory not found",
					                MessageBoxButtons.OK, MessageBoxIcon.Error);
					_localpath.Focus();
					e.Cancel = true;
				}
			}
			else if(oldpage == _pageDestination) {
				if(_destinationName.Text.Length == 0) {
					MessageBox.Show(this, "Please enter a name for the destination.", "No destination name",
									MessageBoxButtons.OK, MessageBoxIcon.Error);
					_destinationName.Focus();
					e.Cancel = true;
				}
			}
		}

		/// <summary>
		/// Occurs after the wizard switches the page.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="CristiPotlog.Controls.Wizard.AfterSwitchPagesEventArgs"/> instance containing the event data.</param>
		private void _wizard_AfterSwitchPages(object sender, Wizard.AfterSwitchPagesEventArgs e) {
			WizardPage newpage = _wizard.Pages[e.NewIndex];

			if(newpage == _pageSourcePath) {
				_wizard.NextEnabled = !string.IsNullOrEmpty(_localpath.Text);
			}
			else if(newpage == _pageDestination) {
				_wizard.NextEnabled = !string.IsNullOrEmpty(_pageDestination.Text) && _plugin.SelectedItem != null;
			}
		}

		#endregion

		#region Step: Source path

		private void _localpath_TextChanged(object sender, EventArgs e) {
			_wizard.NextEnabled = !string.IsNullOrEmpty(_localpath.Text);
		}

		private void _browse_Click(object sender, EventArgs e) {
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.Description = "Please select a directory";
			if (!string.IsNullOrEmpty(_localpath.Text))
				dlg.SelectedPath = _localpath.Text;

			if (dlg.ShowDialog(this) == DialogResult.Cancel)
				return;

			string newpath = dlg.SelectedPath;

			_localpath.Text = newpath;
		}

		#endregion

		#region Step: Destination

		private void _destinationName_TextChanged(object sender, EventArgs e) {
			_wizard.NextEnabled = !string.IsNullOrEmpty(_destinationName.Text) && _plugin.SelectedItem != null;
		}

		private void _plugin_SelectionChangeCommitted(object sender, EventArgs e) {
			_wizard.NextEnabled = !string.IsNullOrEmpty(_destinationName.Text) && _plugin.SelectedItem != null;

			PluginDescriptor descriptor = _plugin.SelectedItem as PluginDescriptor;
			if (descriptor != null)
				_project.ActiveDeployConfig.Destinations[0].ChangePlugin(descriptor);
		}

		private void _editPluginSettings_Click(object sender, EventArgs e) {
			PluginDescriptor descriptor = _plugin.SelectedItem as PluginDescriptor;
			if (descriptor == null)
				return;

			//PluginManager.LoadPlugins(_project);
			IDeployerPlugin plugin = PluginManager.GetPluginForDestination(_project.ActiveDeployConfig.Destinations[0]);
			plugin.ShowConfigureSettingsDialog(this);
		}


		#endregion

		private void _wizard_Finish(object sender, EventArgs e) {
			// Fill in missing information
			_project.LocalPath = _localpath.Text;
			_project.ActiveDeployConfig.Destinations[0].Name = _destinationName.Text;

			// Setup filters to point to our single destination
			DeployDestination dest = _project.ActiveDeployConfig.Destinations[0];
			foreach(FilterSettings fs in _project.ActiveDeployConfig.FilterSettings) {
				foreach(Filter f in fs.IncludeFiles) {
					f.DeployDestinationIdentifier = dest.Identifier;
				}
			}
		}




	}
}