using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CliToolkit
{
    public abstract class CliCommand : CliCommandBase
    {
        protected abstract void OnExecute(string[] args);

        internal override void Finish(IConfiguration config, string[] args)
        {
            OnExecute(args);
        }
    }

    public abstract class CliCommand<TOptions> : CliCommandBase where TOptions : class
    {
        protected abstract void OnExecute(TOptions options, string[] args);

        internal readonly Type OptionsType = typeof(TOptions);

        internal Dictionary<string, string> GetSwitchMaps()
        {
            var baseName = OptionsType.Name;
            var properties = OptionsType.GetProperties();
            return properties.ToDictionary(p => $"--{p.Name}", p => $"--{baseName}:{p.Name}");
        }

        internal override void Finish(IConfiguration config, string[] args)
        {
            var newConfig = new ConfigurationBuilder()
                .AddConfiguration(config)
                .AddCommandLine(args)
                .Build();

            var options = newConfig.GetSection(OptionsType.Name)
                .Get<TOptions>();

            OnExecute(options, args);
        }
    }
}
