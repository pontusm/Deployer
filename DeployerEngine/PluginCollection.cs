using System;
using System.Collections;
using System.Collections.Generic;
using DeployerPluginInterfaces;

namespace DeployerEngine
{
	/// <summary>
	/// Contains a collection of plugins.
	/// </summary>
	/// <remarks>
	/// 2005-01-14	POMU: Class Created
	/// </remarks>
	public class PluginCollection : IEnumerable, ICollection
	{
		#region Private members
		//private Hashtable _plugins = new Hashtable();
		private Dictionary<string, IDeployerPlugin> _plugins = new Dictionary<string, IDeployerPlugin>(StringComparer.InvariantCultureIgnoreCase);
		#endregion

		#region Indexers

		/// <summary>
		/// Gets the plugin with the specified identifier
		/// </summary>
		public IDeployerPlugin this[string identifier] {
			get {
				return Get(identifier);
			}
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator() {
			foreach (IDeployerPlugin plugin in _plugins.Values)
				yield return plugin;
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
				return _plugins.Count;
			}
		}

		public void CopyTo(Array array, int index) {
			throw new NotImplementedException();
		}

		public object SyncRoot {
			get {
				return null;
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Clears the plugins (must unload domain also)
		/// </summary>
		internal void Clear() {
			_plugins.Clear();
		}
		
		/// <summary>
		/// Checks if a plugin for the specified destination exists or not.
		/// </summary>
		public bool Contains(string identifier) {
			return _plugins.ContainsKey(identifier);
		}

		/// <summary>
		/// Gets the plugin with the specified destination name
		/// </summary>
		public IDeployerPlugin Get(string identifier) {
			if (!_plugins.ContainsKey(identifier))
				return null;
			return _plugins[identifier];
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Adds a plugin to the collection.
		/// </summary>
		internal void Add(string identifier, IDeployerPlugin plugin) {
			if (_plugins.ContainsKey(identifier))
				throw new ArgumentException(string.Format("Plugin for destination '{0}' already loaded.", identifier));
			_plugins.Add(identifier, plugin);
		}

		/// <summary>
		/// Removes the specified destination.
		/// </summary>
		/// <param name="identifier">The destination identifier.</param>
		/// <returns>True if removed, or false if not found.</returns>
		internal bool Remove(string identifier) {
			return _plugins.Remove(identifier);
		}
		#endregion
	}
}
