using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace CliToolkit
{
    public abstract class CliCommand : CliCommandBase
    {
        protected abstract void OnExecute(string[] args);

        internal override void Finish(IConfiguration config, string[] args)
        {
            if (ConfigurationProperties.Count > 0)
            {
                var switchMaps = GetSwitchMaps(Type);
                var newConfig = new ConfigurationBuilder()
                    .AddConfiguration(config)
                    .AddCommandLine(args, switchMaps)
                    .Build();

                var section = newConfig.GetSection(Type.Name);

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
    }

    public abstract class CliCommand<TOptions> : CliCommandBase where TOptions : class
    {
        protected abstract void OnExecute(TOptions options, string[] args);

        internal readonly Type OptionsType = typeof(TOptions);

        internal override void Finish(IConfiguration config, string[] args)
        {
            var switchMaps = GetSwitchMaps(typeof(TOptions));
            var newConfig = new ConfigurationBuilder()
                .AddConfiguration(config)
                .AddCommandLine(args, switchMaps)
                .Build();

            var section = newConfig.GetSection(OptionsType.Name);
            var options = section.Get<TOptions>();

            OnExecute(options, args);
        }
    }
}
