using System.Net;
using System.Threading.Tasks;

namespace Core.ImageManagers.ContentProviders
{
    internal interface IStaticImageProvider
    {
        string PostPattern { get; }
        string ImagePattern { get; }
        string RootUri { get; }
        string GenerateIndexPageUrl(int page);

        async Task<string> GetContentAsync(string url)
        {
            using (var client = new WebClient())
            {
                return await client.DownloadStringTaskAsync(url);
            }
        }
    }
}