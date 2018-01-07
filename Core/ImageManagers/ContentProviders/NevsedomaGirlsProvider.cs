namespace Core.ImageManagers.ContentProviders
{
    internal class NevsedomaGirlsProvider : NevsedomaBaseProvider
    {
        protected override string RaitingPattern => "<span.*?rating.*?>\\+([5-9]|[1-9][0-9]|[1-9][0-9][0-9]).*?</span>";
        protected override string Category => "krasivye_devushki_18";
    }
}
