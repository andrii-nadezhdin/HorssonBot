using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Base;
using Core.ImageManagers;
using Core.ImageManagers.ContentProviders;
using Microsoft.Bot.Schema;

namespace Core.Commands
{
    internal class KorzikPostImageCommand : CommandBase
    {
        protected override string ExactCommand => string.Empty;
        protected override string[] ContainsCommands => new[] { string.Empty };
        protected override string[] Responses => new []{ "Держи", "Достаточно спама!", "Ок", "Сейчас поищу", "Лови лошадок", "Игого!", "Готово" };
        protected override int ChanseToResponseInPercent => 20;

        protected override async Task<List<string>> ExecuteInternalAsync(IMessageActivity activity)
        {
            var settings = new StaticContentPosterSettings
            {
                SkipPostWhenLessThen = 7,
                SkipFirstFromPost = 1,
                AvaliableIndexPages = 100
            };
            var imagePoster = new StaticContentImagePoster(Randomizer, new PatternMatcher(), new KorzikEromiksProvider());
            return await imagePoster.PostAsync(settings);
        }
    }
}