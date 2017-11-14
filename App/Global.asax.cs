using Autofac;
using App.Core;
using App.Core.Base;
using App.Core.Commands;
using App.Core.ImageManagers;
using System.Web.Http;

namespace HorssonBot
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
			var builder = new ContainerBuilder();
			builder.RegisterType<Randomizer>().As<IRandomizer>();
			builder.RegisterType<PatternMatcher>().As<IPatternMatcher>();
			builder.RegisterType<CommandResponsible>().As<ICommandResponsible>()
				.SingleInstance()
				.OnActivated(c => c.Instance.RegisterNext<InterractCommand>()
					.RegisterNext<PostImageCommand>()
					.RegisterNext<HelpCommand>()
					.RegisterNext<SetPostCountCommand>()
					.RegisterNext<DefaultCommand>());
			var container = builder.Build();
			Resolver.SetContainer(container);
		}
	}
}
