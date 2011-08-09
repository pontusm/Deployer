using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Deployer {
	/// <summary>
	/// Handles window position/size persisting.
	/// Based on code by Joel Matthias, http://www.codeproject.com/csharp/restoreformstate.asp
	/// </summary>
	internal class PersistWindowState {
		// event info that allows form to persist extra window state data
		public delegate void WindowStateDelegate(object sender, RegistryKey key);
		public event WindowStateDelegate LoadStateEvent;
		public event WindowStateDelegate SaveStateEvent;

		private static List<PersistWindowState> _windows = new List<PersistWindowState>();

		private Form _window;
		private string _regPath;
		private int _normalLeft;
		private int _normalTop;
		private int _normalWidth;
		private int _normalHeight;
		private FormWindowState _windowState;
		private bool _allowSaveMinimized = false;

		protected PersistWindowState(Form window, string registrypath, bool allowSaveMinimized) {
			_window = window;
			_regPath = string.Format("{0}\\{1}", registrypath, window.GetType().Name);
			_allowSaveMinimized = allowSaveMinimized;

			// subscribe to form's events
			_window.Closing += new CancelEventHandler(OnClosing);
			_window.Resize += new EventHandler(OnResize);
			_window.Move += new EventHandler(OnMove);
			_window.Load += new EventHandler(OnLoad);
			_window.Closed += new EventHandler(OnClosed);
			
			// get initial width and height in case form is never resized
			_normalWidth = _window.Width;
			_normalHeight = _window.Height;
		}

		//public bool AllowSaveMinimized {
		//    set {
		//        _allowSaveMinimized = value;
		//    }
		//}

		/// <summary>
		/// Handles window state persistance for the supplied window.
		/// </summary>
		/// <param name="window">The window to save settings for.</param>
		/// <param name="registrypath">The registry path where settings will be stored. A subkey will be created
		/// with the same name as the window class.</param>
		/// <param name="allowSaveMinimized">Whether to allow storing the window in minimized state.</param>
		internal static void HandlePersistance(Form window, string registrypath, bool allowSaveMinimized) {
			_windows.Add( new PersistWindowState(window, registrypath, allowSaveMinimized) );
		}

		private void OnResize(object sender, EventArgs e) {
			// save width and height
			if (_window.WindowState == FormWindowState.Normal) {
				_normalWidth = _window.Width;
				_normalHeight = _window.Height;
			}
		}

		private void OnMove(object sender, EventArgs e) {
			// save position
			if (_window.WindowState == FormWindowState.Normal) {
				_normalLeft = _window.Left;
				_normalTop = _window.Top;
			}
			// save state
			_windowState = _window.WindowState;
		}

		private void OnClosed(object sender, EventArgs e) {
			_windows.Remove(this);
		}

		private void OnClosing(object sender, CancelEventArgs e) {
			// save position, size and state
			RegistryKey key = Registry.CurrentUser.CreateSubKey(_regPath);
			key.SetValue("Left", _normalLeft);
			key.SetValue("Top", _normalTop);
			key.SetValue("Width", _normalWidth);
			key.SetValue("Height", _normalHeight);

			// check if we are allowed to save the state as minimized (not normally)
			if (!_allowSaveMinimized) {
				if (_windowState == FormWindowState.Minimized)
					_windowState = FormWindowState.Normal;
			}

			key.SetValue("WindowState", (int)_windowState);

			// fire SaveState event
			if (SaveStateEvent != null)
				SaveStateEvent(this, key);
		}

		private void OnLoad(object sender, EventArgs e) {
			// attempt to read state from registry
			RegistryKey key = Registry.CurrentUser.OpenSubKey(_regPath);
			if (key != null) {
				int left = (int)key.GetValue("Left", _window.Left);
				int top = (int)key.GetValue("Top", _window.Top);
				int width = (int)key.GetValue("Width", _window.Width);
				int height = (int)key.GetValue("Height", _window.Height);
				FormWindowState windowState = (FormWindowState)key.GetValue("WindowState", (int)_window.WindowState);

				_window.Location = new Point(left, top);
				_window.Size = new Size(width, height);
				_window.WindowState = windowState;

				EnsureFormVisible(_window);

				// fire LoadState event
				if (LoadStateEvent != null)
					LoadStateEvent(this, key);
			}
		}

		private void EnsureFormVisible(Form form) {
			Rectangle bounds = bounds = Screen.GetWorkingArea(form);

			// Ensure top visible
			if (form.Top < bounds.Top) {
				form.Top = bounds.Top;
			}

			// Move the form up if it is too low
			if (form.Top + 40 > bounds.Bottom) {
				form.Top = bounds.Bottom - 40;
			}

			// MOve form inside screen horizontally
			if (form.Right - 80 < bounds.Left) {
				form.Left = bounds.Left - form.Width + 80;
			}
			if (form.Left + 60 > bounds.Right) {
				form.Left = bounds.Right - 60;
			}
		}
	}
}
