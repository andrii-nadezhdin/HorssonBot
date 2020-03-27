namespace Core.ImageManagers.ContentProviders
{
    internal class KorzikEromiksProvider : IStaticImageProvider
    {
        private const string _schema = "https";
        private const string _rootUri = "korzik.net";

        public string PostPattern => $"<a .*?href=\"(.*?{_rootUri}/erotika/.*?(eromiks|ero-miks).*?.html)\".*?>";
        public string ImagePattern => $"<img.*?src=\"(.*?uploads/posts.*?.jpg)\".*?/>";
        public string RootUri => $"{_schema}//{_rootUri}";

        public string GenerateIndexPageUrl(int page)
        {
            return $"{_schema}://{_rootUri}/erotika/page/{page}/";
        }
    }
}
