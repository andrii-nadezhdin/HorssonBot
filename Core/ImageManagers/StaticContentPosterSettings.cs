namespace Core.ImageManagers
{
    internal class StaticContentPosterSettings
    {
        public int PostImageCount { get; set; }
        public int SkipPostWhenLessThen { get; set; }
        public int SkipFirstFromPost { get; set; }
        public int? AvaliableIndexPages { get; set; }
    }
}