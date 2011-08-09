using System;
using System.Data;
using DeployerEngine.DatabaseInfoService;

namespace DeployerEngine.Database.Objects
{
	/// <summary>
	/// Represents a view in the database.
	/// </summary>
	/// <remarks>
	/// 2005-04-07	POMU: Class Created
	/// </remarks>
	public class DbView : IDatabaseObject
	{
		#region Private members
		private bool		_isnew;
		private string		_viewname;
		private bool		_includeInDeployment;

		#endregion

		#region Constructors
		public DbView(DataRow row)
		{
			_viewname = (string)row["TABLE_NAME"];
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
		/// Indicates whether the view is new or not.
		/// </summary>
		public bool IsNew {
			get { return _isnew; }
			set { _isnew = value; }
		}

		/// <summary>
		/// Gets the name of the view.
		/// </summary>
		public string Name {
			get { return _viewname; }
		}

		/// <summary>
		/// Gets the type of the object.
		/// </summary>
		public string TypeName {
			get { return "View"; }
		}
		
		#endregion

		#region Public methods

		/// <summary>
		/// Deploys the database object.
		/// </summary>
		public void Deploy(DatabaseInfo sourceDbService, DatabaseInfo destinationDbService) {
			// Retrieve source code
			string src = sourceDbService.GetViewSourceCode(_viewname);

			if(!_isnew)
				src = src.Replace("CREATE VIEW", "ALTER VIEW");

			// Deploy it
			destinationDbService.ExecuteNonQuery(src);
		}

		#endregion
	}
}
