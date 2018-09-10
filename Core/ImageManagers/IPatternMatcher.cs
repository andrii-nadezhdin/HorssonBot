using System.Collections.Generic;

namespace Core.ImageManagers
{
    internal interface IPatternMatcher
    {
        List<string> GetPatternValues(string text, string pattern);
    }
}
