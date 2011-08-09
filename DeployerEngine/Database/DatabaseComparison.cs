using System;
using System.Collections;
using DeployerEngine.Project;

namespace DeployerEngine.Database
{
	/// <summary>
	/// Contains the result of a database comparison.
	/// </summary>
	/// <remarks>
	/// 2005-03-30	POMU: Class Created
	/// </remarks>
	public class DatabaseComparison
	{
		#region Private members
		private TableComparisonCollection _tablecomparisons = new TableComparisonCollection();
		private DatabasePair _databases;
		#endregion

		#region Constructors
		internal DatabaseComparison(DatabasePair databases) {
			_databases = databases;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Contains the databases involved in the comparison.
		/// </summary>
		public DatabasePair Databases {
			get { return _databases; }
		}

		/// <summary>
		/// If true, all the compared databases are equal.
		/// </summary>
		public bool IsEqual {
			get {
				foreach(TableComparison comparison in _tablecomparisons) {
					if(!comparison.IsEqual)
						return false;
				}
				return true;
			}
		}

		/// <summary>
		/// Gets the list of TableComparison objects containing information about compared table definitions.
		/// </summary>
		public TableComparisonCollection TableComparisons {
			get { return _tablecomparisons; }
		}

		#endregion
	}

	#region TableComparison class

	/// <summary>
	/// Contains information about the comparison of two tables.
	/// </summary>
	public class TableComparison {
		#region Private members

		private string _tablename;
		private TableDescriptor _sourceTable;
		private TableDescriptor _destinationTable;

		#endregion

		#region Constructor

		internal TableComparison(string tablename, TableDescriptor sourceTable, TableDescriptor destinationTable) {
			_tablename = tablename;
			_sourceTable = sourceTable;
			_destinationTable = destinationTable;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Contains the definition for the source table, or null if the table does not exist in the source database.
		/// </summary>
		public TableDescriptor SourceTable {
			get { return _sourceTable; }
		}

		/// <summary>
		/// Contains the definition for the destination table, or null if the table does not exist in the destination database.
		/// </summary>
		public TableDescriptor DestinationTable {
			get { return _destinationTable; }
		}

		/// <summary>
		/// If true, the two compared tables are equal.
		/// </summary>
		public bool IsEqual {
			get {
				if(_sourceTable == null || _destinationTable == null)
					return false;

				return _sourceTable.IsEmpty && _destinationTable.IsEmpty;
			}
		}

		/// <summary>
		/// Gets a summary of the comparison result.
		/// </summary>
		public string Summary {
			get {
				return GetSummary();
			}
		}

		/// <summary>
		/// Contains the name of the table that is being compared.
		/// </summary>
		public string TableName {
			get { return _tablename; }
		}

		#endregion

		/// <summary>
		/// Construct a short summary of the comparison.
		/// </summary>
		private string GetSummary() {
			string summary = string.Empty;

			if(_sourceTable == null)
				summary = "Table missing in source database.";
			else if(_destinationTable == null)
				summary = "Table missing in destination database.";
			else if(IsEqual)
				summary = "Table is OK.";
			else {
				summary = "Mismatch in ";
				if(_sourceTable.ColumnDefinition.Rows.Count > 0 || _destinationTable.ColumnDefinition.Rows.Count > 0)
					summary += "columns, ";
				if(_sourceTable.ConstraintDefinition.Rows.Count > 0 || _destinationTable.ConstraintDefinition.Rows.Count > 0)
					summary += "constraints, ";
				if(_sourceTable.ForeignKeyDefinition.Rows.Count > 0 || _destinationTable.ForeignKeyDefinition.Rows.Count > 0)
					summary += "foreign keys, ";
				if(_sourceTable.IndexDefinition.Rows.Count > 0 || _destinationTable.IndexDefinition.Rows.Count > 0)
					summary += "index, ";
				if(_sourceTable.TableInfoDefinition.Rows.Count > 0 || _destinationTable.TableInfoDefinition.Rows.Count > 0)
					summary += "table info, ";
				if(_sourceTable.TableDefinition.Rows.Count > 0 || _destinationTable.TableDefinition.Rows.Count > 0)
					summary += "table definition, ";

				summary = summary.Substring(0, summary.Length - 2) + ".";
			}

			return summary;
		}
	}

	#endregion

	#region TableComparisonCollection class

	public class TableComparisonCollection : IEnumerable, ICollection {
		#region Private members

		private ArrayList _list = new ArrayList();

		#endregion

		#region Constructor

		public TableComparisonCollection() {}

		#endregion

		#region ICollection Members

		public bool IsSynchronized {
			get {
				return _list.IsSynchronized;
			}
		}

		public int Count {
			get {
				return _list.Count;
			}
		}

		public void CopyTo(Array array, int index) {
			_list.CopyTo(array, index);
		}

		public object SyncRoot {
			get {
				return _list.SyncRoot;
			}
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator() {
			return _list.GetEnumerator();
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Adds a table comparison object to the collection.
		/// </summary>
		/// <param name="tablecomparison"></param>
		internal void Add(TableComparison tablecomparison) {
			_list.Add(tablecomparison);
		}

		#endregion
	}

	#endregion
}
