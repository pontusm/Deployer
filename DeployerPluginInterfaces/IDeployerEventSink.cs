using System;
using System.Windows.Forms;

namespace DeployerPluginInterfaces
{
	/// <summary>
	/// Plugins can call methods in this interface to talk to the host application.
	/// </summary>
	public interface IDeployerEventSink
	{
		void OnNotificationMessage(object sender, string message);
		void OnTransferProgress(object sender, TransferEventArgs e);
	}
}
