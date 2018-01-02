using System.Configuration;
using System.Linq;
using System.Web.Http;
using Autofac;
using Host.Controllers.App_Start;
using Microsoft.ApplicationInsights.Extensibility;
using Core.Base;
using Core.Configuration;

namespace Host
{
	public class WebApiApplication : System.Web.HttpApplication
	{
	    private const string AppInsightsInstrumentationKeyName = "ApplicationInsightsInstrumentationKey";

        protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
		    if (ConfigurationManager.AppSettings.AllKeys.Any(k => k == AppInsightsInstrumentationKeyName))
		    {
		        TelemetryConfiguration.Active.InstrumentationKey =
		            ConfigurationManager.AppSettings[AppInsightsInstrumentationKeyName];
		    }
		    else
		    {
		        TelemetryConfiguration.Active.DisableTelemetry = true;
            }   
            var builder = new ContainerBuilder();
		    CoreResolver.Configure(builder);
            var container = builder.Build();
			Resolver.SetContainer(container);
		}
	}
}
