namespace Core.Extensions
{
    internal static class StringExtensions
    {
        public static string CombineUrl(this string baseUrl, string path) =>
            $"{baseUrl.TrimEnd('/')}/{path.TrimStart('/')}";
    }
}
