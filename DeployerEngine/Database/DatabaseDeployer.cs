using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using DeployerEngine.Database.Objects;
using DeployerEngine.DatabaseInfoService;
using DeployerEngine.Project;
using DeployerPluginInterfaces;

namespace DeployerEngine.Database
{
	/// <summary>
	/// Handles database deployment
	/// </summary>
	/// <remarks>
	/// 2005-03-30	POMU: Class Created
	/// </remarks>
	internal class DatabaseDeployer
	{
		#region Private members
		private static Guid _clienttoken = new Guid("{50B41C08-8B7D-4f63-A777-3BCE5305B16F}");

		private DatabaseInfo _sourceDbService;
		private DatabaseInfo _destinationDbService;

		#endregion

		#region Constructors
		internal DatabaseDeployer()
		{
		}
		#endregion

		#region Internal methods

		/// <summary>
		/// Scans for differences in the specified databases.
		/// </summary>
		/// <param name="databases">The source and destination databases to compare.</param>
		internal DatabaseComparison CompareDatabases(DatabasePair databases) {
			// Start web service session
			InitializeServices(databases);

			DatabaseComparison comparison = new DatabaseComparison(databases);

			// Retrieve source tables and notify client
			DataTable sourceTables = _sourceDbService.GetTables().Tables[0];
			foreach(DataRow row in sourceTables.Rows) {
				EventManager.OnSourceTableFound((string)row["TABLE_NAME"]);
			}

			// Retrieve destination tables and perform comparison
			DataTable destinationTables = _destinationDbService.GetTables().Tables[0];
			CompareAllTables(sourceTables, destinationTables, comparison);

			ShutdownServices();

			return comparison;
		}

		/// <summary>
		/// Deploys the source database to the destination database. Only stored procedures, views and functions
		/// are deployed.
		/// </summary>
		internal void DeployDatabase(DatabaseDeploymentStructure structure) {
			EventManager.OnNotificationMessage("Starting deployment...");

			InitializeServices(structure.Databases);

			try {
				// Deploy objects
				foreach(IDatabaseObject obj in structure.DatabaseObjects) {
					if(obj.IncludeInDeployment) {
						bool retry;
						do {
							retry = false;		// Assume we won't retry operation
							try {
								// Signal that deployment started
								DatabaseTransferEventArgs e = new DatabaseTransferEventArgs(obj);
								EventManager.OnDatabaseTransferBegin(e);

								obj.Deploy(_sourceDbService, _destinationDbService);

								// Signal that deployment completed
								EventManager.OnDatabaseTransferComplete(e);
							}
							catch (DeployCancelException) {
								throw;
							}
							catch (Exception ex) {
								DeployErrorForm dlg = new DeployErrorForm();
								dlg.ErrorMessage = ex.Message;
								dlg.SetLocalInfo("Object Name:", obj.Name);
								dlg.SetRemoteInfo("Type:", obj.TypeName);
								DialogResult result = dlg.ShowDialog();
								switch (result) {
									case DialogResult.Retry:
										retry = true;
										break;
									case DialogResult.Ignore:
										break;		// Skip to next file
									case DialogResult.Cancel:
										throw new DeployCancelException("User cancelled.");
								}
							}
						} while (retry);
					}
				}
			}
			catch (DeployCancelException ex) {
				// Deployment has been cancelled
				EventManager.OnNotificationMessage("*** Deployment stopped! " + ex.Message);
			}
			ShutdownServices();

			EventManager.OnNotificationMessage("Deployment completed.");
		}

