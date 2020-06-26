using AnsiCodes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CliToolkit
{
    public abstract class CliApp : CliCommand
    {
        public CliAppInfo AppInfo { get; } = new CliAppInfo();

        public int Start(string[] args)
        {
            try
            {
                PrintHeader();
                Parse(this, args);
                return AppInfo.ExitCode = 0;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    Console.WriteLine($"{Color.Red}{Format.Bold}{ex.Message}{Reset.All}");
                }
                var logger = AppInfo.ServiceCollection.BuildServiceProvider().GetService<ILogger>();
                logger?.LogError(ex, ex.Message);
                return AppInfo.ExitCode = 1;
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
