using System;
using System.Linq;
using System.Reflection;
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
            var helpMenu = (HelpMenu) caller.GetType()
                .GetProperty("HelpMenu", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(caller);

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
                .Concat<Argument>(new HelpMenu[] { helpMenu })
                .OrderBy(arg => arg.Keyword);
            
            caller.PrintHeader();
            
            Console.WriteLine();

            foreach (var command in commands)
            {
                Console.WriteLine($"    {command.Keyword}    {command.Description}");
            }

            if (commands.Count() > 0) { Console.WriteLine(); }

            foreach (var arg in optionalArgs)
            {
                if (arg is Flag)
                {
                    var flag = (Flag) arg;

                    if (string.IsNullOrEmpty(flag.ShortKeyword))
                    {
                        Console.WriteLine($"    {flag.Style.GetPrefix(false)}{flag.Keyword}    {flag.Description}");
                    }
                    else
                    {
                        Console.WriteLine($"    {flag.Style.GetPrefix(false)}{flag.Keyword}  or  {flag.Style.GetPrefix(true)}{flag.ShortKeyword}    {flag.Description}");
                    }
                }
                else if (arg is Property)
                {
                    var property = (Property) arg;

                    if (string.IsNullOrEmpty(property.ShortKeyword))
                    {
                        Console.WriteLine($"    {property.Style.GetPrefix(false)}{property.Keyword}    {property.Description}");
                    }
                    else
                    {
                        Console.WriteLine($"    {property.Style.GetPrefix(false)}{property.Keyword}  or  {property.Style.GetPrefix(true)}{property.ShortKeyword}    {property.Description}");
                    }
                }
                else if (arg is HelpMenu)
                {
                    var help = (HelpMenu) arg;
                    Console.WriteLine($"    {help.Style.GetPrefix(false)}{help.Keyword}  or  {help.Style.GetPrefix(false)}{help.ShortKeyword}    {help.Description}");
                }
            }
            Console.WriteLine();
        }
    }
}