		/// <summary>
		/// Scans a database for objects to deploy.
		/// </summary>
		internal DatabaseDeploymentStructure ScanDatabase(DeploymentProject project, DatabasePair databases) {
			InitializeServices(databases);

			DatabaseDeploymentStructure structure = new DatabaseDeploymentStructure(databases);

			ScanProcedures(project.ActiveDeployConfig.DatabaseSettings.ExcludeProcedures, structure);
			ScanViews(structure);

			ShutdownServices();

			return structure;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Compares all the supplied table definitions.
		/// </summary>
		/// <param name="sourceTables"></param>
		/// <param name="destinationTables"></param>
		private void CompareAllTables(DataTable sourceTables, DataTable destinationTables, DatabaseComparison comparison) {

			// Verify source tables
			foreach(DataRow row in sourceTables.Rows) {
				// Fetch information about source table
				string tabname = (string)row["TABLE_NAME"];
				Debug.WriteLine("Retrieving table information for " + tabname);
				DataSet ds = _sourceDbService.GetTableInfo(tabname);
				TableDescriptor sourceTable = new TableDescriptor(ds);

				// Lookup destination table
				TableComparison tablecomparison;
				DataRow[] desttabs = destinationTables.Select(string.Format("TABLE_NAME='{0}'", tabname));
				if(desttabs.Length > 0) {
					// Table exists in destination, retrieve more info and compare them
					ds = _destinationDbService.GetTableInfo(tabname);
					TableDescriptor destinationTable = new TableDescriptor(ds);
					FilterMatches(sourceTable, destinationTable);

					// Create comparison result
					tablecomparison = new TableComparison(tabname, sourceTable, destinationTable);
				}
				else {
					// Create comparison result (destination table is missing)
					tablecomparison = new TableComparison(tabname, sourceTable, null);
				}

				// Add comparison result and fire event
				comparison.TableComparisons.Add(tablecomparison);
				EventManager.OnTableComparisonComplete(tablecomparison);
			}
		}

		/// <summary>
		/// Compares the table definitions for two tables.
		/// </summary>
		/// <param name="sourceTable"></param>
		/// <param name="destinationTable"></param>
		private void FilterMatches(TableDescriptor sourceTable, TableDescriptor destinationTable) {
			// Filter definitions so they only contain the differences
			DataTableComparer.FilterMatches(sourceTable.ColumnDefinition, destinationTable.ColumnDefinition, new string[] { "COLUMN_NAME" }, new string[] { "TABLE_CATALOG", "ORDINAL_POSITION" });
			DataTableComparer.FilterMatches(sourceTable.ConstraintDefinition, destinationTable.ConstraintDefinition, new string[] { "CONSTRAINT_NAME" }, new string[] { "CONSTRAINT_CATALOG", "TABLE_CATALOG" });
			DataTableComparer.FilterMatches(sourceTable.ForeignKeyDefinition, destinationTable.ForeignKeyDefinition, new string[] { "FOREIGN_KEY_NAME" }, null);
			DataTableComparer.FilterMatches(sourceTable.IndexDefinition, destinationTable.IndexDefinition, new string[] { "INDEX_NAME", "COLUMN_NAME" }, new string[] { "TABLE_CATALOG", "INDEX_CATALOG", "CARDINALITY", "PAGES" });
			DataTableComparer.FilterMatches(sourceTable.TableInfoDefinition, destinationTable.TableInfoDefinition, new string[] { "TABLE_NAME" }, new string[] { "TABLE_CATALOG", "TABLE_VERSION", "CARDINALITY" });
			DataTableComparer.FilterMatches(sourceTable.TableDefinition, destinationTable.TableDefinition, new string[] { "TABLE_NAME" }, new string[] { "TABLE_CATALOG", "DATE_CREATED", "DATE_MODIFIED" });
		}

		/// <summary>
		/// Initializes the web service and starts a new session.
		/// </summary>
		/// <param name="database"></param>
		private DatabaseInfo InitializeService(DatabaseDescriptor database) {
			try {
				DatabaseInfo service = new DatabaseInfo();
				service.CookieContainer = new CookieContainer();
				service.Url = database.Url;
				service.StartSession(_clienttoken, database.Name);
				return service;
			}
			catch (WebException ex) {
				throw new ApplicationException(string.Format("Unable to connect to database info service (Name={0}, Url={1})", database.Name, database.Url), ex);
			}
		}

		/// <summary>
		/// Initializes source and destination web services and starts the sessions.
		/// </summary>
		private void InitializeServices(DatabasePair databases) {
			// Setup web service connections
			_sourceDbService = InitializeService(databases.Source);
			_destinationDbService = InitializeService(databases.Destination);
		}

		/// <summary>
		/// Scans the procedures in the database.
		/// </summary>
		private void ScanProcedures(FilterCollection excludeProcedures, DatabaseDeploymentStructure structure) {
			// Retrieve procs in dest db and build a hashtable with them
			DataTable destprocs = _destinationDbService.GetProcedures().Tables[0];
			Hashtable destprocstable = new Hashtable();
			foreach(DataRow row in destprocs.Rows) {
				DbStoredProcedure proc = new DbStoredProcedure(row);
				destprocstable.Add(proc.Name, proc);
			}
	
			// Retrieve stored procedures and build deployment structure
			DataTable sourceprocs = _sourceDbService.GetProcedures().Tables[0];
			foreach(DataRow row in sourceprocs.Rows) {
				DbStoredProcedure proc = new DbStoredProcedure(row);
				// Does this proc already exist in destination?
				proc.IsNew = !destprocstable.ContainsKey(proc.Name);

				if(!excludeProcedures.IsMatching(proc.Name))
					structure.Add(proc);
			}
		}

		/// <summary>
		/// Scans the views in the database.
		/// </summary>
		private void ScanViews(DatabaseDeploymentStructure structure) {
			// Retrieve views in dest db and build hashtable with them
			DataTable destviews = _destinationDbService.GetViews().Tables[0];
			Hashtable destviewstable = new Hashtable();
			foreach(DataRow row in destviews.Rows) {
				DbView view = new DbView(row);
				destviewstable.Add(view.Name, view);
			}

			// Retrieve views from source and build structure
			DataTable sourceviews = _sourceDbService.GetViews().Tables[0];
			foreach(DataRow row in sourceviews.Rows) {
				DbView view = new DbView(row);
				// Does this view already exist in destination?
				view.IsNew = !destviewstable.ContainsKey(view.Name);

				structure.Add(view);
			}
		}

		/// <summary>
		/// Shuts down the web service sessions.
		/// </summary>
		private void ShutdownServices() {
			// Close web service sessions
			_destinationDbService.ShutdownSession();
			_sourceDbService.ShutdownSession();
		}
		#endregion
	}
}
