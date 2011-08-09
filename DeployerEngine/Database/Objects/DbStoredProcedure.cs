using System;
using System.Data;
using System.Text.RegularExpressions;
using DeployerEngine.DatabaseInfoService;

namespace DeployerEngine.Database.Objects
{
	/// <summary>
	/// Represents a stored procedure in the database.
	/// </summary>
	/// <remarks>
	/// 2005-04-06	POMU: Class Created
	/// </remarks>
	public class DbStoredProcedure : IDatabaseObject
	{
		#region Private members
		private bool		_includeInDeployment;
		private bool		_isnew;
		private string		_procname;
		#endregion

		#region Constructors
		public DbStoredProcedure(DataRow row)
		{
			_procname = (string)row["PROCEDURE_NAME"];
		}
		#endregion

		#region Properties

		/// <summary>
		/// Indicates whether procedure should be included in deployment or not.
		/// </summary>
		public bool IncludeInDeployment {
			get { return _includeInDeployment; }
			set { _includeInDeployment = value; }
		}

		/// <summary>
		/// Indicates whether the procedure is new or not.
		/// </summary>
		public bool IsNew {
			get { return _isnew; }
			set { _isnew = value; }
		}

		/// <summary>
		/// Gets the name of the procedure.
		/// </summary>
		public string Name {
			get { return _procname; }
		}

		/// <summary>
		/// Gets the type of the object.
		/// </summary>
		public string TypeName {
			get { return "Stored procedure"; }
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Deploys the database object.
		/// </summary>
		public void Deploy(DatabaseInfo sourceDbService, DatabaseInfo destinationDbService) {
			// Retrieve source code
			string src = sourceDbService.GetProcedureSourceCode(_procname);

			if(!_isnew)
				src = Regex.Replace(src, @"CREATE\s+PROCEDURE", "ALTER PROCEDURE", RegexOptions.IgnoreCase);

			// Deploy it
			destinationDbService.ExecuteNonQuery(src);
		}

		#endregion
	}
}
