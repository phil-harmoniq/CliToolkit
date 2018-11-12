using System;
using System.Linq;
using CliToolkit.Arguments;
using CliToolkit.Meta;

namespace CliToolkit.Core
{
    internal static class TextHelpers
    {
        internal static bool HeaderWasShown { get; private set; } = false;

        internal static void PrintHeader(CliApp baseApp)
        {
            var leftBar = "";
            var rightBar = "";
            var title = $" {baseApp.AppInfo.Name} {baseApp.AppInfo.Version} ";

            if (title.Length >= baseApp.AppInfo.Width)
            {
                leftBar = "-";
                rightBar = "-";
            }
            else
            {
                var titleLengthIsOdd = title.Length % 2 == 1;
                var barLength = (baseApp.AppInfo.Width - title.Length) / 2;

                leftBar = new string('-', barLength);
                rightBar = new string('-', titleLengthIsOdd ? barLength + 1 : barLength);
            }

            Console.WriteLine();
            Console.Write(leftBar);
            Console.ForegroundColor = baseApp.AppInfo.TitleColor;
            Console.Write(title);
            Console.ResetColor();
            Console.WriteLine(rightBar);
            HeaderWasShown = true;
        }

        internal static void PrintFooter(CliApp baseApp)
        {
            Console.WriteLine(new string('-', baseApp.AppInfo.Width));
            Console.WriteLine();
        }

        internal static void PrintHelpMenu(ICommand caller)
        {
            var classFields = caller.GetType().GetFields();
            var classProperties = caller.GetType().GetProperties();

            var flags = classFields
                .Where(i => i.FieldType == typeof(Flag))
                .Select(i => (Flag)i.GetValue(caller))
                .Concat(classProperties
                .Where(i => i.PropertyType == typeof(Flag))
                .Select(i => (Flag)i.GetValue(caller)));
            var properties = classFields
                .Where(i => i.FieldType == typeof(Property))
                .Select(i => (Property)i.GetValue(caller))
                .Concat(classProperties
                .Where(i => i.PropertyType == typeof(Property))
                .Select(i => (Property)i.GetValue(caller)));
            var commands = classFields
                .Where(i => i.FieldType == typeof(Command))
                .Select(i => (Command)i.GetValue(caller))
                .Concat(classProperties
                .Where(i => i.PropertyType == typeof(Command))
                .Select(i => (Command)i.GetValue(caller)))
                .OrderBy(c => c.Keyword);
            
            var optionalArgs = flags.Concat<Argument>(properties)
                .OrderBy(arg => arg.Keyword);
            
            caller.PrintHeader();
            Console.WriteLine();

            foreach (var command in commands)
            {
                Console.WriteLine($"    {command.Keyword}    {command.Description}");
            }

            foreach (var arg in optionalArgs)
            {
                if (arg is Flag)
                {
                    var flag = (Flag) arg;

                    if (string.IsNullOrEmpty(flag.ShortKeyword))
                    {
                        Console.WriteLine($"    {flag.Keyword}  or  {flag.ShortKeyword}    {flag.Description}");
                    }
                    else
                    {
                        Console.WriteLine($"    {flag.Keyword}    {flag.Description}");
                    }
                }
                else if (arg is Property)
                {
                    var property = (Property) arg;

                    if (string.IsNullOrEmpty(property.ShortKeyword))
                    {
                        Console.WriteLine($"    {property.Keyword}  or  {property.ShortKeyword}    {property.Description}");
                    }
                    else
                    {
                        Console.WriteLine($"    {property.Keyword}    {property.Description}");
                    }
                }
            }
            Console.WriteLine();
        }
    }
}