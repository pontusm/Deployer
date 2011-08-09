using System;
using System.Collections;

namespace DeployerEngine.Project
{
	/// <summary>
	/// Summary description for PluginSettingsCollection.
	/// </summary>
	/// <remarks>
	/// 2005-01-17	POMU: Class Created
	/// </remarks>
	[Serializable]
	public class PluginSettingsCollection : ICollection
	{
		#region Private members
		private ArrayList _list = new ArrayList();
		#endregion

		#region Constructors
		public PluginSettingsCollection()
		{
		}
		#endregion

		#region Indexer

		public PluginSettings this[int index] {
			get {
				return (PluginSettings)_list[index];
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
		/// Add plugin settings to the collection.
		/// </summary>
		public void Add(PluginSettings settings) {
			if(Contains(settings.Identifier))
				throw new InvalidOperationException(string.Format("Plugin settings for plugin '{0}' already exists.", settings.Identifier));

			_list.Add(settings);
		}

		/// <summary>
		/// Clear all plugin settings.
		/// </summary>
		public void Clear() {
			_list.Clear();
		}

		/// <summary>
		/// Indicates whether settings for a certain plugin exists or not in the list.
		/// </summary>
		/// <param name="pluginIdentifier"></param>
		/// <returns></returns>
		public bool Contains(string pluginIdentifier) {
			return Get(pluginIdentifier) != null;
		}

		/// <summary>
		/// Get the plugin settings for the plugin with the specified identifier.
		/// </summary>
		/// <param name="pluginIdentifier"></param>
		/// <returns></returns>
		public PluginSettings Get(string pluginIdentifier) {
			foreach(PluginSettings settings in _list) {
				if(settings.Identifier == pluginIdentifier)
					return settings;
			}
			return null;
		}

		/// <summary>
		/// Removes the settings for the plugin with the specified identifier.
		/// </summary>
		/// <param name="pluginIdentifier"></param>
		public void Remove(string pluginIdentifier) {
			PluginSettings ps = Get(pluginIdentifier);
			if(ps != null)
				_list.Remove(ps);
		}
		#endregion
	}
}
