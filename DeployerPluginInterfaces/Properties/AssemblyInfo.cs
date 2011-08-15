using System.Reflection;
using System.Security;

[assembly: AssemblyTitle("Deployer Plugin Interfaces")]
[assembly: AssemblyDescription("Contains interfaces that Deployer plugins implement.")]

[assembly: AssemblyProduct("Deployer")]
[assembly: AssemblyCompany("Stendahls.net")]
[assembly: AssemblyCopyright("Copyright (c) Pontus Munck and Stendahls 2011")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyConfiguration("")]

// Allow plugins to call back into the DeployerEngine
[assembly: AllowPartiallyTrustedCallers]

[assembly: AssemblyVersion("2.8.0.0")]
