using CliToolkit.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CliToolkit
{
    public class CliAppBuilder<TApp> where TApp : CliApp, new()
    {
        private const int _minWidth = 64;
        private const int _maxWidth = 256;

        private readonly AppSettings _appSettings;

        public CliAppBuilder()
        {
            _appSettings = new AppSettings();
        }

        public TApp Build()
        {
            _appSettings.ConfigurationBuilder = new ConfigurationBuilder();
            _appSettings.ServiceCollection = new ServiceCollection();

            _appSettings.UserConfiguration?.Invoke(_appSettings.ConfigurationBuilder);
            _appSettings.ServiceCollection.AddSingleton<TApp>();
            _appSettings.ServiceCollection.AddOptions();
            _appSettings.UserServiceRegistration?.Invoke(
                _appSettings.ServiceCollection,
                _appSettings.ConfigurationBuilder.Build());

            var services = _appSettings.ServiceCollection.BuildServiceProvider();
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

        public CliAppBuilder<TApp> SetName(string name)
        {
            if (string.IsNullOrEmpty(name)) { throw new CliAppBuilderException("Custom name cannot be null or empty"); }
            _appSettings.Name = name;
            return this;
        }

        public CliAppBuilder<TApp> SetVersion(string version)
        {
            if (string.IsNullOrEmpty(version)) { throw new CliAppBuilderException("Custom version cannot be null or empty"); }
            _appSettings.Version = version;
            return this;
        }

        public CliAppBuilder<TApp> SetMenuWidth(int menuWidth)
        {
            if (menuWidth < _minWidth) { throw new CliAppBuilderException(
                $"Given width {menuWidth} is less than the minimum allowed {_minWidth}"); }
            if (menuWidth > _maxWidth) { throw new CliAppBuilderException(
                $"Given width {menuWidth} is greater than the maximum allowed {_maxWidth}"); }
            _appSettings.MenuWidth = menuWidth;
            return this;
        }

        public CliAppBuilder<TApp> ShowHeaderAndFooter()
        {
            _appSettings.ShowHeaderFooter = true;
            return this;
        }

        public CliAppBuilder<TApp> ShowHeaderAndFooter(ConsoleColor titleColor)
        {
            _appSettings.TitleColor = titleColor;
            _appSettings.ShowHeaderFooter = true;
            return this;
        }

        public CliAppBuilder<TApp> ShowHeaderAndFooter(Action header, Action footer)
        {
            if (header != null) { _appSettings.HeaderAction = header; }
            if (footer != null) { _appSettings.FooterAction = footer; }
            _appSettings.ShowHeaderFooter = true;
            return this;
        }
    }
}
