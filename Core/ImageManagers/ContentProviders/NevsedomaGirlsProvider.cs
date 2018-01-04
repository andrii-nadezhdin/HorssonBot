namespace Core.ImageManagers.ContentProviders
{
    internal class NevsedomaGirlsProvider : IStaticImageProvider
    {
        private const string _schema = "http";
        private const string _rootUri = "nevsedoma.com.ua";

        public string PostPattern => $"<a.*?class=\"s-full-link\".*?href=\"(.*?{_rootUri}/index.php[?]newsid=.*?)\".*?>";
        public string ImagePattern => $"<img.*?src=\"(.*?{_rootUri}.*?jpg)\".*?/>";

        public string GenerateIndexPageUrl(int page)
        {
            return $"{_schema}://{_rootUri}/index.php?do=cat&category=krasivye_devushki_18&cstart={page}";
        }
    }
}
