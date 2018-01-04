using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core.ImageManagers
{
    internal class PatternMatcher : IPatternMatcher
    {
        public IList<string> GetPatternValues(string text, string pattern)
        {
            var matches = Regex.Matches(text, pattern, RegexOptions.Compiled);
            var list = new List<string>();
            foreach (Match match in matches)
            {
                var value = match.Groups[1]?.Value;
                if (!string.IsNullOrEmpty(value) && !list.Contains(value))
                {
                    list.Add(value);
                }
            }
            return list;
        }
    }
}