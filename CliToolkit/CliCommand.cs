using AnsiCodes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CliToolkit
{
    public abstract class CliCommand
    {
        // https://stackoverflow.com/a/4489046
        private static readonly Regex _regex = new Regex(
            @"(?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])",
            RegexOptions.IgnorePatternWhitespace);
        private string KebabConvert(string str) => _regex.Replace(TrimCommandSuffix(str), "-");
        protected abstract void OnExecute(string[] args);

        internal Type Type { get; private set; }
        internal IList<PropertyInfo> ConfigurationProperties { get; private set; }
        internal IList<PropertyInfo> CommandProperties { get; private set; }
        internal string ParsedName { get; set; }

        private const int _menuPadLength = 4;
        private readonly string _menuPad = new string(' ', _menuPadLength);

        private IServiceProvider _serviceProvider;

        public virtual void PrintHelpMenu()
        {
            PrintInformation();
        }

        private void PrintInformation()
        {
            Console.WriteLine();

            if (CommandProperties.Count > 0)
            {
                Console.WriteLine("  Commands:");

                foreach (var prop in CommandProperties)
                {
                    var attr = prop.GetCustomAttribute<CliDescriptionAttribute>();

                    if (attr != null)
                    {
                        WriteLineWithPad($"{KebabConvert(prop.Name).ToLower()}    {attr.Description}");
                    }
                    else
                    {
                        WriteLineWithPad(KebabConvert(prop.Name).ToLower());
                    }
                }
            }

            if (ConfigurationProperties.Count > 0)
            {
                Console.WriteLine($"{Environment.NewLine}  Options:");

                foreach (var prop in ConfigurationProperties)
                {
                    var attr = prop.GetCustomAttribute<CliDescriptionAttribute>();

                    if (attr != null)
                    {
                        WriteLineWithPad($"--{KebabConvert(prop.Name).ToLower()}    {attr.Description}");
                    }
                    else
                    {
                        WriteLineWithPad($"--{KebabConvert(prop.Name).ToLower()}");
                    }
                }
            }

            Console.WriteLine();
        }

        private void WriteLineWithPad(string str)
        {
            Console.WriteLine(_menuPad + str);
        }

        internal void Parse(IServiceCollection services, IConfiguration config, string[] args)
        {
            _serviceProvider = services.BuildServiceProvider();

            Reflect();

            if (args.Length > 0)
            {
                var subCommandProperty = FindMatchingSubCommand(args);

                if (subCommandProperty != null)
                {
                    services.AddSingleton(subCommandProperty.PropertyType);
                    _serviceProvider = services.BuildServiceProvider();

                    if (!subCommandProperty.CanWrite)
                    {
                        throw new Exception($"Property {subCommandProperty} must have a public setter for injection.");
                    }

                    subCommandProperty.SetValue(this, _serviceProvider.GetRequiredService(subCommandProperty.PropertyType));
                    var val = subCommandProperty.GetValue(this, null);
                    var subCommand = (CliCommand)val;
                    subCommand.Parse(services, config, args.Skip(1).ToArray());
                }
            }
            else
            {
                InjectProperties(config, args);
            }
        }

        private void Reflect()
        {
            Type = GetType();

            var restrictedProps = Type.IsSubclassOf(typeof(CliApp))
                ? typeof(CliApp).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name)
                : new string[0];

            var currentProps = Type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !restrictedProps.Contains(p.Name));

            ConfigurationProperties = currentProps.Where(p =>
                p.PropertyType == typeof(string)
                || p.PropertyType == typeof(int)
                || p.PropertyType == typeof(bool))
                .ToList();

            CommandProperties = currentProps.Where(p =>
                p.PropertyType.IsSubclassOf(typeof(CliCommand)))
                .ToList();

            ParsedName = Type.Name.EndsWith("Command")
                ? Type.Name.Substring(0, Type.Name.Length - 7)
                : Type.Name;
        }

        private void InjectProperties(IConfiguration config, string[] args)
        {
            if (ConfigurationProperties.Count > 0)
            {
                var switchMaps = GetSwitchMaps();
                var newConfig = new ConfigurationBuilder()
                    .AddConfiguration(config)
                    .AddCommandLine(args, switchMaps)
                    .Build();

                var namespaceAttr = Type.GetCustomAttribute<CliNamespaceAttribute>();

                var section = newConfig.GetSection(namespaceAttr == null
                    ? Type.Name
                    : namespaceAttr.Namespace);

                foreach (var prop in ConfigurationProperties)
                {
                    var value = section[prop.Name];
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (prop.PropertyType == typeof(int))
                        {
                            prop.SetValue(this, int.Parse(value));
                        }
                        else if (prop.PropertyType == typeof(bool))
                        {
                            prop.SetValue(this, bool.Parse(value));
                        }
                        else
                        {
                            prop.SetValue(this, value);
                        }
                    }
                }
            }

            OnExecute(args);
        }

        private Dictionary<string, string> GetSwitchMaps()
        {
            var baseName = Type.Name;
            var switchMaps = new Dictionary<string, string>();

            foreach (var prop in ConfigurationProperties)
            {
                var kebabCase = KebabConvert(prop.Name);
                var menuAttribute = prop.GetCustomAttribute<CliDescriptionAttribute>();
                switchMaps.Add($"--{prop.Name}", $"{baseName}:{prop.Name}");

                if (kebabCase != prop.Name)
                {
                    switchMaps.Add($"--{kebabCase}", $"{baseName}:{prop.Name}");
                }

                if (menuAttribute != null && menuAttribute.ShortFlag != default(char))
                {
                    switchMaps.Add($"-{menuAttribute.ShortFlag}", $"{baseName}:{prop.Name}");
                }
            }

            return switchMaps;
        }

        private PropertyInfo FindMatchingSubCommand(string[] args)
        {
            return CommandProperties.FirstOrDefault(p =>
            {
                var type = p.PropertyType;
                var parsedName = TrimCommandSuffix(type.Name);
                var kebabName = KebabConvert(parsedName);
                var arg = args[0];
                var attribute = p.GetCustomAttribute<CliDescriptionAttribute>();

                if (arg.Equals(ParsedName, StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals(kebabName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                if (attribute != null && attribute.ShortFlag != default(char) &&
                    arg.Equals(attribute.ShortFlag.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                return false;
            });
        }

        private string TrimCommandSuffix(string name)
        {
            return name.EndsWith("Command", StringComparison.OrdinalIgnoreCase)
                ? name.Substring(0, name.Length - 7)
                : name;
        }
    }
}
