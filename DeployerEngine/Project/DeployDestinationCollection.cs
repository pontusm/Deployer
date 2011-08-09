using System;
using System.Collections;
using System.Collections.Generic;

namespace DeployerEngine.Project {
	/// <summary>
	/// A collection of deploy destinations.
	/// </summary>
	/// <remarks>
	/// 2006-10-12	POMU: Class created 
	/// </remarks>
	[Serializable]
	public class DeployDestinationCollection : List<DeployDestination> {
		#region Private members
		//private List<DeployDestination> _list = new List<DeployDestination>();
		#endregion

		#region Indexer

		//public DeployDestination this[int index] {
		//    get {
		//        return _list[index];
		//    }
		//}

		#endregion

		#region ICollection Members

		//public bool IsSynchronized {
		//    get {
		//        return false;
		//    }
		//}

		//public int Count {
		//    get {
		//        return _list.Count;
		//    }
		//}

		//public void CopyTo(Array array, int index) {
		//    for (int i = 0; i < _list.Count; i++ ) {
		//        array.SetValue(_list[i], index + i);
		//    }
		//}

		//public object SyncRoot {
		//    get {
		//        return null;
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
		/// Add a deploy destination.
		/// </summary>
		public new void Add(DeployDestination item) {
			if (Contains(item.Identifier))
				throw new InvalidOperationException(string.Format("Deploy destination with identifier '{0}' already exists.", item.Identifier));

			base.Add(item);
		}

		/// <summary>
		/// Clear all deploy destinations.
		/// </summary>
		public new void Clear() {
			base.Clear();
		}

		/// <summary>
		/// Indicates whether a deploy destination exists or not in the list.
		/// </summary>
		/// <returns></returns>
		public bool Contains(string deployDestinationIdentifier) {
			return Get(deployDestinationIdentifier) != null;
		}

		/// <summary>
		/// Get the deploy destination with the specified identifier.
		/// </summary>
		public DeployDestination Get(string deployDestinationIdentifier) {
			foreach (DeployDestination item in this) {
				if (string.Compare(item.Identifier, deployDestinationIdentifier, true) == 0)
					return item;
			}
			return null;
		}

		/// <summary>
		/// Returns the next available unused destination identifier.
		/// </summary>
		public string GetNextIdentifier() {
			int i = 1;
			string id;
			do {
				id = "D" + i;
			} while (Contains(id));
			return id;
		}

		/// <summary>
		/// Removes the settings for the plugin with the specified identifier.
		/// </summary>
		public void Remove(string deployDestinationIdentifier) {
			DeployDestination item = Get(deployDestinationIdentifier);
			if (item != null)
				base.Remove(item);
		}
		#endregion
		
	}
}