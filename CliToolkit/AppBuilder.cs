using CliToolkit.Exceptions;
using CliToolkit.Meta;

namespace CliToolkit
{
    /// <summary>
    /// Use an AppBuilder to create a new CliApp with custom configuration.
    /// </summary>
    public class AppBuilder<TApp> where TApp : CliApp, new()
    {
        private readonly TApp _app;
        private const int _minimumWidth = 32;
        private const int _maximumWidth = 128;

        /// <summary>
        /// Creates a new AppBuilder for customizing a new CliApp.
        /// </summary>
        public AppBuilder()
        {
            _app = new TApp();
        }

        /// <summary>
        /// Overrides the application's name with the given value.
        /// </summary>
        public AppBuilder<TApp> SetName(string name)
        {
            ThrowIfEmptyString(name, "SetName() was called with an empty string.");
            _app.AppInfo.Name = name;
            return this;
        }

        /// <summary>
        /// Overrides the application's version with the given value.
        /// </summary>
        public AppBuilder<TApp> SetVersion(string version)
        {
            ThrowIfEmptyString(version, "SetVersion() was called with an empty string.");
            _app.AppInfo.Version = version;
            return this;
        }

        public AppBuilder<TApp> SetWidth(int width)
        {
            if (width > _maximumWidth)
            {
                throw new AppConfigurationException($"Given width {width} is larger than the maximum width {_maximumWidth}");
            }
            if (width < _minimumWidth)
            {
                throw new AppConfigurationException($"Given width {width} is smaller than the minimum width {_minimumWidth}");
            }

            _app.AppInfo.Width = width;
            return this;
        }

        /// <summary>
        /// Builds the configured application.
        /// </summary>
        public TApp Build()
        {
            return _app;
        }

        /// <summary>
        /// Builds and starts the configured application.
        /// </summary>
        /// <param name="args">The arguments provided to the application.</param>
        /// <returns></returns>
        public TApp Start(string[] args)
        {
            return (TApp) _app.Start(args);
        }

        private static void ThrowIfEmptyString(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new AppConfigurationException(message);
            }
        }
    }
}