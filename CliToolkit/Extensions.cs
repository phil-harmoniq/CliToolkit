using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CliToolkit
{
    public static class Extensions
    {
        public static IServiceCollection AddConfig<TConfig>(
            this IServiceCollection services, IConfiguration config) where TConfig : class, new()
        {
            services.Configure<TConfig>(config.GetSection(typeof(TConfig).Name));
            services.AddScoped(sp => sp.GetRequiredService<IOptions<TConfig>>().Value);
            return services;
        }
    }
}
