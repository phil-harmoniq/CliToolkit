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
        private readonly bool _isAppRoot;
        private readonly CliOptionsAttribute _optionsAttribute;
        private readonly PropertyInfo[] _implicitBoolProperties;

        private string[] _namespaceList;
        private string _namespace;

        internal AppSettings AppSettings { get; private set; }
        internal CliCommand Parent { get; private set; }
        internal Type Type { get; }
        internal PropertyInfo[] ConfigurationProperties { get; }
        internal PropertyInfo[] CommandProperties { get; }

        /// <summary>
        /// The entrypoint for a single command within the command-line application.
        /// </summary>
        public CliCommand()
        {
            Type = GetType();
            _isAppRoot = Type.IsSubclassOf(typeof(CliApp));
            _optionsAttribute = Type.GetCustomAttribute<CliOptionsAttribute>();

            var allProps = Type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            ConfigurationProperties = allProps.GetConfigProperties().ToArray();
            CommandProperties = allProps.GetCommandProperties().ToArray();
            _implicitBoolProperties = ConfigurationProperties
                .Where(p => p.PropertyType == typeof(bool) && !p.HasAttribute<CliExplicitBoolAttribute>())
                .ToArray();
        }

        /// <summary>
        /// The entrypoint for the current command.
        /// </summary>
        /// <param name="args">The filtered arguments left after parsing.</param>
        public abstract void OnExecute(string[] args);

        /// <summary>
        /// Display a friendly help menu for this commands sub-commands and/or options.
        /// </summary>
        public void PrintHelpMenu() => HelpMenu.Print(Type, CommandProperties, ConfigurationProperties, this);

        internal void Parse(CliCommand parent, AppSettings appSettings, string[] args)
        {
            Parent = parent;
            AppSettings = appSettings;
            _namespaceList = GetNamespaceList(new List<string>()).ToArray();
            _namespace = string.Join(":", _namespaceList);

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
            if (string.IsNullOrEmpty(_optionsAttribute?.Namespace)) { namespaceList.Insert(0, Type.Name); }
            else { namespaceList.Insert(0, _optionsAttribute.Namespace); }
            return Parent.GetNamespaceList(namespaceList);
        }

        private void Start(string[] args)
        {
            string[] filteredArgs = new string[0];
            if (ConfigurationProperties.Length > 0)
            {
                var switchMaps = ConfigurationProperties.GetSwitchMaps(_namespace);
                var implicitSwitchMaps = _implicitBoolProperties.GetSwitchMaps(_namespace);
                filteredArgs = args.Except(implicitSwitchMaps.Keys, StringComparer.OrdinalIgnoreCase).ToArray();
                var configBuilder = new ConfigurationBuilder();
                AppSettings.UserConfiguration?.Invoke(configBuilder);
                var configWithoutCli = configBuilder.Build();
                configBuilder.AddCommandLine(filteredArgs, switchMaps);
                var config = configBuilder.Build();

                var commandName = _optionsAttribute?.Namespace ?? Type.Name;
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
            foreach (var prop in ConfigurationProperties)
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
            return CommandProperties.FirstOrDefault(prop =>
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
            AppSettings.ConfigurationBuilder.AddCommandLine(args, switchMaps);
            var config = AppSettings.ConfigurationBuilder.Build();
            AppSettings.ServiceCollection.AddSingleton(subCommandProp.PropertyType);
            AppSettings.UserServiceRegistration?.Invoke(AppSettings.ServiceCollection, config);
            return AppSettings.ServiceCollection.BuildServiceProvider();
        }
    }
}
