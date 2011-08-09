using System;
using DeployerEngine.DatabaseInfoService;

namespace DeployerEngine.Database.Objects
{
	/// <summary>
	/// Interface representing a database object.
	/// </summary>
	/// <remarks>
	/// 2005-04-06	POMU: Class Created
	/// </remarks>
	public interface IDatabaseObject
	{
		bool IncludeInDeployment { get; set; }

		/// <summary>
		/// Indicates whether the database object is new or not.
		/// </summary>
		bool IsNew { get; set; }

		/// <summary>
		/// Gets the name of the object.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the type of the object.
		/// </summary>
		string TypeName { get; }

		/// <summary>
		/// Deploys the database object.
		/// </summary>
		void Deploy(DatabaseInfo sourceDbService, DatabaseInfo destinationDbService);
	}
}
