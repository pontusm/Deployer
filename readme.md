# Deployer

The Deployer is a desktop application used for deploying files to remote servers. It has been in used for over six
years at [Stendahls](http://www.stendahls.se) for the daily development work. We use it to deploy files, special
objects and database changes for a range of large scale websites.

Deployment can be performed to more than one destination and is controlled by filters. The tool can automatically
deploy files of a certain type or within a certain folder to a specific destination. The method of deployment is
extensible through the use of plugins. Out of the box we support deploying to file shares and FTP but it is quite
easy to build new plugins to support more destination types.

At work we have used this successfully to deploy special files to a web service that automatically deploys them
as objects in a content management database.
