using Core.Infrastructure;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;

namespace Core.ImageManagers.ContentProviders
{
    internal class KorzikEromiksProvider : IStaticImageProvider
    {
        private readonly static CookieAwareWebClient _client;

        static KorzikEromiksProvider()
        {
            var values = new NameValueCollection
            {
                { "login_name", Environment.GetEnvironmentVariable("korzik_login_name") },
                { "login_password", Environment.GetEnvironmentVariable("korzik_login_password")  },
                { "login", "submit" }
            };
            _client = new CookieAwareWebClient();
            _client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            _client.UploadValues($"{_schema}://{_rootUri}", "POST", values);
        }

        private const string _schema = "https";
        private const string _rootUri = "korzik.net";

        public string PostPattern => $"<a .*?href=\"(.*?{_rootUri}/erotika/.*?(eromiks|ero-miks).*?.html)\".*?>";
        public string ImagePattern => $"<img.*?src=\"(.*?uploads/posts.*?.jpg)\".*?/>";
        public string RootUri => $"{_schema}://{_rootUri}";


        public string GenerateIndexPageUrl(int page)
        {
            return $"{_schema}://{_rootUri}/erotika/page/{page}/";
        }

        public async Task<string> GetContentAsync(string url)
        {
            return await _client.DownloadStringTaskAsync(url);
        }
    }
}
