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
                PrintError(ex);
            }
            catch (Exception ex)
            {
                PrintError(ex);
                throw;
            }
            finally
            {
                if (_userSettings.ShowHeaderFooter) { _userSettings.FooterAction.Invoke(); }
            }
        }

        internal void AddAppSettings(AppSettings userSettings)
        {
            _userSettings = userSettings;
        }

        private void PrintError(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }
}
