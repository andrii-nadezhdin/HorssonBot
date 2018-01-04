namespace Core.ImageManagers.ContentProviders
{
    internal interface IStaticImageProvider
    {
        string PostPattern { get; }
        string ImagePattern { get; }
        string GenerateIndexPageUrl(int page);
    }
}