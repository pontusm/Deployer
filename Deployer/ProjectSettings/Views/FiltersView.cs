using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DeployerEngine.Project;

namespace Deployer.ProjectSettings.Views {
	public partial class FiltersView : UserControl {
		#region Private members

		private DeploymentProject _project;
		private int _currentFilterSettingsIndex = 0;

		#endregion
		
		public FiltersView() {
			InitializeComponent();
		}

		/// <summary>
		/// Gets or sets the project to edit settings for.
		/// </summary>
		public DeploymentProject Project {
			get { return _project; }
			set { _project = value; }
		}

		#region Event handlers

		#region Startup/load

		private void FiltersView_Load(object sender, EventArgs e) {
            RefreshUI();
		}

		#endregion

        public void RefreshUI() {
            if (_project == null)
                return;

            // Setup datagridviews
            SetupIncludeGridView();
            SetupExcludeGridViews();

            // Populate filters list
            _filters.DataSource = new ArrayList(_project.ActiveDeployConfig.FilterSettings);

            UpdateUI();
        }

		private void _filters_SelectedIndexChanged(object sender, EventArgs e) {
			_currentFilterSettingsIndex = _filters.SelectedIndex;
			PopulateGridViews();
		}

		/// <summary>
		/// Handles the AddingNew event of the _includeFilesSource control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.AddingNewEventArgs"/> instance containing the event data.</param>
		private void _includeFilesSource_AddingNew(object sender, AddingNewEventArgs e) {
			DeployDestinationCollection destinations = _project.ActiveDeployConfig.Destinations;
			if (destinations.Count > 0)
				e.NewObject = new Filter(FilterExpressionType.Wildcard, string.Empty, destinations[0].Identifier);
			else
				e.NewObject = new Filter(FilterExpressionType.Wildcard, string.Empty);
		}

		/// <summary>
		/// Handles the CurrentItemChanged event of the _includeFilesSource control.
		/// This is fired when a property of the current item changes.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void _includeFilesSource_CurrentItemChanged(object sender, EventArgs e) {
			Filter filter = _includeFilesSource.Current as Filter;
			if (filter == null)
				return;

			if(!string.IsNullOrEmpty(filter.RemoteFileName) && filter.ExpressionType != FilterExpressionType.ExactFileName) {
				DialogResult result = MessageBox.Show(this,
				                                      "Remote file names are only supported for 'ExactFileName' filter matches. Unless you change the expression type the remote file name will be ignored.\n\nWould you like to change that expression type to 'ExactFileName'?",
				                                      "Wrong expression type", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (result == DialogResult.Yes)
					filter.ExpressionType = FilterExpressionType.ExactFileName;
			}
		}

		/// <summary>
		/// Handles the DataError event of the _includeFiles control.
		/// This is fired when invalid data has been entered in the grid.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
		private void _includeFiles_DataError(object sender, DataGridViewDataErrorEventArgs e) {
			// Get information about where the error occured
			DataGridViewColumn column = _includeFiles.Columns[e.ColumnIndex];
			FilterSettings fs = _project.ActiveDeployConfig.FilterSettings[_currentFilterSettingsIndex];
			Filter filter = fs.IncludeFiles[e.RowIndex];
			if (column.Name == includeFileDestination.Name) {
				DeployDestinationCollection destinations = _project.ActiveDeployConfig.Destinations;
				if (destinations.Count > 0) {
					MessageBox.Show(this,
					                string.Format(
					                	"Expression '{0}' contains an invalid destination identifier '{1}'. It will be set to the first destination.", filter.Expression, filter.DeployDestinationIdentifier),
					                "Data error");
					filter.DeployDestinationIdentifier = destinations[0].Identifier;
				}
			}
			e.ThrowException = false;
		}

		private void _includeFiles_SelectionChanged(object sender, EventArgs e) {
			UpdateUI();
		}

		private void _remove_Click(object sender, EventArgs e) {
			// Remove selected entries
			for(int i = _includeFiles.SelectedRows.Count - 1; i >= 0; i--) {
				_includeFilesSource.RemoveAt(_includeFiles.SelectedRows[i].Index);
			}
		}

		#region Move rows

		private void _moveUp_Click(object sender, EventArgs e) {
			MoveSelectedRows(-1);
		}

		private void _moveDown_Click(object sender, EventArgs e) {
			MoveSelectedRows(1);
		}

		private void MoveSelectedRows(int offset) {
			List<int> newindexlist = new List<int>();
			foreach (DataGridViewRow row in _includeFiles.SelectedRows) {
				int oldindex = row.Index;
				int newindex = row.Index + offset;
				if (newindex >= 0 && newindex < _includeFilesSource.Count) {
					newindexlist.Add(newindex);
					Filter filter = (Filter)_includeFilesSource[oldindex];
					_includeFilesSource.RemoveAt(oldindex);
					_includeFilesSource.Insert(newindex, filter);
					_includeFilesSource.Position = newindex;
				}
			}

			UpdateRowSelection(newindexlist);
		}

		private void UpdateRowSelection(List<int> selectedIndices) {
			// Clear all then select new indices
			foreach (DataGridViewRow row in _includeFiles.Rows) {
				row.Selected = false;
			}
			foreach (int i in selectedIndices)
				_includeFiles.Rows[i].Selected = true;
		}

		#endregion

		#endregion

		#region Setup gridview columns

		private void SetupIncludeGridView() {
			// Set the available expression types
			includeFileExpressionType.DataSource = Enum.GetValues(typeof(FilterExpressionType));

			includeFileDestination.DisplayMember = "Name";
			includeFileDestination.ValueMember = "Identifier";
			includeFileDestination.DataSource = _project.ActiveDeployConfig.Destinations;
		}

		private void SetupExcludeGridViews() {
			excludeDirectoryExpressionType.DataSource = Enum.GetValues(typeof (FilterExpressionType));
			excludeFileExpressionType.DataSource = Enum.GetValues(typeof(FilterExpressionType));
		}
		
		#endregion
		
		#region Populate gridviews

		private void PopulateGridViews() {
			FilterSettings fs = _project.ActiveDeployConfig.FilterSettings[_currentFilterSettingsIndex];
			_includeFilesSource.DataSource = fs.IncludeFiles;
			_excludeDirectoriesSource.DataSource = fs.ExcludeDirectories;
			_excludeFilesSource.DataSource = fs.ExcludeFiles;
		}

		#endregion

		/// <summary>
		/// Updates the UI.
		/// </summary>
		private void UpdateUI() {
			_remove.Enabled = _includeFiles.SelectedRows.Count > 0;
			_moveUp.Enabled = _includeFiles.SelectedRows.Count > 0 && _includeFiles.SelectedRows[0].Index > 0;
			_moveDown.Enabled = _includeFiles.SelectedRows.Count > 0 && (_includeFiles.SelectedRows[_includeFiles.SelectedRows.Count - 1].Index < _includeFilesSource.Count - 1);
		}

	}
}
