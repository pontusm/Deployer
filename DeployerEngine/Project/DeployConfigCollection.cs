

using System;
using System.Collections;
using System.Collections.Generic;

namespace DeployerEngine.Project {
	/// <summary>
	/// Contains a collection of deploy configs.
	/// </summary>
	/// <remarks>
	/// 2006-10-12	POMU: Class created 
	/// </remarks>
	[Serializable]
	public class DeployConfigCollection : ICollection {
		#region Private members
		private List<DeployConfig> _list = new List<DeployConfig>();
		#endregion

		#region Indexer

		public DeployConfig this[int index] {
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
		/// Add a deploy config.
		/// </summary>
		public void Add(DeployConfig item) {
			if(Contains(item.Name))
				throw new InvalidOperationException(string.Format("Deploy config '{0}' already exists.", item.Name));

			_list.Add(item);
		}

		/// <summary>
		/// Clear all deploy configs.
		/// </summary>
		public void Clear() {
			_list.Clear();
		}

		/// <summary>
		/// Indicates whether a deploy config exists or not in the list.
		/// </summary>
		/// <returns></returns>
		public bool Contains(string deployConfigName) {
			return Get(deployConfigName) != null;
		}

		/// <summary>
		/// Get the deploy config with the specified name.
		/// </summary>
		public DeployConfig Get(string deployConfigName) {
			foreach (DeployConfig item in _list) {
				if (string.Compare(item.Name, deployConfigName, true) == 0)
					return item;
			}
			return null;
		}

		/// <summary>
		/// Removes the settings for the plugin with the specified identifier.
		/// </summary>
		public void Remove(string deployConfigName) {
			DeployConfig item = Get(deployConfigName);
			if (item != null)
				_list.Remove(item);
		}
		#endregion
	}
}