using AnsiCodes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CliToolkit
{
    public abstract class CliAppOld : CliCommandOld
    {
        public CliAppInfo AppInfo { get; } = new CliAppInfo();

        public void Start(string[] args)
        {
            try
            {
                PrintHeader();
                Parse(this, args);
            }
            catch (CliRuntimeException ex)
            {

            }
            finally
            {
                PrintFooter();
            }
        }

        private void PrintHeader()
        {
            var title = $" {AppInfo.Name} v{AppInfo.Version} ";
            var padWidth = (AppInfo.Width - title.Length) / 2;
            var unevenWidth = (AppInfo.Width - title.Length) % 2 != 0;
            var leftPad = new string('-', padWidth);
            var rightPad = new string('-', unevenWidth ? padWidth + 1 : padWidth);
            var output = Environment.NewLine + leftPad + Color.Cyan + Format.Bold + title + Reset.All + rightPad;
            Console.WriteLine(output);
        }

        private void PrintFooter()
        {
            Console.WriteLine(new string('-', AppInfo.Width) + Environment.NewLine);
        }
    }
}
