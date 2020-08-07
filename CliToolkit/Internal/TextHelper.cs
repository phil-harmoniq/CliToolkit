using System.Text.RegularExpressions;

namespace CliToolkit.Internal
{
    internal static class TextHelper
    {
        // https://stackoverflow.com/a/4489046
        internal static readonly Regex _regex = new Regex(
            @"(?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])",
            RegexOptions.IgnorePatternWhitespace);

        internal static string KebabConvert(string str) => _regex.Replace(str, "-");
    }
}
