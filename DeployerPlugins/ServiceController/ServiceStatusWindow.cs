using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DeployerPlugins
{
	public partial class ServiceStatusWindow : Form
	{
		public ServiceStatusWindow()
		{
			InitializeComponent();
		}

		public string StatusMessage
		{
			get { return lblStatus.Text; }
			set { lblStatus.Text = value; }
		}

		public void ProgressStep()
		{
			pgProgress.PerformStep();
			Application.DoEvents();
		}
	}
}
