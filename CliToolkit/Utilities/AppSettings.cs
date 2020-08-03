using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace CliToolkit.Utilities
{
    internal class AppSettings
    {
        internal AssemblyName AppInfo { get; }

        internal bool ShowHeaderFooter { get; set; }
        internal Action HeaderAction { get; set; }
        internal Action FooterAction { get; set; }
        internal Action<IConfigurationBuilder> UserConfiguration { get; set; }
        internal Action<IServiceCollection, IConfiguration> UserServiceRegistration { get; set; }
        internal int MenuWidth { get; set; } = 72;

        internal AppSettings()
        {
            AppInfo = Assembly.GetEntryAssembly().GetName();
        }

        private void HeaderActionDefault()
        {
            var title = $" {AppInfo.Name} v{AppInfo.Version} ";
            var padWidth = (MenuWidth - title.Length) / 2;
            var unevenWidth = (MenuWidth - title.Length) % 2 != 0;
            var leftPad = new string('-', padWidth);
            var rightPad = new string('-', unevenWidth ? padWidth + 1 : padWidth);
            Console.Write(Environment.NewLine + leftPad);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(title, ConsoleColor.Cyan);
            Console.ResetColor();
            Console.Write(rightPad + Environment.NewLine);
        }

        private void FooterActionDefault()
        {
            Console.WriteLine(new string('-', MenuWidth) + Environment.NewLine);
        }
    }
}
