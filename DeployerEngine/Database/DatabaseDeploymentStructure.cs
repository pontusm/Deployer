using System;
using System.Collections;
using DeployerEngine.Database.Objects;
using DeployerEngine.Project;

namespace DeployerEngine.Database
{
	/// <summary>
	/// Contains the objects to deploy for a database.
	/// </summary>
	/// <remarks>
	/// 2005-04-06	POMU: Class Created
	/// </remarks>
	public class DatabaseDeploymentStructure
	{
		#region Private members
		private DatabasePair _databases;
		private SortedList _objects = new SortedList();
		#endregion

		#region Constructors

		public DatabaseDeploymentStructure(DatabasePair databases) {
			_databases = databases;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the number of objects in the collection.
		/// </summary>
		public int Count {
			get { return _objects.Count; }
		}

		/// <summary>
		/// Gets the databases involved in the deployment.
		/// </summary>
		public DatabasePair Databases {
			get { return _databases; }
		}

		/// <summary>
		/// Gets a collection of database objects in the structure.
		/// </summary>
		public ICollection DatabaseObjects {
			get { return _objects.Values; }
		}

		#endregion

		#region Public methods

		public void Add(IDatabaseObject obj) {
			_objects.Add(obj.Name, obj);
		}

		/// <summary>
		/// Returns the total amount of objects that should be deployed.
		/// </summary>
		/// <returns></returns>
		public int GetTotalObjectsToDeploy() {
			int count = 0;
			foreach(IDatabaseObject obj in _objects.Values) {
				if(obj.IncludeInDeployment)
					count++;
			}
			return count;
		}

		#endregion
	}
}
