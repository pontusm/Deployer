using System;

namespace DeployerEngine.Project
{
	/// <summary>
	/// Contains a pair of databases that should be compared when deploying.
	/// </summary>
	/// <remarks>
	/// 2005-03-30	POMU: Class Created
	/// </remarks>
	[Serializable]
	public class DatabasePair
	{
		#region Private members
		private DatabaseDescriptor _source;
		private DatabaseDescriptor _destination;
		#endregion

		#region Constructors
		public DatabasePair()
		{
		}

		public DatabasePair(DatabaseDescriptor source, DatabaseDescriptor destination) {
			_source = source;
			_destination = destination;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the source database.
		/// </summary>
		public DatabaseDescriptor Source {
			get { return _source; }
			set {
				if(_source != value) {
					_source = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the destination database.
		/// </summary>
		public DatabaseDescriptor Destination {
			get { return _destination; }
			set {
				if(_destination != value) {
					_destination = value;
				}
			}
		}

		#endregion

		#region Public methods

		public override bool Equals(object obj) {
			DatabasePair pair = obj as DatabasePair;
			if(pair == null)
				return false;

			return this.Source.Equals(pair.Source) && this.Destination.Equals(pair.Destination);
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		#endregion
	}
}
