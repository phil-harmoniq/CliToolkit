using CliToolkit.Internal;

namespace CliToolkit
{
    public abstract class CliApp : CliCommand
    {
        private AppSettings _userSettings;

        public void Start(string[] args)
        {
            try
            {
                if (_userSettings.ShowHeaderFooter) { _userSettings.HeaderAction.Invoke(); }
                Parse(this, _userSettings, args);
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
    }
}
