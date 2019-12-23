using System.Collections.Generic;
using System.Threading.Tasks;
using Core.ImageManagers;
using Core.ImageManagers.ContentProviders;
using Microsoft.Bot.Schema;

namespace Core.Commands
{
    internal class NevsedomaGirlsPostImageCommand : CommandBase
    {
        protected override string ExactCommand => "POST";
        protected override string[] ContainsCommands => new[] {
            "ДРУГ", "АЛЬТЕРНАТИВ", "НЕВСЕДОМА", "ПИНГ", "ТЕСТ"
        };
        protected override string[] Responses => new []{ "Слушаюсь и повинуюсь", "Да будет так", "Ок", "Прррр!", "Игого!"};
        protected override int ChanseToResponseInPercent => 30;

        protected override async Task<List<string>> ExecuteInternalAsync(IMessageActivity activity)
        {
            var settings = new StaticContentPosterSettings
            {
                SkipPostWhenLessThen = 8,
                SkipFirstFromPost = 5,
                AvaliableIndexPages = 500
            };
            var imagePoster = new StaticContentImagePoster(Randomizer, new PatternMatcher(), new NevsedomaGirlsProvider());
            var returnToList = new List<string>();
            for (var i = 0; i < Constants.ImagePostCount; i++)
            {
                returnToList.AddRange(await imagePoster.PostAsync(settings));
            }
            return returnToList;
        }
    }
}