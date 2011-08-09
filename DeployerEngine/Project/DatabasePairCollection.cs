using System;
using System.Collections;

namespace DeployerEngine.Project
{
	/// <summary>
	/// Handles a collection of database descriptors.
	/// </summary>
	/// <remarks>
	/// 2005-03-30	POMU: Class Created
	/// </remarks>
	[Serializable]
	public class DatabasePairCollection : ICollection
	{
		#region Private members
		private ArrayList _list = new ArrayList();
		#endregion

		#region Constructors
		public DatabasePairCollection()
		{
		}
		#endregion

		#region Indexer

		public DatabasePair this[int index] {
			get {
				return (DatabasePair)_list[index];
			}
		}

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

		#region Public methods

		/// <summary>
		/// Add a pair to the collection.
		/// </summary>
		public void Add(DatabasePair pair) {
			_list.Add(pair);
		}

		/// <summary>
		/// Clear the collection.
		/// </summary>
		public void Clear() {
			_list.Clear();
		}

		/// <summary>
		/// Removes a pair from the collection.
		/// </summary>
		public void Remove(DatabasePair pair) {
			_list.Remove(pair);
		}
		#endregion
	}
}
