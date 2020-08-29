using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CliToolkit.Internal
{
    internal static class PropertyInfoExtensions
    {
        internal static bool HasPublicSetter(this PropertyInfo prop)
        {
            return prop.CanWrite && prop.GetSetMethod(true).IsPublic;
        }

        internal static IEnumerable<PropertyInfo> GetCommandProperties(this IEnumerable<PropertyInfo> props)
        {
            return props.Where(p => p.PropertyType.IsSubclassOf(typeof(CliCommand)));
        }

        internal static bool HasAttribute<TAttribute>(this PropertyInfo prop)
            where TAttribute : Attribute
        {
            return prop.GetCustomAttribute<TAttribute>() != null;
        }

        internal static IEnumerable<PropertyInfo> GetConfigProperties(this IEnumerable<PropertyInfo> props)
        {
            return props.Where(p =>
                (p.PropertyType == typeof(string)
                || p.PropertyType == typeof(int)
                || p.PropertyType == typeof(bool))
                && p.HasPublicSetter());
        }

        internal static Dictionary<string, string> GetSwitchMaps(
            this IEnumerable<PropertyInfo> properties, string @namespace)
        {
            var switchMaps = new Dictionary<string, string>();
            foreach (var prop in properties)
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
