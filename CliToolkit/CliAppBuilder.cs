using CliToolkit.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CliToolkit
{
    /// <summary>
    /// Create and customize a new <see cref="CliApp"/> instance.
    /// </summary>
    /// <typeparam name="TApp">The user-defined class deriving from <see cref="CliApp"/>.</typeparam>
    public class CliAppBuilder<TApp> where TApp : CliApp
    {
        private const int _minWidth = 64;
        private const int _maxWidth = 256;

        private readonly AppSettings _appSettings;

        /// <summary>
        /// Create and customize a new <see cref="CliApp"/> instance.
        /// </summary>
        public CliAppBuilder()
        {
            _appSettings = new AppSettings();
        }

        /// <summary>
        /// Finalize creation of the cusomized <typeparamref name="TApp"/> instance.
        /// </summary>
        /// <returns>A built command-line application ready to run.</returns>
        public TApp Build()
        {
            _appSettings.ConfigurationBuilder = new ConfigurationBuilder();
            _appSettings.ServiceCollection = new ServiceCollection();

            _appSettings.UserConfiguration?.Invoke(_appSettings.ConfigurationBuilder);
            _appSettings.ServiceCollection.AddSingleton<TApp>();
            _appSettings.UserServiceRegistration?.Invoke(
                _appSettings.ServiceCollection,
                _appSettings.ConfigurationBuilder.Build());

            var services = _appSettings.ServiceCollection.BuildServiceProvider();
            var app = services.GetRequiredService<TApp>();
            app.AddAppSettings(_appSettings);
            return app;
        }

        /// <summary>
        /// Start the command-line application.
        /// </summary>
        /// <param name="args">The arguments to be pased for execution.</param>
        /// <returns>A built command-line application that finished execution.</returns>
        public TApp Start(string[] args)
        {
            var app = Build();
            app.Start(args);
            return app;
        }

        /// <summary>
        /// Add custom configuration providers.
        /// </summary>
        /// <param name="configure">The user-defined delegate to register configuration providers.</param>
        public CliAppBuilder<TApp> Configure(
            Action<IConfigurationBuilder> configure)
        {
            _appSettings.UserConfiguration = configure;
            return this;
        }

        /// <summary>
        /// Add custom service registrations.
        /// </summary>
        /// <param name="registerServices">The user-defined delegate to register services.</param>
        public CliAppBuilder<TApp> RegisterServices(
            Action<IServiceCollection, IConfiguration> registerServices)
        {
            _appSettings.UserServiceRegistration = registerServices;
            return this;
        }

        /// <summary>
        /// Set a custom name for the application. This will be dipslayed in the default help menu.
        /// </summary>
        public CliAppBuilder<TApp> SetName(string name)
        {
            if (string.IsNullOrEmpty(name)) { throw new CliAppBuilderException(
                "Custom name cannot be null or empty"); }

            _appSettings.Name = name;
            return this;
        }

        /// <summary>
        /// Set a custom version for the application. This will be dipslayed in the default help menu.
        /// </summary>
        public CliAppBuilder<TApp> SetVersion(string version)
        {
            if (string.IsNullOrEmpty(version)) { throw new CliAppBuilderException(
                "Custom version cannot be null or empty"); }

            _appSettings.Version = version;
            return this;
        }

        /// <summary>
        /// Enable the default header and footer. Displays assembly name and product version.
        /// </summary>
        /// <param name="width">Custom header/footer width. Must be between 64 and 256.</param>
        public CliAppBuilder<TApp> ShowHeaderAndFooter(int width = 72)
        {
            ValidateWidth(width);
            _appSettings.ShowHeaderFooter = true;
            return this;
        }

        /// <summary>
        /// Enable the default header and footer. Displays assembly name and product version.
        /// </summary>
        /// <param name="titleColor">Custom title color.</param>
        /// <param name="width">Custom header/footer width. Must be between 64 and 256.</param>
        /// <returns></returns>
        public CliAppBuilder<TApp> ShowHeaderAndFooter(ConsoleColor titleColor, int width = 72)
        {
            ValidateWidth(width);
            _appSettings.TitleColor = titleColor;
            _appSettings.ShowHeaderFooter = true;
            return this;
        }

        /// <summary>
        /// Register and enable a custom header and footer
        /// </summary>
        /// <param name="header">The header action takes place before app execution.</param>
        /// <param name="footer">The footer action takes place after execution has been completed.</param>
        /// <returns></returns>
        public CliAppBuilder<TApp> ShowHeaderAndFooter(Action header, Action footer)
        {
            if (header != null) { _appSettings.HeaderAction = header; }
            if (footer != null) { _appSettings.FooterAction = footer; }
            _appSettings.ShowHeaderFooter = true;
            return this;
        }

        private void ValidateWidth(int width)
        {
            if (width < _minWidth)
            {
                throw new CliAppBuilderException(
                    $"Given width {width} is less than the minimum allowed {_minWidth}");
            }
            if (width > _maxWidth)
            {
                throw new CliAppBuilderException(
                    $"Given width {width} is greater than the maximum allowed {_maxWidth}");
            }
        }
    }
}
