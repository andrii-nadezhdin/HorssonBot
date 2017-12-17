using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Core.Base;

namespace Core.ImageManagers
{
    internal class KorzikDailyPoster
	{
		private const string _rootUri = "https://korzik.net";
		private readonly string _imageUploadUrl = $"{_rootUri}/uploads/posts";
		protected readonly string _eroticaUrl = $"{_rootUri}/erotika";

		private const int _maxTries = 100;

		protected readonly IRandomizer _randomizer;
		protected readonly IPatternMatcher _patternMatcher;

		public KorzikDailyPoster(IRandomizer randomizer, IPatternMatcher patternMatcher)
		{
			_randomizer = randomizer;
			_patternMatcher = patternMatcher;
		}

		public async Task<IList<string>> PostAsync(PosterSettings settings)
		{
			int tries = 0;
			while (tries <= _maxTries)
			{
				tries++;
				var topic = GetTopic();
				if (string.IsNullOrEmpty(topic))
				{
					continue;
				}
				var images = await GetImages(settings, topic);
				if (images == null || images.Count == 0)
				{
					continue;
				}
				return images;
			}
			return null;
		}

		private async Task<IList<string>> GetImages(PosterSettings settings, string topic)
		{
			string html;
			using (var client = new WebClient())
			{
				html = await client.DownloadStringTaskAsync(topic);
			}
			var list = _patternMatcher.GetPatternValues(html, $"<img(.*?)src=\"({_imageUploadUrl}.*?.jpg)\"(.*?)/>");
			if (list.Count == 0)
			{
				return null;
			}
			list.RemoveAt(0);
			if (list.Count > settings.ResultCount && list.Count > settings.SkipForCount)
			{
				return _randomizer.GetRandomFromList(list, settings.ResultCount);
			}
			return null;
		}

		private string GetTopic()
		{
			string pageUrl = $"{_eroticaUrl}/page/{_randomizer.GetRandom(100)}/";
			string html;
			using (var client = new WebClient())
			{
				html = client.DownloadString(pageUrl);
			}
			var topics = _patternMatcher.GetPatternValues(html, $"<a (.*?)href=\"({_eroticaUrl}/.*?eromiks.*?.html)\"(.*?)>");
			if (topics.Count != 0)
			{
				return _randomizer.GetRandomFromList(topics);
			}
			return null;
		}
	}
}