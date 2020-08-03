using CliToolkit.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CliToolkit
{
    public class CliAppBuilder<TApp> where TApp : CliApp, new()
    {
        private Action<IConfigurationBuilder> _userConfiguration;
        private Action<IServiceCollection, IConfiguration> _userServices;

        public CliAppBuilder()
        {
        }

        public TApp Build()
        {
            var app = new TApp();
            var appSettings = new AppSettings
            {
                UserConfiguration = _userConfiguration,
                UserServices = _userServices,
            };
            app.AddAppSettings(appSettings);
            return app;
        }

        public TApp Start(string[] args)
        {
            var app = Build();
            app.Start(args);
            return app;
        }

        public CliAppBuilder<TApp> Configure(Action<IConfigurationBuilder> configure)
        {
            _userConfiguration = configure;
            return this;
        }

        public CliAppBuilder<TApp> RegisterServices(Action<IServiceCollection, IConfiguration> register)
        {
            _userServices = register;
            return this;
        }
    }
}
