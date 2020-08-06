using CliToolkit.Internal;
using System;

namespace CliToolkit
{
    public abstract class CliApp : CliCommand
    {
        private AppSettings _userSettings;

        public int ExitCode { get; private set; }

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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            catch (CliAppBuilderException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to initialized the given command.");
                Console.WriteLine(ex.Message);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw;
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
