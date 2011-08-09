using System;
using System.Xml;

namespace DeployerPlugins
{
	/// <summary>
	/// Handles settings stored in XML for FTP deployment.
	/// </summary>
	/// <remarks>
	/// 2005-01-18	POMU: Class Created
	/// </remarks>
	internal class FtpDeployerSettings
	{
		#region Private members
		private string _address;
		private int _port = 21;
		private string _path = "";
		private string _login;
		private string _password;
		#endregion

		#region Constructors

		internal FtpDeployerSettings() {
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the address of the remote FTP server.
		/// </summary>
		internal string Address {
			get { return _address; }
			set {
				if(_address != value) {
					_address = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the port to use.
		/// </summary>
		internal int Port {
			get { return _port; }
			set {
				if(_port != value) {
					_port = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the remote path.
		/// </summary>
		internal string Path {
			get { return _path; }
			set {
				string fixedpath = FixRemotePath(value);
				if(_path != fixedpath) {
					_path = fixedpath;
				}
			}
		}

		/// <summary>
		/// Gets or sets the login.
		/// </summary>
		internal string Login {
			get { return _login; }
			set {
				if(_login != value) {
					_login = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		internal string Password {
			get { return _password; }
			set {
				if(_password != value) {
					_password = value;
				}
			}
		}
		
		#endregion

		#region Internal methods

		internal void LoadFromXml(XmlElement settings) {
			_address = XmlHelper.GetTextFromChildElement(settings, "Address");
			_port = XmlHelper.GetIntFromChildElement(settings, "Port", 21);
			_path = XmlHelper.GetTextFromChildElement(settings, "Path");
			_login = XmlHelper.GetTextFromChildElement(settings, "Login");
			_password = XmlHelper.GetTextFromChildElement(settings, "Password");
		}

		internal void SaveToXml(XmlElement settings) {
			XmlHelper.SetChildElementText(settings, "Address", _address);
			XmlHelper.SetChildElementText(settings, "Port", _port.ToString());
			XmlHelper.SetChildElementText(settings, "Path", _path);
			XmlHelper.SetChildElementText(settings, "Login", _login);
			XmlHelper.SetChildElementText(settings, "Password", _password);
		}

		#endregion

		#region Private methods

		private string FixRemotePath(string path) {
			path = path.Replace("\\", "/");
			if (!path.StartsWith("/"))
				path = "/" + path;
			if(!path.EndsWith("/"))
				path += "/";
			return path;
		}

		#endregion
	}
}
