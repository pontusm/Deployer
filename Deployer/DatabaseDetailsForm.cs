using System;
using System.Data;
using System.Windows.Forms;
using DeployerEngine.Database;

namespace Deployer
{
	/// <summary>
	/// Summary description for DatabaseDetailsForm.
	/// </summary>
	public class DatabaseDetailsForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button _close;

		private TableComparison _comparison;
		private System.Windows.Forms.TabControl _tabcontrol;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DatabaseDetailsForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Gets or sets the comparison to view.
		/// </summary>
		public TableComparison Comparison {
			get { return _comparison; }
			set { _comparison = value; }
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DatabaseDetailsForm));
			this._close = new System.Windows.Forms.Button();
			this._tabcontrol = new System.Windows.Forms.TabControl();
			this.SuspendLayout();
			// 
			// _close
			// 
			this._close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._close.Location = new System.Drawing.Point(776, 616);
			this._close.Name = "_close";
			this._close.TabIndex = 1;
			this._close.Text = "Close";
			this._close.Click += new System.EventHandler(this._close_Click);
			// 
			// _tabcontrol
			// 
			this._tabcontrol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._tabcontrol.Location = new System.Drawing.Point(8, 8);
			this._tabcontrol.Name = "_tabcontrol";
			this._tabcontrol.SelectedIndex = 0;
			this._tabcontrol.Size = new System.Drawing.Size(848, 600);
			this._tabcontrol.TabIndex = 2;
			// 
			// DatabaseDetailsForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(864, 646);
			this.Controls.Add(this._tabcontrol);
			this.Controls.Add(this._close);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DatabaseDetailsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Comparison details";
			this.Load += new System.EventHandler(this.DatabaseDetailsForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		#region Event handlers

		private void DatabaseDetailsForm_Load(object sender, System.EventArgs e) {
			CreateTabs();
		}

		private void _close_Click(object sender, EventArgs e) {
			Close();
		}

		private void tab_Resize(object sender, EventArgs e) {
			ResizeTabControls((TabPage)sender);
		}
		#endregion

		#region Private methods

		/// <summary>
		/// Creates the tabs.
		/// </summary>
		private void CreateTabs() {
			// No source table?
			if(_comparison.SourceTable == null)
				return;

			// Go through the definitions
			foreach(DataTable table in _comparison.SourceTable.AllDefinitions) {
				TabPage tab = new TabPage(table.TableName);
				tab.Resize += new EventHandler(tab_Resize);
				CreateGrid(tab, table, string.Format("Source: {0}", _comparison.TableName));
				if(_comparison.DestinationTable != null) {
					// Locate definition in destination descriptor
					DataTable dt = _comparison.DestinationTable.AllDefinitions[table.TableName];
					if(dt != null)
						CreateGrid(tab, dt, string.Format("Destination: {0}", _comparison.TableName));
				}
				_tabcontrol.Controls.Add(tab);
				ResizeTabControls(tab);
			}
		}

		/// <summary>
		/// Creates a grid on the supplied tab page.
		/// </summary>
		private void CreateGrid(TabPage tab, DataTable datasource, string captiontext) {
			DataGrid grid = new DataGrid();
			grid.CaptionText = captiontext;
			grid.DataSource = datasource;
			tab.Controls.Add(grid);
		}

		private void ResizeTabControls(TabPage tab) {
			// Place all controls in the tab horisontally
			int leftpos = 0;
			int controlwidth = tab.Width / tab.Controls.Count;
			foreach(Control control in tab.Controls) {
				control.Top = 0;
				control.Left = leftpos;
				control.Width = controlwidth;
				control.Height = tab.Height;
				leftpos += controlwidth;
			}
		}

		#endregion

	}
}
