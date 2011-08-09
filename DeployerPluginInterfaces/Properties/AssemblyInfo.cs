using System.Reflection;
using System.Security;

[assembly: AssemblyTitle("Deployer Plugin Interfaces")]
[assembly: AssemblyDescription("Contains interfaces that Deployer plugins implement.")]

// Allow plugins to call back into the DeployerEngine
[assembly: AllowPartiallyTrustedCallers]

