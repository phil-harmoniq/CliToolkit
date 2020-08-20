using System;
using System.Collections.Generic;
using System.Reflection;

namespace CliToolkit.Internal
{
    internal static class HelpMenu
    {
        private const string _titlePad = "  ";
        private const string _optionPad = "    ";

        internal static void Print(
            Type commandType,
            IList<PropertyInfo> commandProps,
            IList<PropertyInfo> configProps)
        {
            Console.WriteLine();

            var rootAttr = commandType.GetCustomAttribute<CliOptionsAttribute>();

            if (rootAttr != null)
            {
                Console.WriteLine($"{_titlePad}{rootAttr.Description}{Environment.NewLine}");
            }

            if (commandProps.Count > 0)
            {
                Console.WriteLine($"{_titlePad}Commands:");

                foreach (var prop in commandProps)
                {
                    var kebab = TextHelper.KebabConvert(prop.Name).ToLower();
                    Console.WriteLine($"{_optionPad}{kebab}");
                }

                Console.WriteLine();
            }

            if (configProps.Count > 0)
            {
                Console.WriteLine($"{_titlePad}Options:");

                foreach (var prop in configProps)
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
    }
}
