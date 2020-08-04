using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Reflection;

namespace CliToolkit.Internal
{
    internal class AppSettings
    {
        internal AssemblyName AppInfo { get; }
        public FileVersionInfo VersionInfo { get; }

        internal bool ShowHeaderFooter { get; set; }
        internal Action HeaderAction { get; set; }
        internal Action FooterAction { get; set; }
        internal Action<IConfigurationBuilder> UserConfiguration { get; set; }
        internal Action<IServiceCollection, IConfiguration> UserServiceRegistration { get; set; }
        internal int MenuWidth { get; set; } = 72;
        internal ConsoleColor TitleColor { get; set; } = ConsoleColor.Cyan;

        internal AppSettings()
        {
            var assembly = Assembly.GetEntryAssembly();
            AppInfo = assembly.GetName();
            VersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            HeaderAction = HeaderActionDefault;
            FooterAction = FooterActionDefault;
        }

        private void HeaderActionDefault()
        {
            var title = $" {AppInfo.Name} v{VersionInfo.ProductVersion} ";
            var padWidth = (MenuWidth - title.Length) / 2;
            var unevenWidth = (MenuWidth - title.Length) % 2 != 0;
            var leftPad = new string('-', padWidth);
            var rightPad = new string('-', unevenWidth ? padWidth + 1 : padWidth);
            Console.Write(Environment.NewLine + leftPad);
            Console.ForegroundColor = TitleColor;
            Console.Write(title);
            Console.ResetColor();
            Console.Write(rightPad + Environment.NewLine);
        }

        private void FooterActionDefault()
        {
            Console.WriteLine(new string('-', MenuWidth) + Environment.NewLine);
        }
    }
}
