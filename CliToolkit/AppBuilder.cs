using CliToolkit.Exceptions;

namespace CliToolkit
{
    /// <summary>
    /// Use an AppBuilder to create a new CliApp with custom configuration.
    /// </summary>
    public class AppBuilder<TApp> where TApp : CliApp, new()
    {
        private readonly TApp _app;

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
            _app.Name = name;
            return this;
        }

        /// <summary>
        /// Overrides the application's version with the given value.
        /// </summary>
        public AppBuilder<TApp> SetVersion(string version)
        {
            ThrowIfEmptyString(version, "SetVersion() was called with an empty string.");
            _app.Version = version;
            return this;
        }

        /// <summary>
        /// Sets the application' header and footer text.
        /// </summary>
        /// <param name="header">The new header.</param>
        /// <param name="footer">(Optional) The new footer text.</param>
        /// <returns></returns>
        public AppBuilder<TApp> SetHeader(string header, string footer = null)
        {
            ThrowIfEmptyString(header, "SetHeader() was called with an empty string.");
            if (footer != null) { ThrowIfEmptyString(footer, "Optional footer was empty when calling SetHeader()"); }
            _app._headerText = header;
            _app._footerText = footer;
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