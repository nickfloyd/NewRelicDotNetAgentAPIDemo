NewRelic .Net Agent API
==========================

Use this to help you implement the New Relic .net Agent API methods.  This .net MVC 4 site uses the various methods available in the New Relic .net agent API and provides examples on different use cases for each.

The New Relic .net API docs are available [here](https://newrelic.com/docs/dotnet/AgentApi).


Requirements
----------------

*  Visual Studio 2012
*  .net 4.5
*  ASP .net MVC 4
*  Nuget 2.7

Note: If you are using Visual Studio 2010 you can do the following after cloning:  
git checkout v2010

Setup
----------------

1. Open the solution
2. Add virtual directory via Project Properties >> Web >> Create Virtual Directory (or you can alternatively use IIS express)
3. Run or deploy the project on / to a server with the [NewRelic .net Agent Installed](https://download.newrelic.com/dot_net_agent/release/)
4. If local the resulting endpoint will be: http://localhost/NewRelicDotNetAgentAPIDemo

Note:  Package restore is enabled on this solution, so when you build the project will attempt to download missing packages.
