using System;
using System.Text.RegularExpressions;

namespace CliToolkit
{
    internal static class TextHelper
    {
        // https://stackoverflow.com/a/4489046
        internal static readonly Regex _regex = new Regex(
            @"(?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])",
            RegexOptions.IgnorePatternWhitespace);

        internal static string KebabConvert(string str) => _regex.Replace(TrimCommandSuffix(str), "-");

        internal static string TrimCommandSuffix(string name)
        {
            return name.EndsWith("Command", StringComparison.OrdinalIgnoreCase)
                ? name.Substring(0, name.Length - 7)
                : name;
        }
    }
}
