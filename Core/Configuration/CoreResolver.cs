using Autofac;
using Core.Base;
using Core.Commands;
using Core.ImageManagers;

namespace Core.Configuration
{
    public static class CoreResolver
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterType<Randomizer>().As<IRandomizer>();
            builder.RegisterType<PatternMatcher>().As<IPatternMatcher>();
            builder.RegisterType<CommandResponsible>().As<ICommandResponsible>()
                .SingleInstance()
                .OnActivated(c => c.Instance.RegisterNext<InterractCommand>()
                    .RegisterNext<HelpCommand>()
                    .RegisterNext<SetPostCountCommand>()
                    .RegisterNext<NevsedomaPostImageCommand>()
                    .RegisterNext<KorzikPostImageCommand>());
        }
    }
}
