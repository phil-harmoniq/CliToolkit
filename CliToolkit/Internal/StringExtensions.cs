using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CliToolkit.Internal
{
    internal static class StringExtensions
    {
        // https://stackoverflow.com/a/4489046
        internal static readonly Regex _regex = new Regex(
            @"(?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])",
            RegexOptions.IgnorePatternWhitespace);

        internal static string KebabConvert(this string str)
        {
            return _regex.Replace(str, "-");
        }

        internal static bool IsValidShortKey(this char shortKey)
        {
            return char.IsLetter(shortKey);
        }

        internal static bool EqualsIgnoreCase(this string str1, string str2)
        {
            return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
        }

        internal static bool ContainsOrStartsWith(this IEnumerable<string> targetValues, string value)
        {
            if (targetValues.Contains(value, StringComparer.OrdinalIgnoreCase)) { return true; }

            foreach (var targetValue in targetValues)
            {
                if (value.StartsWith(targetValue, StringComparison.OrdinalIgnoreCase)
                    && value.Contains("=")) { return true; }
            }

            return false;
        }
    }
}
