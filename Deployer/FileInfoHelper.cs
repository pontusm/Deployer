

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Deployer {
	/// <summary>
	/// Helper class for retrieving file information.
	/// </summary>
	/// <remarks>
	/// 2007-01-31 POMU: Class created
	/// </remarks>
	public class FileInfoHelper {

		struct SHFILEINFO {
			public IntPtr hIcon;
			public int iIcon;
			public int dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		}

		[Flags]
		enum FileInfoFlags : int {
			SHGFI_ICON = 0x000000100,     // get icon
			SHGFI_DISPLAYNAME = 0x000000200,     // get display name
			SHGFI_TYPENAME = 0x000000400,     // get type name
			SHGFI_ATTRIBUTES = 0x000000800,     // get attributes
			SHGFI_ICONLOCATION = 0x000001000,     // get icon location
			SHGFI_EXETYPE = 0x000002000,     // return exe type
			SHGFI_SYSICONINDEX = 0x000004000,     // get system icon index
			SHGFI_LINKOVERLAY = 0x000008000,     // put a link overlay on icon
			SHGFI_SELECTED = 0x000010000,     // show icon in selected state
			SHGFI_ATTR_SPECIFIED = 0x000020000,     // get only specified attributes
			SHGFI_LARGEICON = 0x000000000,     // get large icon
			SHGFI_SMALLICON = 0x000000001,     // get small icon
			SHGFI_OPENICON = 0x000000002,     // get open icon
			SHGFI_SHELLICONSIZE = 0x000000004,     // get shell size icon
			SHGFI_PIDL = 0x000000008,     // pszPath is a pidl
			SHGFI_USEFILEATTRIBUTES = 0x000000010,     // use passed dwFileAttribute
			SHGFI_ADDOVERLAYS = 0x000000020,     // apply the appropriate overlays
			SHGFI_OVERLAYINDEX = 0x000000040,     // Get the index of the overlay in 
			// the upper 8 bits of the iIcon
		}

		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		static extern Int64 SHGetFileInfo(string pszPath, int dwFileAttributes, out SHFILEINFO psfi, int cbFileInfo, FileInfoFlags uFlags);

		/// <summary>
		/// Creates a new bitmap with the small icon for a file.
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static Bitmap GetFileIconAsBitmap(string filename) {
			SHFILEINFO fi;
			SHGetFileInfo(filename, 0, out fi, Marshal.SizeOf(typeof(SHFILEINFO)), FileInfoFlags.SHGFI_ICON | FileInfoFlags.SHGFI_SMALLICON);
			using (Icon icon = Icon.FromHandle(fi.hIcon)) {
				Bitmap bm = new Bitmap(icon.Size.Width, icon.Size.Height);
				using (Graphics g = Graphics.FromImage(bm))
					g.DrawIcon(icon, 0, 0);
				return bm;
			}
		}
	}
}