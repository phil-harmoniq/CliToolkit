using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CliToolkit.Internal
{
    internal static class TextHelper
    {
        // https://stackoverflow.com/a/4489046
        internal static readonly Regex _regex = new Regex(
            @"(?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])",
            RegexOptions.IgnorePatternWhitespace);

        internal static string KebabConvert(this string str) => _regex.Replace(str, "-");

        internal static Dictionary<string, string> GetSwitchMaps(
            IList<PropertyInfo> configProperties, string @namespace)
        {
            var switchMaps = new Dictionary<string, string>();
            foreach (var prop in configProperties)
            {
                switchMaps.Add($"--{prop.Name}", $"{@namespace}:{prop.Name}");
                var kebabName = prop.Name.KebabConvert();
                if (!kebabName.Equals(prop.Name, StringComparison.OrdinalIgnoreCase))
                {
                    switchMaps.Add($"--{kebabName}", $"{@namespace}:{prop.Name}");
                }
                var propOptions = prop.GetCustomAttribute<CliOptionsAttribute>();
                if (propOptions?.ShortKey != null && propOptions.ShortKey.IsValidShortKey())
                {
                    var shortKey = $"-{propOptions.ShortKey}";
                    if (switchMaps.ContainsKey(shortKey))
                    {
                        throw new CliAppBuilderException(
                            $"Cannot assign duplicate short-keys: {shortKey}");
                    }
                    switchMaps.Add(shortKey, $"{@namespace}:{prop.Name}");
                }
            }
            return switchMaps;
        }
    }
}
