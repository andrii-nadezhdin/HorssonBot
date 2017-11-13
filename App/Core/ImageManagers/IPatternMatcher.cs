using System.Collections.Generic;

namespace App.Core.ImageManagers
{
    internal interface IPatternMatcher
    {
        IList<string> GetPatternValues(string text, string pattern);
    }
}
