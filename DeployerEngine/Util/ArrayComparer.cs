

namespace DeployerEngine.Util {
	/// <summary>
	/// Help methods for comparison.
	/// </summary>
	/// <remarks>
	/// 2007-03-08 POMU: Class created
	/// </remarks>
	public class ArrayComparer {

		/// <summary>
		/// Compares two byte arrays.
		/// </summary>
		public static bool Compare(byte[] b1, byte[] b2) {
			if (b1.Length != b2.Length)
				return false;
			for (int i = 0; i < b1.Length; i++)
				if (b1[i] != b2[i])
					return false;
			return true;
		}
	}
}