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
        private readonly Type _type;
        private readonly bool _isAppRoot;
        private readonly CliOptionsAttribute _optionsAttribute;
        private readonly IList<PropertyInfo> _configurationProperties;
        private readonly IList<PropertyInfo> _commandProperties;

        private string _namespace;
        private CliCommand _parent;
        private AppSettings _userSettings;

        public CliCommand()
        {
            _type = GetType();
            _isAppRoot = _type.IsSubclassOf(typeof(CliApp));
            _optionsAttribute = _type.GetCustomAttribute<CliOptionsAttribute>();

            var allProps = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            _configurationProperties = allProps.Where(p =>
                p.PropertyType == typeof(string)
                || p.PropertyType == typeof(int)
                || p.PropertyType == typeof(bool))
                .ToList();

            _commandProperties = allProps.Where(p =>
                p.PropertyType.IsSubclassOf(typeof(CliCommand)))
                .ToList();
        }

        protected abstract void OnExecute(string[] args);

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
                    var switchMaps = GetSwitchMaps();
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

        private Dictionary<string, string> GetSwitchMaps()
        {
            var switchMaps = new Dictionary<string, string>();
            foreach (var prop in _configurationProperties)
            {
                switchMaps.Add($"--{prop.Name}", $"{_namespace}:{prop.Name}");
                var kebabName = TextHelper.KebabConvert(prop.Name);
                if (!kebabName.Equals(prop.Name, StringComparison.OrdinalIgnoreCase))
                {
                    switchMaps.Add($"--{kebabName}", $"{_namespace}:{prop.Name}");
                }
                var propOptions = prop.GetCustomAttribute<CliOptionsAttribute>();
                if (propOptions != null && propOptions.ShortKey != default(char))
                {
                    switchMaps.Add($"-{propOptions.ShortKey}", $"{_namespace}:{prop.Name}");
                }

            }
            return switchMaps;
        }

        private void InjectPropertiesAndStart(string[] args)
        {
            var switchMaps = GetSwitchMaps();
            if (_configurationProperties.Count > 0)
            {
                var configBuilder = new ConfigurationBuilder();
                _userSettings.UserConfiguration?.Invoke(configBuilder);
                configBuilder.AddCommandLine(args, switchMaps);
                var config = configBuilder.Build();

                var commandName = _optionsAttribute?.Namespace ?? _type.Name;
                IConfiguration configSection;
                if (_isAppRoot) { configSection = config; }
                else { configSection = config.GetSection(commandName); }

                foreach (var prop in _configurationProperties)
                {
                    var value = configSection[prop.Name];
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

        private PropertyInfo FindMatchingSubCommand(string arg)
        {
            return _commandProperties.FirstOrDefault(p =>
            {
                var aliases = new List<string>
                {
                    p.Name,
                    TextHelper.KebabConvert(p.Name)
                };
                return aliases.Contains(arg, new IgnoreCaseComparer());
            });
        }
    }
}
