using CliToolkit.Utilities;

namespace CliToolkit
{
    public abstract class CliApp : CliCommand
    {
        private AppSettings _userSettings;

        public void Start(string[] args)
        {
            try
            {
                //PrintHeader();
                Parse(this, _userSettings ?? new AppSettings(), args);
            }
            finally
            {
                //PrintFooter();
            }
        }

        internal void AddAppSettings(AppSettings userSettings)
        {
            _userSettings = userSettings;
        }
    }
}
