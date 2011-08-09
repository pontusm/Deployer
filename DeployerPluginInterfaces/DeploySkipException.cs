using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DeployerPluginInterfaces {
	/// <summary>
	/// This exception is thrown when a plugin wants to skip a file.
	/// </summary>
	/// <remarks>
	/// 2006-01-16	POMU: Class Created
	/// </remarks>
	[Serializable]
	public class DeploySkipException : ApplicationException {
		public DeploySkipException() : base()
		{
		}

		private DeploySkipException(SerializationInfo info, StreamingContext context) : base(info, context) {}
	}
}
