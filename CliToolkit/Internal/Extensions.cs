using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CliToolkit.Internal
{
    internal static class Extensions
    {
        internal static bool HasPublicSetter(this PropertyInfo prop)
        {
            return prop.CanWrite && prop.GetSetMethod(true).IsPublic;
        }

        internal static bool IsValidShortKey(this char shortKey)
        {
            return char.IsLetter(shortKey);
        }
        internal static IList<PropertyInfo> GetConfigProperties(this IList<PropertyInfo> props)
        {
            return props.Where(p =>
                (p.PropertyType == typeof(string)
                || p.PropertyType == typeof(int)
                || p.PropertyType == typeof(bool))
                && p.HasPublicSetter())
                .ToList();
        }

        internal static IList<PropertyInfo> GetCommandProperties(this IList<PropertyInfo> props)
        {
            return props.Where(p => p.PropertyType.IsSubclassOf(typeof(CliCommand))).ToList();
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

        internal static bool HasAttribute<TAttribute>(this PropertyInfo prop)
            where TAttribute : Attribute
        {
            return prop.GetCustomAttribute<TAttribute>() != null;
        }

        internal static bool EqualsIgnoreCase(this string str1, string str2)
        {
            return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
        }
    }
}
