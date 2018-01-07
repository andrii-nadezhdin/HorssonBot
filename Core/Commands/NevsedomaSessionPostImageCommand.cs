using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Base;
using Core.ImageManagers;
using Core.ImageManagers.ContentProviders;
using Microsoft.Bot.Connector;

namespace Core.Commands
{
    internal class NevsedomaSessionPostImageCommand : CommandBase
    {
        protected override string ExactCommand => "PROSESSION";
        protected override string[] ContainsCommands => new[] {
            "ПРОФЕССИОНАЛ", "КУЛЬТУР", "ОДЕТАЯ", "ОДЕТУЮ", "В ОДЕЖДЕ"
        };
        protected override string[] Responses => new []{ "Слушаюсь и повинуюсь", "Да будет так", "Ок", "Прррр!", "Игого!"};
        protected override int ChanseToResponseInPercent => 30;

        protected override async Task<IList<string>> ExecuteInternalAsync(Activity activity)
        {
            var settings = new StaticContentPosterSettings
            {
                PostImageCount = 1,
                SkipPostWhenLessThen = 7,
                SkipFirstFromPost = 2,
                AvaliableIndexPages = 70
            };
            var imagePoster = new StaticContentImagePoster(Randomizer, new PatternMatcher(), new NevsedomaPhotoSessionProvider());
            var imageCount = BotState.Instance.Get(activity.Conversation?.Id, Constants.PostCountParameter, 3);
            var returnToList = new List<string>();
            for (var i = 0; i < imageCount; i++)
            {
                returnToList.AddRange(await imagePoster.PostAsync(settings));
            }
            return returnToList;
        }
    }
}