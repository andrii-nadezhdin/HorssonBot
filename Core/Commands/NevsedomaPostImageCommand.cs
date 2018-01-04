using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Base;
using Core.ImageManagers;
using Core.ImageManagers.ContentProviders;
using Microsoft.Bot.Connector;

namespace Core.Commands
{
    internal class NevsedomaPostImageCommand : CommandBase
    {
        protected override string ExactCommand => "POST2";
        protected override string[] ContainsCommands => new[] {
            "ДРУГ", "АЛЬТЕРНАТИВ", "НЕВСЕДОМА", "ПИНГ", "ТЕСТ"
        };
        protected override string[] Responses => new []{ "Слушаюсь и повинуюсь", "Да будет так", "Ок", "Прррр!", "Игого!"};
        protected override int ChanseToResponseInPercent => 30;

        protected override async Task<IList<string>> ExecuteInternalAsync(Activity activity)
        {
            var settings = new StaticContentPosterSettings
            {
                ResultCount = BotState.Instance.Get(activity.Conversation?.Id, Constants.PostCountParameter, 3),
                SkipForCount = 7,
                AvaliableIndexPages = 300
            };
            var imagePoster = new StaticContentImagePoster(Randomizer, new PatternMatcher(), new NevsedomaGirlsProvider());
            return await imagePoster.PostAsync(settings);
        }
    }
}