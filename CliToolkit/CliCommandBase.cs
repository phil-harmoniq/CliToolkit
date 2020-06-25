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
        internal abstract void Finish(IConfiguration config, string[] args);

        internal void Parse(IServiceCollection sc, IConfiguration config, string[] args)
        {
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
            var regex = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])",RegexOptions.IgnorePatternWhitespace);

            var baseName = type.Name;
            var properties = type.GetProperties();
            var switchMaps = new Dictionary<string, string>();

            foreach (var prop in properties)
            {
                var kebabCase = regex.Replace(prop.Name, "-");
                switchMaps.Add($"--{prop.Name}", $"{baseName}:{prop.Name}");
                switchMaps.Add($"--{kebabCase}", $"{baseName}:{prop.Name}");
            }

            return switchMaps;
        }

        private PropertyInfo FindMatchingSubCommand(string[] args)
        {
            return GetType()
                .GetProperties()
                .Where(p => p.PropertyType.IsSubclassOf(typeof(CliCommandBase)))
                .FirstOrDefault(p =>
                {
                    var arg = args[0];
                    var t = p.PropertyType;
                    var attribute = t.GetCustomAttribute<CliCommandRouteAttribute>();
                    return attribute?.Keyword == arg || attribute?.AlternateKeyword == arg;
                });
        }
    }
}
