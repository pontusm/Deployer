using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;

namespace DeployerPluginInterfaces {
	/// <summary>
	/// Abstract base class for Deployer plugins.
	/// </summary>
	public abstract class DeployerPlugin : MarshalByRefObject {

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override object InitializeLifetimeService() {
			// Default lifetime is only 5 minutes. Our plugins must live longer.
			ILease lease = (ILease) base.InitializeLifetimeService();
			if(lease.CurrentState == LeaseState.Initial) {
				lease.InitialLeaseTime = TimeSpan.Zero;		// Infinite
			}
			return lease;
		}

		public Version Version {
			get {
				return Assembly.GetExecutingAssembly().GetName().Version;
			}
		}
	}
}
