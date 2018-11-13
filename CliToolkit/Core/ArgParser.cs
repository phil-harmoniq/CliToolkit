using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CliToolkit.Arguments;
using CliToolkit.Exceptions;

namespace CliToolkit.Core
{
    internal static class ArgParser
    {
        internal static void ParseArgs(ICommand caller, string[] args)
        {
            var classFields = caller.GetType().GetFields();
            var classProperties = caller.GetType().GetProperties();
            var helpMenuList = new List<HelpMenu>();
            
            if (!(caller is HelpMenu))
            {
                var helpMenu = (HelpMenu) caller.GetType()
                    .GetProperty("HelpMenu", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
                    .GetValue(caller);
                helpMenuList.Add(helpMenu);
                helpMenu.SetParent(caller);
            }

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
                .Select(i => (Command)i.GetValue(caller)));
            
            foreach (var command in commands) { command.SetParent(caller); }

            CheckForDuplicateKeywords(flags, properties, commands, helpMenuList);

            var allDeclaredArgs = flags
                .Concat<Argument>(properties)
                .Concat<Argument>(commands)
                .Concat<Argument>(helpMenuList);
            
            var onExecuteArgs = new List<string>();

            ICommand commandTarget = null;
            bool commandFound = false;
            int indexStoppedAt = 0;

            for (var i = 0; i < args.Length; i++)
            {
                var argsToCapture = 0;
                var currentArg = args[i];
                var nextArg = i + 1 < args.Length ? args[1] : null;

                foreach (var arg in allDeclaredArgs)
                {
                    var matchingKeywords = arg.IsMatchingKeyword(new string[] { currentArg, nextArg });
                    if (matchingKeywords > 0)
                    {
                        if (arg is Command)
                        {
                            commandFound = true;
                            commandTarget = (Command) arg;
                            indexStoppedAt = i;
                        }
                        else if (arg is HelpMenu)
                        {
                            commandFound = true;
                            commandTarget = (HelpMenu) arg;
                            indexStoppedAt = i;
                        }
                        argsToCapture = matchingKeywords;
                        break;
                    }
                }
                if (argsToCapture == 0)
                {
                    // If arg doesn't match, add to the application arguments
                    onExecuteArgs.Add(currentArg);
                }
                else if (argsToCapture == 2)
                {
                    // If a property arg, add extra incriment to skip next arg
                    i++;
                }
                // Do nothing if argsToCapture == 1
                if (commandFound) { break; }
            }

            if (commandFound)
            {
                ArgParser.ParseArgs(commandTarget, args.Skip(indexStoppedAt + 1).ToArray());
            }
            else
            {
                caller.OnExecute(onExecuteArgs.ToArray());
            }
        }

        private static void CheckForDuplicateKeywords(IEnumerable<Flag> flags, IEnumerable<Property> properties, IEnumerable<Command> commands, IEnumerable<HelpMenu> helpMenus)
        {
            var allUniqueKeywords = new Dictionary<string, List<Argument>>();
            
            foreach (var flag in flags)
            {
                if (allUniqueKeywords.Keys.Contains(flag.Keyword))
                {
                    allUniqueKeywords[flag.Keyword].Add(flag);
                }
                else
                {
                    allUniqueKeywords.Add(flag.Keyword ,new List<Argument> { flag });
                }
                if (!string.IsNullOrEmpty(flag.ShortKeyword))
                {
                    if (allUniqueKeywords.Keys.Contains(flag.ShortKeyword))
                    {
                        allUniqueKeywords[flag.ShortKeyword].Add(flag);
                    }
                    else
                    {
                        allUniqueKeywords.Add(flag.ShortKeyword ,new List<Argument> { flag });
                    }
                }
            }
            
            foreach (var property in properties)
            {
                if (allUniqueKeywords.Keys.Contains(property.Keyword))
                {
                    allUniqueKeywords[property.Keyword].Add(property);
                }
                else
                {
                    allUniqueKeywords.Add(property.Keyword ,new List<Argument> { property });
                }
                if (!string.IsNullOrEmpty(property.ShortKeyword))
                {
                    if (allUniqueKeywords.Keys.Contains(property.ShortKeyword))
                    {
                        allUniqueKeywords[property.ShortKeyword].Add(property);
                    }
                    else
                    {
                        allUniqueKeywords.Add(property.ShortKeyword ,new List<Argument> { property });
                    }
                }
            }
            
            foreach (var command in commands)
            {
                if (allUniqueKeywords.Keys.Contains(command.Keyword))
                {
                    allUniqueKeywords[command.Keyword].Add(command);
                }
                else
                {
                    allUniqueKeywords[command.Keyword] = new List<Argument> { command };
                }
            }
            
            foreach (var helpMenu in helpMenus)
            {
                if (allUniqueKeywords.Keys.Contains(helpMenu.Keyword))
                {
                    allUniqueKeywords[helpMenu.Keyword].Add(helpMenu);
                }
                else
                {
                    allUniqueKeywords.Add(helpMenu.Keyword ,new List<Argument> { helpMenu });
                }
                if (!string.IsNullOrEmpty(helpMenu.ShortKeyword))
                {
                    if (allUniqueKeywords.Keys.Contains(helpMenu.ShortKeyword))
                    {
                        allUniqueKeywords[helpMenu.ShortKeyword].Add(helpMenu);
                    }
                    else
                    {
                        allUniqueKeywords.Add(helpMenu.ShortKeyword ,new List<Argument> { helpMenu });
                    }
                }
            }

            foreach (var keyword in allUniqueKeywords)
            {
                if (keyword.Value.Count > 1)
                {
                    throw new AppConfigurationException($"Duplicate keyword {keyword.Key} found. All keywords must be unique.");
                }
            }
        }
    }
}