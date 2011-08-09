using System;
using System.Runtime.Serialization;

namespace DeployerPluginInterfaces
{
	/// <summary>
	/// This exception is thrown when a plugin wants to cancel the deployment.
	/// </summary>
	/// <remarks>
	/// 2005-01-31	POMU: Class Created
	/// </remarks>
	[Serializable]
	public class DeployCancelException : ApplicationException
	{
		public DeployCancelException(string message) : base(message)
		{
		}

		protected DeployCancelException(SerializationInfo info, StreamingContext context) : base(info, context) {}
	}
}
