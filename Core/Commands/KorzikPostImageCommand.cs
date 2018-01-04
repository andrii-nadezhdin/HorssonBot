using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Base;
using Core.ImageManagers;
using Core.ImageManagers.ContentProviders;
using Microsoft.Bot.Connector;

namespace Core.Commands
{
    internal class KorzikPostImageCommand : CommandBase
    {
        protected override string ExactCommand => string.Empty;
        protected override string[] ContainsCommands => new[] { string.Empty };
        protected override string[] Responses => new []{ "Держи", "Достаточно спама!", "Ок", "Сейчас поищу", "Лови лошадок", "Игого!", "Готово" };
        protected override int ChanseToResponseInPercent => 20;

        protected override async Task<IList<string>> ExecuteInternalAsync(Activity activity)
        {
            var settings = new StaticContentPosterSettings
            {
                ResultCount = BotState.Instance.Get(activity.Conversation?.Id, Constants.PostCountParameter, 3),
                SkipForCount = 7,
                AvaliableIndexPages = 100
            };
            var imagePoster = new StaticContentImagePoster(Randomizer, new PatternMatcher(), new KorzikEromiksProvider());
            return await imagePoster.PostAsync(settings);
        }
    }
}