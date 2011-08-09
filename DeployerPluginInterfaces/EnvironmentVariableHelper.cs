using System;
using System.Text.RegularExpressions;

namespace DeployerPluginInterfaces
{
	/// <summary>
	/// Helper class for substituting environment variables.
	/// </summary>
	/// <remarks>
	/// 2010-07-02  POMU: Class created
	/// </remarks>
	public static class EnvironmentVariableHelper
	{
		public static readonly Regex _regex = new Regex(@"%(\w+)%");

		/// <summary>
		/// Resolves the environment variables found in a string.
		/// </summary>
		/// <param name="text">The text to scan.</param>
		public static string ResolveVariables(string text)
		{
			// Replace environment variables in the path
			return _regex.Replace(text,
								  match =>
								  {
									string envname = match.Groups[1].Value;
									string value = Environment.GetEnvironmentVariable(envname);
									return value ?? match.Value;
								  });
		}
	}
}