using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CliToolkit
{
    public abstract class CliCommandBase
    {
        private static readonly Regex KebabCaseRegex = new Regex(@"(?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

        internal Type Type { get; private set; }
        internal IList<PropertyInfo> ConfigurationProperties { get; private set; }
        internal IList<PropertyInfo> CommandProperties { get; private set; }
        internal string ParsedName { get; set; }

        private string KebabConvert(string str)
        {
            return KebabCaseRegex.Replace(str, "-");
        }

        internal abstract void Finish(IConfiguration config, string[] args);

        internal void Parse(IServiceCollection sc, IConfiguration config, string[] args)
        {
            Type = GetType();
            var properties = Type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            ConfigurationProperties = properties.Where(p => p.PropertyType == typeof(string) || p.PropertyType == typeof(int)
                || p.PropertyType == typeof(bool)).ToList();
            CommandProperties = properties.Where(p => p.PropertyType.IsSubclassOf(typeof(CliCommand))).ToList();
            ParsedName = Type.Name.EndsWith("Command")
                ? Type.Name.Substring(0, Type.Name.Length - 7)
                : Type.Name;

            if (args.Length > 0)
            {
                var subCommandProperty = FindMatchingSubCommand(args);

                if (subCommandProperty != null)
                {
                    sc.AddSingleton(subCommandProperty.PropertyType);
                    var sp = sc.BuildServiceProvider();

                    if (!subCommandProperty.CanWrite)
                    {
                        throw new Exception($"Property {subCommandProperty} must have a public setter for injection.");
                    }

                    subCommandProperty.SetValue(this, sp.GetRequiredService(subCommandProperty.PropertyType));
                    var val = subCommandProperty.GetValue(this, null);
                    var subCommand = (CliCommandBase)val;
                    subCommand.Parse(sc, config, args.Skip(1).ToArray());
                }
                else
                {
                    Finish(config, args);
                }
            }
            else
            {
                Finish(config, args);
            }
        }

        internal Dictionary<string, string> GetSwitchMaps(Type type)
        {
            var baseName = Type.Name;
            var switchMaps = new Dictionary<string, string>();

            foreach (var prop in ConfigurationProperties)
            {
                var kebabCase = KebabConvert(prop.Name);
                var menuAttribute = prop.GetCustomAttribute<CliMenuAttribute>();
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
                var parsedName = type.Name.EndsWith("Command")
                    ? type.Name.Substring(0, type.Name.Length - 7)
                    : type.Name;
                var kebabName = KebabConvert(parsedName);
                var arg = args[0];
                var attribute = p.GetCustomAttribute<CliMenuAttribute>();

                if (arg.Equals(ParsedName, StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals(kebabName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                if (attribute.ShortFlag != default(char) &&
                    arg.Equals(attribute.ShortFlag.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                return false;
            });
        }
    }
}
