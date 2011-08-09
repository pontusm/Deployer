using System;
using DeployerEngine.Database.Objects;

namespace DeployerEngine.Database
{
	/// <summary>
	/// Contains information about a database object transfer.
	/// </summary>
	/// <remarks>
	/// 2005-04-07	POMU: Class Created
	/// </remarks>
	public class DatabaseTransferEventArgs : EventArgs
	{
		#region Private members
		private IDatabaseObject _databaseobject;	
		#endregion

		#region Constructors
		public DatabaseTransferEventArgs(IDatabaseObject databaseobject) {
			_databaseobject = databaseobject;
		}

		#endregion

		#region Properties

		public IDatabaseObject DatabaseObject {
			get { return _databaseobject; }
		}

		#endregion
	}
}
