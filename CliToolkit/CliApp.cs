using CliToolkit.Internal;
using System;

namespace CliToolkit
{
    /// <summary>
    /// The root of a command-line application.
    /// </summary>
    public abstract class CliApp : CliCommand
    {
        private AppSettings _userSettings;

        /// <summary>
        /// The exit code reported upon completion of the command route.
        /// </summary>
        public int ExitCode { get; private set; }

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <param name="args">The arguments provided from the console.</param>
        public void Start(string[] args)
        {
            try
            {
                if (_userSettings.ShowHeaderFooter) { _userSettings.HeaderAction.Invoke(); }
                Parse(this, _userSettings, args);
            }
            catch (CliException ex)
            {
                ExitCode = ex.ExitCode;
                if (!ex.HasDefaultExcepionMessage())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (ex.ShowStackTrace)
                    {
                        var exName = ex.GetType().FullName;
                        Console.WriteLine($"Unhandled exception. {exName}: {ex.Message}");
                        Console.WriteLine(ex.StackTrace);
                    }
                    else
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (CliAppBuilderException ex)
            {
                ExitCode = 1;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(nameof(CliAppBuilderException));
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                ExitCode = 1;
                var exName = ex.GetType().FullName;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Unhandled exception. {exName}: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Console.ResetColor();
                if (_userSettings.ShowHeaderFooter) { _userSettings.FooterAction.Invoke(); }
            }
        }

        internal void AddAppSettings(AppSettings userSettings)
        {
            _userSettings = userSettings;
        }
    }
}
