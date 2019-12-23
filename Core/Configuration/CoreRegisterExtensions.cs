using Core;
using Core.Base;
using Core.ImageManagers;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Configuration
{
    public static class CoreDependenciesExtension
    {
        public static IServiceCollection RegisterCoreDependencies(this IServiceCollection services)
        {
            services.AddTransient<IRandomizer, Randomizer>();
            services.AddSingleton<IPatternMatcher, PatternMatcher>();
            services.AddSingleton<ICommandResponsible, CommandResponsible>();

            return services;
        }
    }
}
