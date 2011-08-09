using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DeployerEngine.Project;

namespace Deployer.ProjectSettings.Views {
	public partial class TimestampView : UserControl {

		private DeploymentProject _project;

		public TimestampView() {
			InitializeComponent();
		}

		/// <summary>
		/// Gets or sets the project to edit settings for.
		/// </summary>
		public DeploymentProject Project {
			get { return _project; }
			set { _project = value; }
		}
		
		private void TimestampView_Load(object sender, EventArgs e) {
			if (_project == null)
				return;

			string identifier = _project.ActiveDeployConfig.TimestampDestinationIdentifier;
			
			_timestampDestination.ValueMember = "Identifier";
			_timestampDestination.DisplayMember = "Name";
			_timestampDestination.DataSource = _project.ActiveDeployConfig.Destinations;

			if (!string.IsNullOrEmpty(identifier)) {
				_enableTimestamping.Checked = true;
				_timestampDestination.SelectedItem = _project.ActiveDeployConfig.Destinations.Get(identifier);
			}
			else {
				_enableTimestamping.Checked = false;
				_timestampDestination.Enabled = false;
			}
		}

		private void _timestampDestination_SelectedIndexChanged(object sender, EventArgs e) {
			_project.ActiveDeployConfig.TimestampDestinationIdentifier = (string) _timestampDestination.SelectedValue;
		}

		private void _enableTimestamping_CheckedChanged(object sender, EventArgs e) {
			_timestampDestination.Enabled = _enableTimestamping.Checked;

			if(!_enableTimestamping.Checked) {
				_project.ActiveDeployConfig.TimestampDestinationIdentifier = null;
			}
		}
	}
}
