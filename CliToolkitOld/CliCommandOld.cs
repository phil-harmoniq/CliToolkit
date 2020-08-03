﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CliToolkit
{
    public abstract class CliCommandOld
    {
        private const int _menuPadLength = 4;
        private readonly string _menuPad = new string(' ', _menuPadLength);

        private IServiceProvider _serviceProvider;
        private IList<PropertyInfo> _configurationProperties;
        private IList<PropertyInfo> _commandProperties;
        private CliNamespaceAttribute _namespaceAttribute;
        private CliCommandOld _parent;
        private CliAppOld _applicationRoot;
        private Type _type;

        internal string CommandName { get; private set; }
        internal string KebabAlias { get; private set; }
        internal bool IsApplicationRoot { get; private set; }

        protected abstract void OnExecute(string[] args);

        public void PrintHelpMenu()
        {
            Console.WriteLine();

            var rootAttr = _type.GetCustomAttribute<CliDescriptionAttribute>();

            if (rootAttr != null)
            {
                Console.WriteLine("  " + rootAttr.Description + Environment.NewLine);
            }

            if (_commandProperties.Count > 0)
            {
                Console.WriteLine("  Commands:");

                foreach (var prop in _commandProperties)
                {
                    var attr = prop.GetCustomAttribute<CliDescriptionAttribute>();

                    if (attr != null)
                    {
                        Console.WriteLine($"{_menuPad}{TextHelper.KebabConvert(prop.Name).ToLower()}    {attr.Description}");
                    }
                    else
                    {
                        Console.WriteLine($"{_menuPad}{TextHelper.KebabConvert(prop.Name).ToLower()}");
                    }
                }

                Console.WriteLine();
            }

            if (_configurationProperties.Count > 0)
            {
                Console.WriteLine("Options:");

                foreach (var prop in _configurationProperties)
                {
                    var attr = prop.GetCustomAttribute<CliDescriptionAttribute>();

                    if (attr != null)
                    {
                        Console.WriteLine($"{_menuPad}{TextHelper.KebabConvert(prop.Name).ToLower()}    {attr.Description}");
                    }
                    else
                    {
                        Console.WriteLine($"{_menuPad}{TextHelper.KebabConvert(prop.Name).ToLower()}");
                    }
                }

                Console.WriteLine();
            }
        }

        internal void Parse(CliCommandOld caller, string[] args)
        {
            _parent = caller;
            Reflect();

            if (args.Length > 0)
            {
                var subCommandProp = FindMatchingSubCommand(args[0]);

                if (subCommandProp != null)
                {
                    var configBuilder = new ConfigurationBuilder();
                    _applicationRoot.AppInfo.UserConfigBuilder?.Invoke(configBuilder);
                    configBuilder.AddCommandLine(args, GetSwitchMaps());
                    var config = configBuilder.Build();

                    var services = new ServiceCollection();
                    services.AddOptions();
                    services.AddSingleton(subCommandProp.PropertyType);

                    _applicationRoot.AppInfo.UserServiceRegistration?.Invoke(services, config);
                    _serviceProvider = services.BuildServiceProvider();

                    if (!subCommandProp.CanWrite)
                    {
                        throw new Exception($"Property {subCommandProp} must have a public setter for injection.");
                    }

                    subCommandProp.SetValue(this, _serviceProvider.GetRequiredService(subCommandProp.PropertyType));
                    var val = subCommandProp.GetValue(this, null);
                    var subCommand = (CliCommandOld)val;
                    subCommand.Parse(this, args.Skip(1).ToArray());
                }
                else
                {
                    InjectProperties(args);
                }
            }
            else
            {
                InjectProperties(args);
            }
        }

        internal string GetNamespace()
        {
            if (IsApplicationRoot && _namespaceAttribute != null)
            {
                return CommandName;
            }
            if (IsApplicationRoot)
            {
                return "";
            }

            return string.Join(":", new[] { _parent.GetNamespace(), CommandName }
                .Where(s => !string.IsNullOrEmpty(s)));
        }

        internal CliAppOld GetApplicationRoot()
        {
            if (IsApplicationRoot)
            {
                var obj = this;
                return (CliAppOld)obj;
            }
            return _parent.GetApplicationRoot();
        }

        private void Reflect()
        {
            if (GetType().IsSubclassOf(typeof(CliAppOld)))
            {
                IsApplicationRoot = true;
            }

            _type = GetType();
            _applicationRoot = GetApplicationRoot();

            var restrictedProps = IsApplicationRoot
                ? typeof(CliAppOld).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name)
                : new string[0];

            var currentProps = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !restrictedProps.Contains(p.Name));

            _configurationProperties = currentProps.Where(p =>
                p.PropertyType == typeof(string)
                || p.PropertyType == typeof(int)
                || p.PropertyType == typeof(bool))
                .ToList();

            _commandProperties = currentProps.Where(p =>
                p.PropertyType.IsSubclassOf(typeof(CliCommandOld)))
                .ToList();

            _namespaceAttribute = _type.GetCustomAttribute<CliNamespaceAttribute>();

            if (_namespaceAttribute == null)
            {
                CommandName = _type.Name;
                KebabAlias = TextHelper.KebabConvert(_type.Name);
            }
            else
            {
                CommandName = _namespaceAttribute.Namespace;
                KebabAlias = TextHelper.KebabConvert(_namespaceAttribute.Namespace);
            }
        }

        private void InjectProperties(string[] args)
        {
            var name = GetNamespace();
            if (_configurationProperties.Count > 0)
            {
                var configBuilder = new ConfigurationBuilder();
                _applicationRoot.AppInfo.UserConfigBuilder?.Invoke(configBuilder);
                configBuilder.AddCommandLine(args, GetSwitchMaps());
                var config = configBuilder.Build();

                IConfiguration section;

                if (_namespaceAttribute == null)
                {
                    if (IsApplicationRoot)
                    {
                        section = config;
                    }
                    else
                    {
                        section = config.GetSection(CommandName);
                    }
                }
                else if (string.IsNullOrEmpty(_namespaceAttribute.Namespace))
                {
                    section = config;
                }
                else
                {
                    section = config.GetSection(_namespaceAttribute.Namespace);
                }

                foreach (var prop in _configurationProperties)
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
            var switchMaps = new Dictionary<string, string>();
            foreach (var prop in _configurationProperties)
            {
                switchMaps.Add($"--{prop.Name}", $"{GetNamespace()}:{prop.Name}");
                var kebabName = TextHelper.KebabConvert(prop.Name);
                if (!kebabName.Equals(prop.Name, StringComparison.OrdinalIgnoreCase))
                {
                    switchMaps.Add($"--{kebabName}", $"{GetNamespace()}:{prop.Name}");
                }
            }
            return switchMaps;
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
                return aliases.Contains(arg, null);
            });
        }
    }
}