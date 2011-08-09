using System;
using System.Data;

namespace DeployerEngine.Database
{
	/// <summary>
	/// Contains information about a database table.
	/// </summary>
	/// <remarks>
	/// 2005-03-30	POMU: Class Created
	/// </remarks>
	public class TableDescriptor
	{
		#region Private members
		private DataSet _dataset;
		#endregion

		#region Constructors
		public TableDescriptor(DataSet ds)
		{
			if(ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
				throw new ArgumentException("No table information supplied.");

			_dataset = ds;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Indicates whether any of the definition tables has any rows or not.
		/// </summary>
		public bool IsEmpty {
			get {
				foreach(DataTable table in _dataset.Tables) {
					if(table.Rows.Count > 0)
						return false;
				}
				return true;
			}
		}

		/// <summary>
		/// Gets a collection with all the definition tables.
		/// </summary>
		public DataTableCollection AllDefinitions {
			get {
				return _dataset.Tables;
			}
		}

		public DataTable ColumnDefinition {
			get { return _dataset.Tables["TableColumn"]; }
		}

		public DataTable ConstraintDefinition {
			get { return _dataset.Tables["TableConstraint"]; }
		}

		public DataTable ForeignKeyDefinition {
			get { return _dataset.Tables["ForeignKey"]; }
		}

		public DataTable IndexDefinition {
			get { return _dataset.Tables["Index"]; }
		}

		public DataTable TableInfoDefinition {
			get { return _dataset.Tables["TableInfo"]; }
		}

		public DataTable TableDefinition {
			get { return _dataset.Tables["Table"]; }
		}

		#endregion

	}
}
