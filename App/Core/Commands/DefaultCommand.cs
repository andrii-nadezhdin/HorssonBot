using System.Collections.Generic;
using Microsoft.Bot.Connector;
using App.Core.ImageManagers;
using System.Threading.Tasks;

namespace App.Core.Commands
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
                ResultCount = 1, SkipForCount = 7
            };
            var imagePoster = new KorzikDailyPoster(Randomizer, new PatternMatcher());
            return await imagePoster.PostAsync(settings);
        }
    }
}