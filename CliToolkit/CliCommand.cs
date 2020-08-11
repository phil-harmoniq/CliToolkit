using CliToolkit.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CliToolkit
{
    public abstract class CliCommand
    {
        private const string _titlePad = "  ";
        private const string _optionPad = "    ";

        private readonly Type _type;
        private readonly bool _isAppRoot;
        private readonly CliOptionsAttribute _optionsAttribute;
        private readonly IList<PropertyInfo> _configurationProperties;
        private readonly IList<PropertyInfo> _commandProperties;
        private readonly IList<PropertyInfo> _implicitBoolProperties;

        private string _namespace;
        private CliCommand _parent;
        private AppSettings _userSettings;
        private IList<string> _implicitKeywords;

        public CliCommand()
        {
            _type = GetType();
            _isAppRoot = _type.IsSubclassOf(typeof(CliApp));
            _optionsAttribute = _type.GetCustomAttribute<CliOptionsAttribute>();

            var allProps = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            _configurationProperties = allProps.GetConfigProperties();
            _commandProperties = allProps.GetCommandProperties();
            _implicitBoolProperties = _configurationProperties
                .Where(p => p.PropertyType == typeof(bool)
                    && p.GetCustomAttribute<CliExplicitBoolAttribute>() == null)
                .ToList();
        }

        public abstract void OnExecute(string[] args);

        public void PrintHelpMenu()
        {
            Console.WriteLine();

            var rootAttr = _type.GetCustomAttribute<CliOptionsAttribute>();

            if (rootAttr != null)
            {
                Console.WriteLine($"{_titlePad}{rootAttr.Description}{Environment.NewLine}");
            }

            if (_commandProperties.Count > 0)
            {
                Console.WriteLine($"{_titlePad}Commands:");

                foreach (var prop in _commandProperties)
                {
                    var kebab = TextHelper.KebabConvert(prop.Name).ToLower();
                    Console.WriteLine($"{_optionPad}{kebab}");
                }

                Console.WriteLine();
            }

            if (_configurationProperties.Count > 0)
            {
                Console.WriteLine($"{_titlePad}Options:");

                foreach (var prop in _configurationProperties)
                {
                    var attr = prop.GetCustomAttribute<CliOptionsAttribute>();

                    var output = $"--{TextHelper.KebabConvert(prop.Name).ToLower()}";
                    if (attr != null && attr.ShortKey.IsValidShortKey())
                    {
                        output = $"{output}, -{attr.ShortKey}";
                    }

                    Console.WriteLine($"{_optionPad}{output}");
                }

                Console.WriteLine();
            }
        }

        internal void Parse(
            CliCommand caller,
            AppSettings userSettings,
            string[] args)
        {
            _parent = caller;
            _userSettings = userSettings;
            var namespaces = GetNamespaceList(new List<string>());
            _namespace = string.Join(":", namespaces);

            if (args.Length > 0)
            {
                var subCommandProp = FindMatchingSubCommand(args[0]);

                if (subCommandProp != null)
                {
                    var subType = subCommandProp.PropertyType;
                    var subProps = subType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    var configProps = subProps.GetConfigProperties();
                    var switchMaps = GetSwitchMaps(configProps);
                    _userSettings.ConfigurationBuilder.AddCommandLine(args, switchMaps);
                    var config = _userSettings.ConfigurationBuilder.Build();
                    _userSettings.ServiceCollection.AddSingleton(subCommandProp.PropertyType);
                    _userSettings.UserServiceRegistration?.Invoke(_userSettings.ServiceCollection, config);
                    var serviceProvider = _userSettings.ServiceCollection.BuildServiceProvider();

                    if (!subCommandProp.CanWrite)
                    {
                        throw new Exception($"Property {subCommandProp.Name} must have a public setter for injection.");
                    }

                    subCommandProp.SetValue(this, serviceProvider.GetRequiredService(subCommandProp.PropertyType));
                    var val = subCommandProp.GetValue(this, null);
                    var subCommand = (CliCommand)val;
                    subCommand.Parse(this, userSettings, args.Skip(1).ToArray());
                }
                else
                {
                    InjectPropertiesAndStart(args);
                }
            }
            else
            {
                InjectPropertiesAndStart(args);
            }
        }

        internal List<string> GetNamespaceList(List<string> namespaceList)
        {
            if (_isAppRoot) { return namespaceList; }
            if (string.IsNullOrEmpty(_optionsAttribute?.Namespace)) { namespaceList.Insert(0, _type.Name); }
            else { namespaceList.Insert(0, _optionsAttribute.Namespace); }
            return _parent.GetNamespaceList(namespaceList);
        }

        private Dictionary<string, string> GetSwitchMaps(IList<PropertyInfo> configProperties)
        {
            var switchMaps = new Dictionary<string, string>();
            foreach (var prop in configProperties)
            {
                switchMaps.Add($"--{prop.Name}", $"{_namespace}:{prop.Name}");
                var kebabName = TextHelper.KebabConvert(prop.Name);
                if (!kebabName.Equals(prop.Name, StringComparison.OrdinalIgnoreCase))
                {
                    switchMaps.Add($"--{kebabName}", $"{_namespace}:{prop.Name}");
                }
                var propOptions = prop.GetCustomAttribute<CliOptionsAttribute>();
                if (propOptions?.ShortKey != null && propOptions.ShortKey.IsValidShortKey())
                {
                    var shortKey = $"-{propOptions.ShortKey}";
                    if (switchMaps.ContainsKey(shortKey))
                    {
                        throw new CliAppBuilderException(
                            $"Cannot assign duplicate short-keys: {shortKey}");
                    }
                    switchMaps.Add(shortKey, $"{_namespace}:{prop.Name}");
                }
            }
            return switchMaps;
        }

        private void InjectPropertiesAndStart(string[] args)
        {
            string[] filteredArgs = new string[0];
            if (_configurationProperties.Count > 0)
            {
                var switchMaps = GetSwitchMaps(_configurationProperties);
                var implicitSwitchMaps = GetSwitchMaps(_implicitBoolProperties);
                filteredArgs = args.Except(implicitSwitchMaps.Keys, StringComparer.OrdinalIgnoreCase).ToArray();
                var configBuilder = new ConfigurationBuilder();
                _userSettings.UserConfiguration?.Invoke(configBuilder);
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

        private PropertyInfo FindMatchingSubCommand(string arg)
        {
            return _commandProperties.FirstOrDefault(p =>
            {
                var aliases = new List<string>
                {
                    p.Name,
                    TextHelper.KebabConvert(p.Name)
                };
                return aliases.Contains(arg, StringComparer.OrdinalIgnoreCase);
            });
        }
    }
}
