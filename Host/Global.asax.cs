using System.Configuration;
using System.Web.Http;
using Autofac;
using Core;
using Host.Controllers.App_Start;
using Microsoft.ApplicationInsights.Extensibility;
using Core.Base;
using Core.Commands;
using Core.Configuration;
using Core.ImageManagers;

namespace Host
{
	public class WebApiApplication : System.Web.HttpApplication
	{
	    private const string AppInsightsInstrumentationKeyName = "ApplicationInsightsInstrumentationKey";

        protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
		    TelemetryConfiguration.Active.InstrumentationKey = ConfigurationManager.AppSettings[AppInsightsInstrumentationKeyName];
            var builder = new ContainerBuilder();
		    CoreResolver.Configure(builder);
            var container = builder.Build();
			Resolver.SetContainer(container);
		}
	}
}
