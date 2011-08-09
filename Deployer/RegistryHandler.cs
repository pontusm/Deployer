using System;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Deployer
{
	/// <summary>
	/// Handles the program settings.
	/// </summary>
	/// <remarks>
	/// 2005-04-08	POMU: Class Created
	/// </remarks>
	public class RegistryHandler {
		private RegistryHandler() {
		}

		/// <summary>
		/// Contains the path in HKEY_CURRENT_USER used to store user settings.
		/// </summary>
		public static string RegistryPath {
			get {
				return string.Format("Software\\{0}\\{1}", Application.CompanyName, Application.ProductName);
			}
		}
		private static RegistryKey RegistryRootKey {
			get {
				//return Registry.CurrentUser.CreateSubKey("Software").CreateSubKey(Application.CompanyName).CreateSubKey(Application.ProductName);
				return Registry.CurrentUser.CreateSubKey(RegistryPath);
			}
		}

		/// <summary>
		/// Deletes a value from the application registry key.
		/// </summary>
		/// <param name="name"></param>
		public static void DeleteValue(string name) {
			RegistryRootKey.DeleteValue(name, false);
		}

		/// <summary>
		/// Retrieves a value from the application registry key.
		/// </summary>
		/// <returns></returns>
		public static string GetValue(string name, string defaultValue) {
			return (string)RegistryRootKey.GetValue(name, defaultValue);
		}

		public static int GetValue(string name, int defaultValue) {
			return (int)RegistryRootKey.GetValue(name, defaultValue);
		}

		public static bool GetValue(string name, bool defaultValue) {
			object val = RegistryRootKey.GetValue(name);
			if(val == null)
				return defaultValue;

			return Convert.ToBoolean(val);
		}

		/// <summary>
		/// Sets a value in the application registry key.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public static void SetValue(string name, string value) {
			RegistryRootKey.SetValue(name, value);
		}

		public static void SetValue(string name, int value) {
			RegistryRootKey.SetValue(name, value);
		}

		public static void SetValue(string name, bool value) {
			if(value == true)
				RegistryRootKey.SetValue(name, 1);
			else
				RegistryRootKey.SetValue(name, 0);
		}
	}
}
