using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace DeployerEngine.Project
{
	/// <summary>
	/// Types of filters that can be used.
	/// </summary>
	public enum FilterExpressionType {
		Wildcard,
		RegExp,
		ExactFileName
	}

	/// <summary>
	/// A file filter.
	/// </summary>
	[Serializable]
	public class Filter {
		private FilterExpressionType _type = FilterExpressionType.RegExp;

		private string _expression;
		//private string _deployPluginIdentifier;
		private string _deployDestinationIdentifier;
		private string _cachedRegularExpression;
		private string _remotePath;
		private string _remoteFileName;

		[NonSerialized]
		private Regex _cachedRegEx;

		public Filter()
        {
			_type = FilterExpressionType.Wildcard;		// Wildcard is default type
		}

		public Filter(FilterExpressionType type, string expression)
		{
		    _type = type;
		    _expression = expression ?? string.Empty;
		}

	    /// <summary>
		/// Initializes a new instance of the <see cref="Filter"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="deployDestinationIdentifier">The deploy destination identifier.</param>
		public Filter(FilterExpressionType type, string expression, string deployDestinationIdentifier) : this(type, expression)
        {
			_deployDestinationIdentifier = deployDestinationIdentifier;
		}

		#region Properties

		/// <summary>
		/// Gets or sets the type of the filter expression.
		/// </summary>
		[XmlAttribute]
		public FilterExpressionType ExpressionType {
			get { return _type; }
			set {
				if(_type != value) {
					_type = value;
					FlushCache();
				}
			}
		}

		/// <summary>
		/// Gets or sets the expression for the filter.
		/// </summary>
		[XmlAttribute]
		public string Expression {
			get { return _expression; }
			set {
				if(_expression != value) {
					_expression = value ?? string.Empty;
					FlushCache();
				}
			}
		}

		/// <summary>
		/// Contains the deploy destination to deploy to.
		/// </summary>
		[XmlAttribute("Destination")]
		public string DeployDestinationIdentifier {
			get { return _deployDestinationIdentifier; }
			set { _deployDestinationIdentifier = value; }
		}

		/// <summary>
		/// The remote path to deploy matching files to. If this is null, the file will be deployed to a path
		/// that matches the local one.
		/// </summary>
		[XmlAttribute("RemotePath")]
		public string RemotePath {
			get { return _remotePath; }
			set { _remotePath = value; }
		}

		/// <summary>
		/// Gets or sets the name that the file will have on the remote location.
		/// </summary>
		/// <value>The name of the remote file.</value>
		[XmlAttribute("RemoteFileName")]
		public string RemoteFileName {
			get { return _remoteFileName; }
			set { _remoteFileName = value; }
		}

		#endregion

		#region Match checking

		/// <summary>
		/// Determines whether the specified text is matching.
		/// </summary>
		/// <param name="text">The text to compare.</param>
		/// <returns>
		/// 	<c>true</c> if the specified text is match; otherwise, <c>false</c>.
		/// </returns>
		public bool IsMatch(string text) {
			if (_type == FilterExpressionType.ExactFileName)
				return string.Compare(Path.GetFileName(text), _expression, true) == 0;

			Regex regex = GetRegularExpressionObject();
			return regex.IsMatch(text);
		}

		/// <summary>
		/// Returns the filter expression as a regular expression object, regardless of its type. The RegEx object
		/// is cached so that it can be retrieved quickly in subsequent calls.
		/// </summary>
		private Regex GetRegularExpressionObject() {
			if(_cachedRegEx == null) {
				_cachedRegEx = new Regex(GetRegularExpressionString(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
			}
			return _cachedRegEx;
		}

		/// <summary>
		/// Returns the filter expression as a regular expression, regardless of its type.
		/// </summary>
		private string GetRegularExpressionString() {
			if (_type == FilterExpressionType.RegExp)
				return _expression;

			// Build regular expression
			if (_cachedRegularExpression == null) {
				// Convert from wildcard type into regular expression
				StringBuilder sb = new StringBuilder(_expression.Length);
				sb.Append('^');
				foreach (char c in _expression) {
					switch (c) {
						case '*':
							sb.Append(".*");
							break;
						case '?':
							sb.Append('.');
							break;
						case '(':
						case ')':
						case '[':
						case ']':
						case '$':
						case '^':
						case '.':
						case '{':
						case '}':
						case '|':
						case '\\':
							sb.Append('\\');
							sb.Append(c);
							break;
						default:
							sb.Append(c);
							break;
					}
				}
				sb.Append('$');

				_cachedRegularExpression = sb.ToString();
			}
			return _cachedRegularExpression;
		}

		#endregion

		/// <summary>
		/// Flushes the cached objects. Used when the expression changes.
		/// </summary>
		private void FlushCache() {
			_cachedRegEx = null;
			_cachedRegularExpression = null;
		}

		public override string ToString() {
			return _expression;
		}
	}
}
