

using System;
using System.Collections;
using System.Collections.Generic;

namespace DeployerEngine.Project {
	/// <summary>
	/// FilterSettingsCollection
	/// </summary>
	/// <remarks>
	/// 2006-10-12	POMU: Class created 
	/// </remarks>
	[Serializable]
	public class FilterSettingsCollection : ICollection {
		#region Private members
		// TODO: Change this into a dictionary maybe?
		private List<FilterSettings> _list = new List<FilterSettings>();
		#endregion

		#region Indexer

		public FilterSettings this[int index] {
			get {
				return _list[index];
			}
		}

		#endregion

		#region ICollection Members

		public bool IsSynchronized {
			get {
				return false;
			}
		}

		public int Count {
			get {
				return _list.Count;
			}
		}

		public void CopyTo(Array array, int index) {
			for (int i = 0; i < _list.Count; i++) {
				array.SetValue(_list[i], index + i);
			}
		}

		public object SyncRoot {
			get {
				return null;
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
		/// Add a deploy destination.
		/// </summary>
		public void Add(FilterSettings item) {
			if (Contains(item.VirtualPath))
				throw new InvalidOperationException(string.Format("Filter settings for path '{0}' already exists.", item.VirtualPath));

			_list.Add(item);
		}

		/// <summary>
		/// Clear all deploy destinations.
		/// </summary>
		public void Clear() {
			_list.Clear();
		}

		/// <summary>
		/// Indicates whether a deploy destination exists or not in the list.
		/// </summary>
		/// <returns></returns>
		public bool Contains(string virtualPath) {
			return Get(virtualPath) != null;
		}

		/// <summary>
		/// Get the deploy destination with the specified name.
		/// </summary>
		public FilterSettings Get(string virtualPath) {
			foreach (FilterSettings item in _list) {
				if (string.Compare(item.VirtualPath, virtualPath, true) == 0)
					return item;
			}
			return null;
		}

		/// <summary>
		/// Removes the settings for the plugin with the specified identifier.
		/// </summary>
		public void Remove(string virtualPath) {
			FilterSettings item = Get(virtualPath);
			if (item != null)
				_list.Remove(item);
		}
		#endregion
		
	}
}