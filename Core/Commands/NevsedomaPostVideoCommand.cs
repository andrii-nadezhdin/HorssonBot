using System.Collections.Generic;
using System.Threading.Tasks;
using Core.ImageManagers;
using Core.ImageManagers.ContentProviders;
using Microsoft.Bot.Schema;

namespace Core.Commands
{
    internal class NevsedomaPostVideoCommand : CommandBase
    {
        protected override string ExactCommand => "MOVIE";
        protected override string[] ContainsCommands => new[] {
            "ВИДЕО", "КИНО", "СИНЕМА", "ЭКШН"
        };
        protected override string[] Responses => new []{ "Синема, синема, синема... от тебя мы без ума!", "Скоро в кинотеатрах", "Тыгыдым-тыгыдым-тыгыдым"};
        protected override int ChanseToResponseInPercent => 10;

        protected override async Task<List<string>> ExecuteInternalAsync(IMessageActivity activity)
        {
            var settings = new StaticContentPosterSettings
            {
                SkipPostWhenLessThen = 1,
                SkipFirstFromPost = 0,
                AvaliableIndexPages = 40
            };
            var imagePoster = new StaticContentImagePoster(Randomizer, new PatternMatcher(), new NevsedomaVideoProvider());
            return await imagePoster.PostAsync(settings);
        }
    }
}