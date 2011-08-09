using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DeployerEngine;
using DeployerEngine.Project;

namespace Deployer.ProjectSettings.Dialogs {
	public partial class AddDestinationForm : Form {

		private DeploymentProject _project;

		public AddDestinationForm() {
			InitializeComponent();
		}

		/// <summary>
		/// Gets or sets the project to edit settings for.
		/// </summary>
		public DeploymentProject Project {
			get { return _project; }
			set { _project = value; }
		}

		#region Startup/load

		private void AddDestinationForm_Load(object sender, EventArgs e) {

			// Populate plugin list
			_plugin.ValueMember = "PluginTypeFullName";
			_plugin.DisplayMember = "PluginName";
			_plugin.DataSource = PluginManager.GetPluginDescriptors();

		}

		#endregion

		private void _ok_Click(object sender, EventArgs e) {
			if(string.IsNullOrEmpty(_destinationName.Text)) {
				MessageBox.Show(this, "Please enter a destination name.", "Destination name missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
				_destinationName.Focus();
				return;
			}
			else if (_project.ActiveDeployConfig.Destinations.Contains(_destinationName.Text)) {
				MessageBox.Show(this, "The destination name is already in use. Please enter a different name.", "Destination name exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
				_destinationName.Focus();
				return;
			}

			DeployDestination dest = new DeployDestination(_destinationName.Text, _destinationName.Text);
			PluginDescriptor plugin = (PluginDescriptor)_plugin.SelectedItem;
			dest.PluginSettings = new PluginSettings(plugin);
			_project.ActiveDeployConfig.Destinations.Add(dest);

			this.DialogResult = DialogResult.OK;
		}
	}
}