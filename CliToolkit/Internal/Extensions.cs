using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
    }
}
