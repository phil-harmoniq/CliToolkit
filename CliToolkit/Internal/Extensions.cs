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
    }
}
