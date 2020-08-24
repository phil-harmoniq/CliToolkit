using CliToolkit.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CliToolkit
{
    /// <summary>
    /// The entrypoint for a single command within the command-line application.
    /// </summary>
    public abstract class CliCommand
    {
        private readonly Type _type;
        private readonly bool _isAppRoot;
        private readonly CliOptionsAttribute _optionsAttribute;
        private readonly IList<PropertyInfo> _configurationProperties;
        private readonly IList<PropertyInfo> _commandProperties;
        private readonly IList<PropertyInfo> _implicitBoolProperties;

        private string _namespace;
        private CliCommand _parent;
        private AppSettings _appSettings;

        /// <summary>
        /// The entrypoint for a single command within the command-line application.
        /// </summary>
        public CliCommand()
        {
            _type = GetType();
            _isAppRoot = _type.IsSubclassOf(typeof(CliApp));
            _optionsAttribute = _type.GetCustomAttribute<CliOptionsAttribute>();

            var allProps = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            _configurationProperties = allProps.GetConfigProperties();
            _commandProperties = allProps.GetCommandProperties();
            _implicitBoolProperties = _configurationProperties
                .Where(p => p.PropertyType == typeof(bool) && !p.HasAttribute<CliExplicitBoolAttribute>())
                .ToList();
        }

        /// <summary>
        /// The entrypoint for the current command.
        /// </summary>
        /// <param name="args">The filtered arguments left after parsing.</param>
        public abstract void OnExecute(string[] args);

        /// <summary>
        /// Display a friendly help menu for this commands sub-commands and/or options.
        /// </summary>
        public void PrintHelpMenu() => HelpMenu.Print(_type, _commandProperties, _configurationProperties);

        internal void Parse(CliCommand caller, AppSettings appSettings, string[] args)
        {
            _parent = caller;
            _appSettings = appSettings;
            var namespaces = GetNamespaceList(new List<string>());
            _namespace = string.Join(":", namespaces);

            if (args.Length > 0)
            {
                var subCommandProp = FindMatchingSubCommand(args[0]);
                if (subCommandProp != null)
                {
                    if (!subCommandProp.CanWrite)
                    {
                        throw new Exception($"Property {subCommandProp.Name} must have a public setter for injection.");
                    }

                    var serviceProvider = GetServiceProvider(subCommandProp, args);
                    subCommandProp.SetValue(this, serviceProvider.GetRequiredService(subCommandProp.PropertyType));
                    var val = subCommandProp.GetValue(this, null);
                    var subCommand = (CliCommand)val;
                    subCommand.Parse(this, appSettings, args.Skip(1).ToArray());
                }
                else
                {
                    Start(args);
                }
            }
            else
            {
                Start(args);
            }
        }

        internal List<string> GetNamespaceList(List<string> namespaceList)
        {
            if (_isAppRoot) { return namespaceList; }
            if (string.IsNullOrEmpty(_optionsAttribute?.Namespace)) { namespaceList.Insert(0, _type.Name); }
            else { namespaceList.Insert(0, _optionsAttribute.Namespace); }
            return _parent.GetNamespaceList(namespaceList);
        }

        private void Start(string[] args)
        {
            string[] filteredArgs = new string[0];
            if (_configurationProperties.Count > 0)
            {
                var switchMaps = _configurationProperties.GetSwitchMaps(_namespace);
                var implicitSwitchMaps = _implicitBoolProperties.GetSwitchMaps(_namespace);
                filteredArgs = args.Except(implicitSwitchMaps.Keys, StringComparer.OrdinalIgnoreCase).ToArray();
                var configBuilder = new ConfigurationBuilder();
                _appSettings.UserConfiguration?.Invoke(configBuilder);
                var configWithoutCli = configBuilder.Build();
                configBuilder.AddCommandLine(filteredArgs, switchMaps);
                var config = configBuilder.Build();

                var commandName = _optionsAttribute?.Namespace ?? _type.Name;
                IConfiguration configSection;
                if (_isAppRoot) { configSection = config; }
                else { configSection = config.GetSection(commandName); }

                IConfiguration configSectionWithoutCli;
                if (_isAppRoot) { configSectionWithoutCli = config; }
                else { configSectionWithoutCli = config.GetSection(commandName); }

                InjectConfigProperties(args, switchMaps, configSection, configSectionWithoutCli);

                for (var i = 0; i < filteredArgs.Length; i++)
                {
                    if (filteredArgs[i] != null && switchMaps.Keys.ContainsOrStartsWith(filteredArgs[i]))
                    {
                        if (!filteredArgs[i].Contains("=") && i + 1 < filteredArgs.Length)
                        {
                            filteredArgs[i + 1] = null;
                        }
                        filteredArgs[i] = null;
                    }
                }
            }

            OnExecute(filteredArgs.Where(s => !string.IsNullOrEmpty(s)).ToArray());
        }

        private void InjectConfigProperties(string[] args,
            Dictionary<string, string> switchMaps,
            IConfiguration configSection,
            IConfiguration configSectionWithoutCli)
        {
            foreach (var prop in _configurationProperties)
            {
                var explicitBoolAttr = prop.GetCustomAttribute<CliExplicitBoolAttribute>();
                var value = configSection[prop.Name];

                if (explicitBoolAttr == null && prop.PropertyType == typeof(bool))
                {
                    value = configSectionWithoutCli[prop.Name] ?? "False";
                    var keys = switchMaps.Where(sm => sm.Value.Equals($"{_namespace}:{prop.Name}"))
                        .Select(sm => sm.Key);
                    if (args.Intersect(keys, StringComparer.OrdinalIgnoreCase).Any())
                    {
                        value = "True";
                    }
                    if (!string.IsNullOrEmpty(value))
                    {
                        prop.SetValue(this, bool.Parse(value));
                    }
                }
                else if (!string.IsNullOrEmpty(value))
                {
                    if (prop.PropertyType == typeof(int))
                    {
                        prop.SetValue(this, int.Parse(value));
                    }
                    else if (prop.PropertyType == typeof(bool))
                    {
                        bool val;
                        bool.TryParse(value, out val);
                        prop.SetValue(this, val);
                    }
                    else
                    {
                        prop.SetValue(this, value);
                    }
                }
            }
        }

        private PropertyInfo FindMatchingSubCommand(string arg)
        {
            return _commandProperties.FirstOrDefault(prop =>
            {
                var aliases = new List<string>
                {
                    prop.Name,
                    prop.Name.KebabConvert()
                };
                return aliases.Contains(arg, StringComparer.OrdinalIgnoreCase);
            });
        }

        private IServiceProvider GetServiceProvider(PropertyInfo subCommandProp, string[] args)
        {
            var subType = subCommandProp.PropertyType;
            var subProps = subType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var configProps = subProps.GetConfigProperties();
            var switchMaps = configProps.GetSwitchMaps(_namespace);
            _appSettings.ConfigurationBuilder.AddCommandLine(args, switchMaps);
            var config = _appSettings.ConfigurationBuilder.Build();
            _appSettings.ServiceCollection.AddSingleton(subCommandProp.PropertyType);
            _appSettings.UserServiceRegistration?.Invoke(_appSettings.ServiceCollection, config);
            return _appSettings.ServiceCollection.BuildServiceProvider();
        }
    }
}
