using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Core.Base;
using Core.Extensions;
using Core.ImageManagers.ContentProviders;

namespace Core.ImageManagers
{
    internal class StaticContentImagePoster
    {
        private const int _maxTries = 10;

        protected readonly IRandomizer _randomizer;
		protected readonly IPatternMatcher _patternMatcher;
        protected readonly IStaticImageProvider _imageProvider;

        public StaticContentImagePoster(IRandomizer randomizer, IPatternMatcher patternMatcher, IStaticImageProvider imageProvider)
		{
			_randomizer = randomizer;
			_patternMatcher = patternMatcher;
		    _imageProvider = imageProvider;
        }

        public async Task<List<string>> PostAsync(StaticContentPosterSettings settings)
		{
			int tries = 0;
			while (tries <= _maxTries)
			{
				tries++;
				var topic = await GetTopic(settings);
				if (string.IsNullOrEmpty(topic))
					continue;
				var images = await GetImages(settings, topic);
				if (images == null || images?.Count == 0)
					continue;
				return images;
			}
			return null;
		}

		private async Task<string> GetTopic(StaticContentPosterSettings settings)
		{
            var indexPageUrl = _imageProvider.GenerateIndexPageUrl(_randomizer.GetRandom(settings.AvaliableIndexPages ?? 100));
			var html = await _imageProvider.GetContentAsync(indexPageUrl);
		    var topics = _patternMatcher.GetPatternValues(html, _imageProvider.PostPattern);
			if (topics.Count != 0)
                return _randomizer.GetRandomFromList(topics);
            return null;
		}

        private async Task<List<string>> GetImages(StaticContentPosterSettings settings, string topic)
        {
			var html = await _imageProvider.GetContentAsync(topic);
			var list = _patternMatcher.GetPatternValues(html, _imageProvider.ImagePattern)
                ?.Skip(settings.SkipFirstFromPost)
                .Where(u => Uri.IsWellFormedUriString(u, UriKind.RelativeOrAbsolute))
                .Select(u => Uri.IsWellFormedUriString(u, UriKind.Relative) ?
                    _imageProvider.RootUri.CombineUrl(u) : u)
                .ToList();
            if (list.Count == 0)
                return null;
            if (list.Count >= Constants.ImagePostCount && list.Count >= settings.SkipPostWhenLessThen)
                return _randomizer.GetRandomFromList(list, Constants.ImagePostCount);
            return null;
        }
    }
}