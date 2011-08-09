using System;

namespace DeployerEngine.Project
{
	/// <summary>
	/// Contains settings for the databases to deploy.
	/// </summary>
	/// <remarks>
	/// 2005-03-30	POMU: Class Created
	/// </remarks>
	[Serializable]
	public class DatabaseSettings
	{
		#region Private members
		private DatabasePairCollection _databases = new DatabasePairCollection();
		private FilterCollection _excludeProcedures = new FilterCollection();
		#endregion

		#region Constructors
		public DatabaseSettings()
		{
		}
		#endregion

		#region Properties

		/// <summary>
		/// Gets a collection of database pairs to deploy.
		/// </summary>
		public DatabasePairCollection Databases {
			get { return _databases; }
		}

		/// <summary>
		/// Gets a collection of filters for the stored procedures to exclude.
		/// </summary>
		public FilterCollection ExcludeProcedures {
			get { return _excludeProcedures; }
			set { _excludeProcedures = value; }
		}
		
		#endregion
	}
}
