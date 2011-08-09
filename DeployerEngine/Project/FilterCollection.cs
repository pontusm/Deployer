using System;
using System.Collections;
using System.Collections.Generic;

namespace DeployerEngine.Project
{
	/// <summary>
	/// A collection of filters.
	/// </summary>
	[Serializable]
	public class FilterCollection : List<Filter> {
		#region Private members

		//private ArrayList _list = new ArrayList();

		#endregion

		#region Constructor

		//public FilterCollection() {}

		#endregion

		#region Indexer

		//public Filter this[int index] {
		//    get {
		//        return (Filter)_list[index];
		//    }
		//}

		#endregion

		#region ICollection Members

		//public bool IsSynchronized {
		//    get {
		//        return _list.IsSynchronized;
		//    }
		//}

		//public int Count {
		//    get {
		//        return _list.Count;
		//    }
		//}

		//public void CopyTo(Array array, int index) {
		//    _list.CopyTo(array, index);
		//}

		//public object SyncRoot {
		//    get {
		//        return _list.SyncRoot;
		//    }
		//}

		#endregion

		#region IEnumerable Members

		//public IEnumerator GetEnumerator() {
		//    return _list.GetEnumerator();
		//}

		#endregion

		#region Public methods

		/// <summary>
		/// Add a filter to the collection.
		/// </summary>
		/// <param name="filter"></param>
		public new void Add(Filter filter) {
			base.Add(filter);
		}

		/// <summary>
		/// Clear all filters.
		/// </summary>
		public new void Clear() {
			base.Clear();
		}

		/// <summary>
		/// Tries to find a filter in the collection that matches the supplied text.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		public Filter GetMatchingFilter(string text) {
			foreach(Filter filter in this) {
				if(filter.IsMatch(text))
					return filter;
			}
			return null;		// No match found
		}

		/// <summary>
		/// Checks if the supplied text matches any of the filters.
		/// </summary>
		/// <param name="text">The text to test.</param>
		public bool IsMatching(string text) {
			foreach(Filter filter in this) {
				if(filter.IsMatch(text))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Removes a filter from the collection.
		/// </summary>
		/// <param name="filter"></param>
		public new void Remove(Filter filter) {
			base.Remove(filter);
		}
		#endregion
	}
}
