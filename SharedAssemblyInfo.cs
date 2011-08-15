using System.Reflection;

[assembly: AssemblyProduct("Deployer")]
[assembly: AssemblyCompany("Stendahls.net")]
[assembly: AssemblyCopyright("Copyright (c) Pontus Munck and Stendahls 2011")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyConfiguration("")]

// VERSIONS:
// 0.1.0.0 - First release
// 0.2.0.0 - Added support for scanning from a specific date.
//           + Some bugfixes
// 0.3.0.0 - Glacial list view added for more functionality.
//           Relative local path is now correctly resolved to an absolute path.
// 0.4.0.0 - Plugin support + many bugfixes
// 0.4.1.0 - Support for deploying GWS objects
// 0.4.2.0 - Filtering against project file added
// 0.5.0.0 - Added database deployment via ServerDatabase web service
//           Project file scanning now handles exclude filter correctly
// 0.5.1.0 - Support for selecting what to deploy in a database
// 0.5.2.0 - Recent project list added
//           Support for retry/skip/cancel if an error occurs during deployment
// 0.6.0.0 - Include filters now match against the entire path instead of just the file name
//           Support for deploying files to a specific remote path
//           Added file copy deployment plugin
// 0.6.0.1 - A few bug fixes
// 0.6.1.0 - Fixed a bug with file filter matching.
// 0.6.1.1 - Fixed a bug with exclude directory matching.
// 0.6.2.0 - Added the ability to to exclude files that match a filter.
// 0.6.2.1 - Fixed a problem with transfering empty files.
// 0.6.3.0 - FileCopy plugin can now overwrite readonly files.
//           Now remembers up to 10 recent files.
//           Fixed a bug with the title percentage not being updated correctly.
// 0.7.0.0 - Updated for .NET 2.0
// 1.0.1.1 - "Overwrite/skip All" added to FileCopy plugin
// 1.1.0.0 - FTP deploy root path bug fixed.
//           Added support for deploy timestamping.
// 1.1.0.1 - Bugfix with timestamp verification.
// 1.1.0.2 - Last deploy time is cleared if it cannot be determined
// 1.1.0.3 - Bugfixes
// 1.2.0.0 - Added auto updater
// 1.2.0.1 - Bugfixes
//           Improved error handling for database deployment
// 1.2.0.2 - Fixed stored procedure deployment
// 1.2.1.0 - Now remembers window size
//           Updated web service references
// 1.2.1.1 - Fixed file scanning with VS 2005 project file
// 2.0.0.0 - Major update of GUI and lots of bugfixes
//           New file queue system
// 2.0.0.1 - Fixed minor bug in folder tree
// 2.0.0.2 - Fixed minor bug in folder tree
// 2.0.0.3 - Database deploy was broken
//           File list is now also updated after a scan
// 2.1.0.0 - Folder tree is refreshed after SaveAs and settings changes
//           FTP connection settings can now be tested
//           Support for hierarchical filter rules
//           Allows multiple deploy destinations with the same plugin type
// 2.1.0.1 - Plugins are now properly unloaded when switching projects
// 2.1.0.2 - Fixed minor bug with the timestamp file
// 2.1.0.3 - Settings can now be edited
// 2.1.0.4 - Remote path is now handled correctly for overriding filter settings.
// 2.1.0.5 - Added files are now detected when scanning for modified files.
// 2.1.1.0 - Small bugfix with unknown deployment destinations.
//           Queue can now keep deployed files so that you can re-deploy easily.
// 2.1.1.1 - Minor cosmetic change (toolbar)
// 2.2.0.0 - Fixed optional timestamping.
//           Ability to deploy a file to a different filename on remote location.
//           Time/date info in log window.
//           Rearranging/removing filters can now be done.
// 2.2.1.0 - Folders can now be added to queue.
//           Destinations can be added/removed in the settings.
//           New Project wizard added
// 2.2.1.1 - Optimized UpdateFolderTree performance
// 2.2.1.2 - Reduced memory consumption
//           Non selected files can be removed from the queue.
// 2.2.1.3 - Fixed small bug with stored procedure deployment
// 2.5.0.0 - Added support for WCM object deployment
// 2.5.0.1 - Fixed a bug with plugins remoting proxy object timing out
//           Added the ability to skip WCM objects that fail
//           Debug messages from the WCM deployment service are no longer shown
// 2.6.0.0 - Fix for broken timestamp file
//           Added support for resolving environment variables
//           Multiple configurations can now be used in a single deploy file
// 2.6.0.1 - Shows display name for configurations
// 2.6.0.2 - Some small bugfixes
//           Environment variables can now also be used in GWS object deploy
// 2.6.0.3 - Bugfix in WcmDeploy plugin
// 2.6.0.4 - Adjusted matching rules for filter settings
// 2.6.0.5 - Last upload time is now retrieved when changing configurations
// 2.7.0.0 - New plugin - ServiceController - for stopping/restarting services
//           Improved new project creation wizard
//           Timestamping is no longer required
//           No longer prompts to save project settings unnecessarily
//           Fixed some minor issues and bugs
// 2.7.0.1 - Fixed serialization bug
// 2.7.1.0 - Better visual feedback from ServiceController
// 2.8.0.0 - Split up the plugin code base in order to launch as OSS
//           Fixed a problem with empty wildcards
//           Better handling of problems with timestamp files
//           Improved the New Project wizard a bit
// 2.8.0.1 - Fixed a bug with specifying special filters for the "bin" folder

[assembly: AssemblyVersion("2.8.0.1")]
