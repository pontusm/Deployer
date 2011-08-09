using System;
using System.Xml;

namespace DeployerPlugins
{
	/// <summary>
	/// Contains helper methods for working with XML.
	/// </summary>
	/// <remarks>
	/// 2005-01-19	POMU: Class Created
	/// </remarks>
	public class XmlHelper
	{
		#region Constructors
		private XmlHelper()
		{
		}
		#endregion

		/// <summary>
		/// Returns the first child element found that has the specified name.
		/// </summary>
		/// <param name="parentElement"></param>
		/// <param name="childElementName"></param>
		/// <returns></returns>
		public static XmlElement GetChildElement(XmlElement parentElement, string childElementName) {
			XmlNodeList nodes = parentElement.GetElementsByTagName(childElementName);
			if(nodes.Count == 0)
				return null;
			return nodes[0] as XmlElement;
		}

		/// <summary>
		/// Sets the inner text of a child element. The element will be created if not found.
		/// </summary>
		/// <param name="parentElement"></param>
		/// <param name="childElementName"></param>
		/// <param name="text"></param>
		/// <returns></returns>
		public static XmlElement SetChildElementText(XmlElement parentElement, string childElementName, string text) {
			XmlElement xe = GetChildElement(parentElement, childElementName);
			if(xe == null) {
				xe = parentElement.OwnerDocument.CreateElement(childElementName);
				parentElement.AppendChild(xe);
			}
			xe.InnerText = text;
			return xe;
		}

		/// <summary>
		/// Returns the text within the first child element that matches the specified name, or null if not found.
		/// </summary>
		/// <param name="parentElement"></param>
		/// <param name="childElementName"></param>
		/// <returns></returns>
		public static string GetTextFromChildElement(XmlElement parentElement, string childElementName) {
			XmlElement xe = GetChildElement(parentElement, childElementName);
			if(xe == null)
				return null;
			return xe.InnerText;
		}

		public static int GetIntFromChildElement(XmlElement parentElement, string childElementName, int defaultValue) {
			string text = GetTextFromChildElement(parentElement, childElementName);
			if(text == null)
				return defaultValue;
			return int.Parse(text);
		}
	}
}
