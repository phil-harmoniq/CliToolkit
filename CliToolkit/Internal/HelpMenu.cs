using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CliToolkit.Internal
{
    internal static class HelpMenu
    {
        private const string _titlePad = "  ";
        private const string _optionPad = "    ";
        private const string _margin = "   ";

        internal static void Print(
            Type commandType,
            ICollection<PropertyInfo> commandProps,
            ICollection<PropertyInfo> configProps,
            CliCommand caller)
        {
            Console.WriteLine();

            var rootAttr = commandType.GetCustomAttribute<CliOptionsAttribute>();

            if (rootAttr != null && !string.IsNullOrEmpty(rootAttr.Description))
            {
                Console.WriteLine($"{_titlePad}{rootAttr.Description}{Environment.NewLine}");
            }

            var commandTree = $"{_optionPad}{string.Join(" ", caller.GetCommandTree())}";

            if (commandProps.Count > 0) { commandTree += " [command]"; }
            if (configProps.Count > 0) { commandTree += " [options]"; }

            Console.WriteLine(commandTree);
            Console.WriteLine();

            var commands = new Dictionary<string, string>();
            var options = new Dictionary<string, string>();

            foreach (var prop in commandProps)
            {
                var attr = prop.GetCustomAttribute<CliOptionsAttribute>();
                var kebab = prop.Name.KebabConvert();
                commands.Add(kebab, attr?.Description);
            }

            foreach (var prop in configProps)
            {
                var attr = prop.GetCustomAttribute<CliOptionsAttribute>();
                var output = $"--{prop.Name.KebabConvert()}";

                if (attr != null && attr.ShortKey.IsValidShortKey())
                {
                    output += $", -{attr.ShortKey}";
                }

                if (prop.PropertyType == typeof(string))
                {
                    output += " <string>";
                }
                else if (prop.PropertyType == typeof(int))
                {
                    output += " <int>";
                }
                else if (prop.PropertyType == typeof(bool)
                    && prop.HasAttribute<CliExplicitBoolAttribute>())
                {
                    output += " <bool>";
                }

                options.Add(output, attr?.Description);
            }

            var longest = new Dictionary<string, string>()
                .Concat(commands)
                .Concat(options)
                .OrderByDescending(kv => kv.Key.Length)
                .First()
                .Key.Length + _optionPad.Length;
            var descriptionPad = longest + _optionPad.Length + _margin.Length;

            if (commands.Count > 0)
            {
                Console.WriteLine($"{_titlePad}Commands:");

                foreach (var command in commands)
                {
                    var prefix = $"{_optionPad}{command.Key}";
                    var marginCount = longest + _margin.Length - prefix.Length;
                    var margin = new string(' ', marginCount);
                    Console.WriteLine($"{_optionPad}{command.Key}{margin}{command.Value}");
                }

                Console.WriteLine();
            }

            if (options.Count > 0)
            {
                Console.WriteLine($"{_titlePad}Options:");

                foreach (var option in options)
                {
                    var prefix = $"{_optionPad}{option.Key}";
                    var marginCount = longest + _margin.Length - prefix.Length;
                    var margin = new string(' ', marginCount);
                    Console.WriteLine($"{_optionPad}{option.Key}{margin}{option.Value}");
                }

                Console.WriteLine();
            }
        }
    }
}
