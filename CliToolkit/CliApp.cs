using AnsiCodes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CliToolkit
{
    public abstract class CliApp : CliCommand
    {
        public IConfiguration Configuration { get; internal set; }
        public IServiceCollection ServiceCollection { get; internal set; }
        public int ExitCode { get; internal set; }
        public string Name { get; internal set; }
        public string Version { get; internal set; }

        internal int Width { get; set; }

        public int Start(string[] args)
        {
            try
            {
                PrintHeader();
                Parse(ServiceCollection, Configuration, args);
                return ExitCode = 0;
            }
            catch (Exception ex)
            {
                if (!ex.Message.Equals($"Exception of type '{ex.GetType().ToString()}' was thrown."))
                {
                    Console.WriteLine($"{Color.Red}{Format.Bold}{ex.Message}{Reset.All}");
                }
                var logger = ServiceCollection.BuildServiceProvider().GetService<ILogger>();
                logger?.LogError(ex, ex.Message);
                return ExitCode = 1;
            }
            finally
            {
                PrintFooter();
            }
        }

        private void PrintHeader()
        {
            var title = $" {Name} v{Version} ";
            var padWidth = (Width - title.Length) / 2;
            var unevenWidth = (Width - title.Length) % 2 != 0;
            var leftPad = new string('-', padWidth);
            var rightPad = new string('-', unevenWidth ? padWidth + 1 : padWidth);
            var output = Environment.NewLine + leftPad + Color.Cyan + Format.Bold + title + Reset.All + rightPad;
            Console.WriteLine(output);
        }

        private void PrintFooter()
        {
            Console.WriteLine(new string('-', Width) + Environment.NewLine);
        }
    }
}
