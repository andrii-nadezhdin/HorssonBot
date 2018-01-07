namespace Core.ImageManagers.ContentProviders
{
    internal class NevsedomaPhotoSessionProvider : NevsedomaBaseProvider
    {
        protected override string RaitingPattern => "<span.*?rating.*?>\\+([3-9][0-9]|[1-9][0-9][0-9]).*?</span>";
        protected override string Category => "yeroticheskie-fotosessii";
    }
}
