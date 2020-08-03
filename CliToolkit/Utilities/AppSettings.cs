using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CliToolkit.Utilities
{
    internal class AppSettings
    {
        internal Action<IConfigurationBuilder> UserConfiguration { get; set; }
        internal Action<IServiceCollection, IConfiguration> UserServices { get; set; }
    }
}
