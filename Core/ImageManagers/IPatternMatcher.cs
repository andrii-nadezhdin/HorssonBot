using System.Collections.Generic;

namespace Core.ImageManagers
{
    internal interface IPatternMatcher
    {
        IList<string> GetPatternValues(string text, string pattern);
    }
}
