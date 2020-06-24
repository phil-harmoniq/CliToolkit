using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace CliToolkit
{
    public abstract class CliCommandBase
    {
        internal abstract void Finish(IConfiguration config, string[] args);

        internal void Parse(IServiceCollection sc, IConfiguration config, string[] args)
        {
            if (args.Length > 0)
            {
                var arg = args[0];
                var argsTrim = args.Skip(1).ToArray();
                var type = GetType();
                var properties = type.GetProperties();

                var commandProperties = properties.Where(p => p.PropertyType.IsSubclassOf(typeof(CliCommandBase)));

                var subCommandProperty = commandProperties.FirstOrDefault(p =>
                {
                    var t = p.PropertyType;
                    var attribute = t.GetCustomAttribute<CliCommandRouteAttribute>();
                    return attribute?.Keyword == arg || attribute?.AlternateKeyword == arg;
                });

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
                    subCommand.Parse(sc, config, argsTrim);
                }
                else
                {
                    Finish(config, argsTrim);
                }
            }
            else
            {
                Finish(config, args);
            }
        }
    }
}
