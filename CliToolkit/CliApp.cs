using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CliToolkit.Arguments;
using CliToolkit.Exceptions;
using CliToolkit.Meta;
using CliToolkit.Utilities;

namespace CliToolkit
{
    /// <summary>
    /// Inherit this class to build a new CLI application.
    /// </summary>
    public abstract class CliApp : ICommand
    {
        private bool _headerWasShown = false;
        private int _width = 64;

        internal string _headerText;
        internal string _footerText;
        
        /// <summary>
        /// This application's name.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// This application's assembly version.
        /// </summary>
        public string Version { get; internal set; }

        /// <summary>
        /// Contains meta-data about this application and its environment.
        /// </summary>
        public AppInfo AppInfo { get; private set; }

        /// <summary>
        /// The exit code after running <see cref="OnExecute" />
        /// </summary>
        /// <value>Any non-zero value indicates an error during execution.</value>
        public int ExitCode { get; internal set; }

        /// <summary>
        /// Constructs a new <see cref="CliApp" /> instance. This should not be called directly.
        /// </summary>
        protected CliApp()
        {
            AppInfo = new AppInfo();
            Name = AppInfo.Assembly.GetName().Name;
            Version = AppInfo.FileVersionInfo.ProductVersion;

            _headerText = CompileHeaderText();
            _footerText = CompileFooterText();
        }

        /// <summary>
        /// <param name="args">The arguments passed to this application.</param>
        /// Defines the default behavior when this application is executed.
        /// </summary>
        public abstract void OnExecute(string[] args);

        // /// <summary>
        // /// Prints the auto-generated help menu. Override this method for a custom menu.
        // /// </summary>
        // public virtual void PrintHelpMenu()
        // {
        // }

        /// <summary>
        /// Begins the application's execution cycle.
        /// </summary>
        /// <param name="args">The arguments passed to this application.</param>
        /// <example>Call using the application's main entrypoint:
        /// <code>
        /// static void Main(string[] args)
        /// {
        ///     var app = new AppBuilder&lt;Program&gt;().Build();
        ///     app.Start(args);
        /// }
        /// </code>
        /// </example>
        /// <returns></returns>
        public CliApp Start(string[] args)
        {
            try
            {
                ArgParser.ParseArgs(this, args);
            }
            catch (AppRuntimeException exception)
            {
                Console.WriteLine($"Error:{AppInfo.NewLine}{exception.Message}");
            }
            finally
            {
                if (_headerWasShown) { PrintFooter(); }
            }
            return this;
        }

        /// <summary>
        /// Prints the app header. The footer will also be printed after execution is complete.
        /// </summary>
        public void PrintHeader()
        {
            _headerWasShown = true;
            Console.WriteLine(_headerText);
        }

        private void PrintFooter()
        {
            if (!string.IsNullOrEmpty(_footerText))
            {
                Console.WriteLine(_footerText);
            }
        }

        private string CompileHeaderText()
        {
            var title = $" {Name} {Version} ";
            if (title.Length >= _width) { return $"-{title}-"; }

            var titleLengthIsOdd = title.Length % 2 == 1;
            var barLength = (_width - title.Length) / 2;

            var leftBar = new string('-', barLength);
            var rightBar = new string('-', titleLengthIsOdd ? barLength + 1 : barLength);

            return $"{AppInfo.NewLine}{leftBar}{title}{rightBar}";
        }

        private string CompileFooterText()
        {
            return $"{new string('-', _width)}{AppInfo.NewLine}";
        }
    }
}