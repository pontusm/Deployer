using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Deployer.ProjectSettings.Dialogs;
using DeployerEngine;
using DeployerEngine.Project;
using DeployerPluginInterfaces;

namespace Deployer.ProjectSettings.Views {
	public partial class DestinationsView : UserControl {

		private DeploymentProject _project;

		public DestinationsView() {
			InitializeComponent();
		}

		/// <summary>
		/// Gets or sets the project to edit settings for.
		/// </summary>
		public DeploymentProject Project {
			get { return _project; }
			set { _project = value; }
		}

		#region Event handlers

		#region Control startup/load

		private void DestinationsView_Load(object sender, EventArgs e) {
			if (_project == null)
				return;
			
			RefreshUI();			
		}

		#endregion

		#region Add/remove destination

		private void _addDestination_Click(object sender, EventArgs e) {
			AddDestinationForm dlg = new AddDestinationForm();
			dlg.Project = _project;
			DialogResult result = dlg.ShowDialog(this);
			if(result == DialogResult.Cancel)
				return;

			RefreshUI();
		}

		private void _removeDestination_Click(object sender, EventArgs e) {
			DeployDestination dest = (DeployDestination)_destinations.SelectedItem;
			if (dest != null) {
				_project.ActiveDeployConfig.Destinations.Remove(dest);
				RefreshUI();
			}
		}

		#endregion

		private void _destinationName_Validated(object sender, EventArgs e) {
			DeployDestination dest = (DeployDestination)_destinations.SelectedItem;
			if (dest == null)
				return;

			if (dest.Name != _destinationName.Text) {
				dest.Name = _destinationName.Text;
				_destinations.SuspendLayout();
				RefreshUI();
				_destinations.ResumeLayout();
				_destinations.SelectedItem = dest;
			}
		}
		
		private void _destinations_SelectedIndexChanged(object sender, EventArgs e) {
			UpdateUI();
		}

		private void _plugin_SelectionChangeCommitted(object sender, EventArgs e) {
			DeployDestination dest = (DeployDestination)_destinations.SelectedItem;
			if (dest == null)
				return;

			PluginDescriptor descriptor = _plugin.SelectedItem as PluginDescriptor;
			if (descriptor == null)
				return;

			dest.ChangePlugin(descriptor);
		}

		private void _editPluginSettings_Click(object sender, EventArgs e) {
			DeployDestination dest = (DeployDestination)_destinations.SelectedItem;
			if (dest == null)
				return;
			
			IDeployerPlugin plugin = PluginManager.GetPluginForDestination(dest);
			plugin.ShowConfigureSettingsDialog(this);
		}

		#endregion

		#region Refresh/update UI

		/// <summary>
		/// Reloads the controls with refreshed data from the data model.
		/// </summary>
		private void RefreshUI() {
			// Populate destinations
			_destinations.ValueMember = "Identifier";
			_destinations.DisplayMember = "Name";
			_destinations.DataSource = new ArrayList(_project.ActiveDeployConfig.Destinations);

			// Populate plugin list
			_plugin.ValueMember = "PluginTypeFullName";
			_plugin.DisplayMember = "PluginName";
			_plugin.DataSource = new List<PluginDescriptor>( PluginManager.GetPluginDescriptors(pd => pd.SupportsFileDeploy) );

			UpdateUI();
		}

		/// <summary>
		/// Updates the UI for the current control state.
		/// </summary>
		private void UpdateUI() {
			DeployDestination dest = (DeployDestination) _destinations.SelectedItem;
			if (dest != null) {
				_destinationName.Text = dest.Name;
				foreach(PluginDescriptor descriptor in _plugin.Items) {
					if(descriptor.PluginTypeFullName == dest.PluginSettings.Type) {
						_plugin.SelectedItem = descriptor;
						break;
					}
				}

				_plugin.Enabled = true;
				_editPluginSettings.Enabled = true;
				_removeDestination.Enabled = true;
			}
			else {
				_destinationName.Text = null;
				_plugin.Enabled = false;
				_editPluginSettings.Enabled = false;
				_removeDestination.Enabled = false;
			}
		}

		#endregion



	}
}
