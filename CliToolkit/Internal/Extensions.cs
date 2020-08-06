using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CliToolkit.Internal
{
    internal static class Extensions
    {
        internal static IServiceCollection AddConfig<TConfig>(
            this IServiceCollection services, IConfiguration config) where TConfig : class, new()
        {
            services.Configure<TConfig>(config.GetSection(typeof(TConfig).Name));
            services.AddScoped(sp => sp.GetRequiredService<IOptions<TConfig>>().Value);
            return services;
        }

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
