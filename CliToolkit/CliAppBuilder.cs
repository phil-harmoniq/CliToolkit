using CliToolkit.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CliToolkit
{
    public class CliAppBuilder<TApp> where TApp : CliApp, new()
    {
        private readonly AppSettings _appSettings;

        public CliAppBuilder()
        {
            _appSettings = new AppSettings();
        }

        public TApp Build()
        {
            var cb = new ConfigurationBuilder();
            var sc = new ServiceCollection();
            _appSettings.UserConfiguration?.Invoke(cb);
            sc.AddSingleton<TApp>();
            _appSettings.UserServiceRegistration?.Invoke(sc, cb.Build());
            var services = sc.BuildServiceProvider();
            var app = services.GetRequiredService<TApp>();
            app.AddAppSettings(_appSettings);
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
            _appSettings.UserConfiguration = configure;
            return this;
        }

        public CliAppBuilder<TApp> RegisterServices(Action<IServiceCollection, IConfiguration> registerServices)
        {
            _appSettings.UserServiceRegistration = registerServices;
            return this;
        }

        public CliAppBuilder<TApp> SetMenuWidth(int menuWidth)
        {
            _appSettings.MenuWidth = menuWidth;
            return this;
        }

        public CliAppBuilder<TApp> ShowHeaderAndFooter(Action header = null, Action footer = null)
        {
            if (header != null) { _appSettings.HeaderAction = header; }
            if (footer != null) { _appSettings.FooterAction = footer; }
            _appSettings.ShowHeaderFooter = true;
            return this;
        }
    }
}
