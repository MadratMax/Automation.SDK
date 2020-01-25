 // ReSharper disable once CheckNamespace
 // Trick to auto-use extension where possible
namespace System
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static IEnumerable<string> ReverseStringFormat(this string value, string template)
        {
            var pattern = "^" + Regex.Replace(template, @"\{[0-9]+\}", "(.*?)") + "$";

            var regex = new Regex(pattern);
            var match = regex.Match(value);

            for (int groupIndex = 1; groupIndex < match.Groups.Count; groupIndex++)
            {
                yield return match.Groups[groupIndex].Value;
            }
        }
    }
}
