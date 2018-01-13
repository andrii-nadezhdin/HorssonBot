namespace Core.ImageManagers.ContentProviders
{
    internal class NevsedomaVideoProvider : NevsedomaBaseProvider
    {
        protected override string RaitingPattern => "<span.*?rating.*?>\\+([3-9][0-9]|[1-9][0-9][0-9]).*?</span>";
        protected override string Category => "vzrosloe_jerotika";
        public override string ImagePattern => $"<source.*?src=\"(.*?{_rootUri}.*?(webm|mp4))\".*?>";
    }
}
