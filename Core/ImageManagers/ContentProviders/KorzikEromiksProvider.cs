﻿using Core.Infrastructure;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;

namespace Core.ImageManagers.ContentProviders
{
    internal class KorzikEromiksProvider : IStaticImageProvider
    {
        private const string _schema = "https";
        private const string _rootUri = "korzik.net";

        public string PostPattern => $"<a .*?href=\"(.*?{_rootUri}/erotika/.*?(eromiks|ero-miks).*?.html)\".*?>";
        public string ImagePattern => $"<img.*?src=\"(.*?uploads/posts.*?.jpg)\".*?/>";
        public string RootUri => $"{_schema}://{_rootUri}";


        public string GenerateIndexPageUrl(int page)
        {
            return $"{_schema}://{_rootUri}/erotika/page/{page}/";
        }

        public async Task<string> GetTopicContentAsync(string url)
        {
            var values = new NameValueCollection
            {
                { "login_name", Environment.GetEnvironmentVariable("korzik_login_name") },
                { "login_password", Environment.GetEnvironmentVariable("korzik_login_password")  },
                { "login", "submit" }
            };
            using var client = new CookieAwareWebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var result = client.UploadValues(RootUri, "POST", values);
            var sessionId = client.ResponseCookies["PHPSESSID"]?.Value;
            if (string.IsNullOrWhiteSpace(sessionId))
                throw new ArgumentNullException($"{nameof(sessionId)} should not be empty or null");
            client.Headers.Add(HttpRequestHeader.Cookie, sessionId);
            return await client.DownloadStringTaskAsync(url);
        }
    }
}
