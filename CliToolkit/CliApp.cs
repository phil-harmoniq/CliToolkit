using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CliToolkit.Arguments;
using CliToolkit.Exceptions;
using CliToolkit.Meta;
using CliToolkit.Core;

namespace CliToolkit
{
    /// <summary>
    /// Inherit this class to build a new CLI application.
    /// </summary>
    public abstract class CliApp : ICommand
    {
        /// <summary>
        /// Contains meta-data about this application and its environment.
        /// </summary>
        public AppInfo AppInfo { get; } = new AppInfo();

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
        }

        /// <summary>
        /// Defines the default behavior when this application is executed.
        /// </summary>
        /// <param name="args">The arguments passed to this application.</param>
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
                if (TextHelpers.HeaderWasShown) { TextHelpers.PrintFooter(this); }
            }
            return this;
        }

        /// <summary>
        /// Prints the app header. The footer will also be printed after execution is complete.
        /// </summary>
        public void PrintHeader()
        {
            TextHelpers.PrintHeader(this);
        }

        /// <summary>
        /// Prints a help menu that list all available commands and/or arguments contained in this CliApp.
        /// </summary>
        public void PrintHelpMenu()
        {
            TextHelpers.PrintHelpMenu(this);
        }
    }
}