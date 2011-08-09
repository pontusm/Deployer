using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using DeployerPluginInterfaces;

namespace DeployerPlugins
{
	/// <summary>
	/// Handles starting/stopping a service when deploying.
	/// </summary>
	/// <remarks>
	/// 2010-07-01  POMU: Class created
	/// </remarks>
	public class ServiceController : DeployerPlugin, IDeployerHook
	{
		private string _url;

		public string PluginIdentifier
		{
			get { return "ServiceController"; }
		}

		public string Name
		{
			get { return "Service Controller"; }
		}

		public string Description
		{
			get { return "Stops and restarts a remote service so that it you can deploy new versions of it."; }
		}

		public void LoadSettings(string settings)
		{
			XmlDocument xd = new XmlDocument();
			xd.LoadXml(settings);
			_url = xd.DocumentElement.GetAttribute("Url");
		}

		public string SaveSettings()
		{
			XmlDocument xd = new XmlDocument();
			xd.LoadXml("<Settings/>");
			xd.DocumentElement.SetAttribute("Url", _url);
			return xd.OuterXml;
		}

		public DialogResult ShowConfigureSettingsDialog(IWin32Window owner)
		{
			return DialogResult.OK;
		}

		public void BeforeDeploy(IDeployerEventSink host)
		{
			ServiceStatusWindow wnd = null;
			try
			{
				var msg = "Stopping service...";
				wnd = new ServiceStatusWindow {StatusMessage = msg};
				wnd.Show();
				host.OnNotificationMessage(this, msg);

				// Pump events to ensure window is shown
				Application.DoEvents();

				wnd.ProgressStep();

				// Stop service
				var wc = new WebClient();
				var result = wc.DownloadString(_url + "&action=stop");
				if(!result.Contains("Stopped"))
				{
					wnd.Hide();

					var dialogResult = MessageBox.Show("Unable to stop service. Do you want to deploy anyway?", "Service controller",
					                                   MessageBoxButtons.YesNo, MessageBoxIcon.Error);
					if(dialogResult == DialogResult.No)
						throw new DeployCancelException("Unable to stop service.");
				}
				else
				{
					wnd.ProgressStep();

					// AFter stopping the service it takes a few seconds for it to proper shutdown so we wait a little extra
					Thread.Sleep(TimeSpan.FromSeconds(5));

					host.OnNotificationMessage(this, "Service stopped.");
				}

				wnd.Close();
				wnd = null;
			}
			finally
			{
				if(wnd != null)
					wnd.Close();
			}
		}

		public void AfterDeploy(IDeployerEventSink host)
		{
			ServiceStatusWindow wnd = null;
			try
			{
				var msg = "Starting service...";
				wnd = new ServiceStatusWindow {StatusMessage = msg};
				wnd.Show();
				host.OnNotificationMessage(this, msg);

				Application.DoEvents();

				wnd.ProgressStep();

				// Start service
				var wc = new WebClient();
				var result = wc.DownloadString(_url + "&action=start");
				wnd.ProgressStep();
				if (!result.Contains("Running"))
				{
					wnd.Hide();
					MessageBox.Show("Failed to start service. You may need to restart it manually.", "Service controller",
									MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{
					host.OnNotificationMessage(this, "Service started.");
				}

				wnd.Close();
				wnd = null;
			}
			finally
			{
				if(wnd != null)
					wnd.Close();
			}
		}
	}
}
