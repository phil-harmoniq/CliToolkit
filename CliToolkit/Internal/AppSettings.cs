using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Reflection;

namespace CliToolkit.Internal
{
    internal class AppSettings
    {
        private string _name;

        internal bool ShowHeaderFooter { get; set; }
        internal Action HeaderAction { get; set; }
        internal Action FooterAction { get; set; }
        internal Action<IConfigurationBuilder> UserConfiguration { get; set; }
        internal Action<IServiceCollection, IConfiguration> UserServiceRegistration { get; set; }
        internal int MenuWidth { get; set; } = 72;
        internal ConsoleColor TitleColor { get; set; } = ConsoleColor.Cyan;
        internal string Version { get; set; }
        internal ConfigurationBuilder ConfigurationBuilder { get; set; }
        internal ServiceCollection ServiceCollection { get; set; }

        internal string Name
        {
            get => _name;
            set
            {
                CustomName = true;
                _name = value;
            }
        }

        internal bool CustomName { get; private set; }

        internal AppSettings()
        {
            var assembly = Assembly.GetEntryAssembly();
            Name = assembly.GetName().Name;
            CustomName = false;
            Version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
            HeaderAction = HeaderActionDefault;
            FooterAction = FooterActionDefault;
        }

        private void HeaderActionDefault()
        {
            var title = $" {Name} v{Version} ";
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
