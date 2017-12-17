using System.Collections.Generic;
using System.Threading.Tasks;
using Core.ImageManagers;
using Microsoft.Bot.Connector;

namespace Core.Commands
{
    internal class DefaultCommand : CommandBase
    {
        protected override string ExactCommand => string.Empty;
        protected override string[] Responses => new[] { "Ок", "Есть", "Готово" };
        protected override string[] ContainsCommands => new[] { string.Empty };
        protected override int ChanseToResponseInPercent => 20;

        protected override async Task<IList<string>> ExecuteInternalAsync(Activity activity)
        {
            var settings = new PosterSettings
            {
                ResultCount = 2, SkipForCount = 7
            };
            var imagePoster = new KorzikDailyPoster(Randomizer, new PatternMatcher());
            return await imagePoster.PostAsync(settings);
        }
    }
}