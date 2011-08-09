using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace DeployerEngine.Database
{
	/// <summary>
	/// Handles comparison of two data tables.
	/// </summary>
	/// <remarks>
	/// 2005-03-30	POMU: Class Created
	/// </remarks>
	public class DataTableComparer
	{
		#region Constructors
		private DataTableComparer()
		{
		}
		#endregion

		/// <summary>
		/// Compares two datatables and filters out all matching rows. The columns in the two tables must be the same.
		/// </summary>
		/// <param name="source">The source table to compare.</param>
		/// <param name="destination">The destination table to compare.</param>
		/// <param name="keycolumns">The column that identifies the row.</param>
		/// <param name="ignorecolumns">A collection of columns to ignore when comparing.</param>
		public static void FilterMatches(DataTable source, DataTable destination, string[] keycolumns, string[] ignorecolumns) {
			StringCollection ignorecols = new StringCollection();
			if(ignorecolumns != null)
				ignorecols.AddRange(ignorecolumns);

			for(int i = source.Rows.Count - 1; i >= 0; i--) {
				DataRow sourcerow = source.Rows[i];

				// Locate the row in the destination table
				string select = string.Empty;
				foreach(string keycolumn in keycolumns) {
					select += string.Format("{0}='{1}' AND ", keycolumn, sourcerow[keycolumn]);
				}
				select = select.Substring(0, select.Length - 5);
				DataRow[] rows = destination.Select(select);		// Select matching rows
				if(rows.Length > 0) {
					DataRow destrow = rows[0];
					if(CompareColumns(source, sourcerow, destrow, ignorecols)) {
						// Rows are matching, remove them
						source.Rows.Remove(sourcerow);
						destination.Rows.Remove(destrow);
					}
				}
			}
		}

		private static bool CompareColumns(DataTable source, DataRow sourcerow, DataRow destrow, StringCollection ignorecolumns) {
			foreach(DataColumn column in source.Columns) {
				// Not in ignored columns?
				if(ignorecolumns == null || !ignorecolumns.Contains(column.ColumnName)) {
					if(sourcerow[column.ColumnName].ToString() != destrow[column.ColumnName].ToString())
						return false;
				}
			}
			return true;		// All columns matching
		}
	}
}
