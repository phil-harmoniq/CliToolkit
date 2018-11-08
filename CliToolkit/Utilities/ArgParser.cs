using System;
using System.Collections.Generic;
using System.Linq;
using CliToolkit.Arguments;
using CliToolkit.Exceptions;

namespace CliToolkit.Utilities
{
    internal static class ArgParser
    {
        internal static void ParseArgs(ICommand command, string[] args)
        {
            var classFields = command.GetType().GetFields();
            var classProperties = command.GetType().GetProperties();

            var flags = classFields
                .Where(i => i.FieldType == typeof(Flag))
                .Select(i => (Flag)i.GetValue(command))
                .Concat(classProperties
                .Where(i => i.PropertyType == typeof(Flag))
                .Select(i => (Flag)i.GetValue(command)));
            var properties = classFields
                .Where(i => i.FieldType == typeof(Property))
                .Select(i => (Property)i.GetValue(command))
                .Concat(classProperties
                .Where(i => i.PropertyType == typeof(Property))
                .Select(i => (Property)i.GetValue(command)));
            var commands = classFields
                .Where(i => i.FieldType == typeof(Command))
                .Select(i => (Command)i.GetValue(command))
                .Concat(classProperties
                .Where(i => i.PropertyType == typeof(Command))
                .Select(i => (Command)i.GetValue(command)));

            CheckForDuplicateKeywords(flags, properties, commands);

            var allDeclaredArgs = flags.Concat<Argument>(properties).Concat<Argument>(commands);
            var onExecuteArgs = new List<string>();

            Command commandTarget = null;
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
                        if (arg.GetType() == typeof(Command))
                        {
                            commandFound = true;
                            commandTarget = (Command) arg;
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
                command.OnExecute(onExecuteArgs.ToArray());
            }
        }

        private static void CheckForDuplicateKeywords(IEnumerable<Flag> flags, IEnumerable<Property> properties, IEnumerable<Command> commands)
        {
            var duplicates = new Dictionary<string, List<Argument>>();

            var uniqueFlagShortKeywords = flags
                .GroupBy(flag => flag.ShortKeyword)
                .Where(group => group.Key != "");

            var uniqueFlagLongKeywords = flags
                .GroupBy(flag => flag.Keyword)
                .Where(group => group.Key != "");

            var uniquePropertyShortKeywords = properties
                .GroupBy(property => property.ShortKeyword)
                .Where(group => group.Key != "");

            var uniquePropertyLongKeywords = properties
                .GroupBy(property => property.Keyword)
                .Where(group => group.Key != "");

            var uniqueCommandLongKeywords = commands
                .GroupBy(command => command.Keyword)
                .Where(group => group.Key != "");

            AddToDuplicatesCollection(duplicates, uniqueFlagShortKeywords);
            AddToDuplicatesCollection(duplicates, uniqueFlagLongKeywords);
            AddToDuplicatesCollection(duplicates, uniquePropertyShortKeywords);
            AddToDuplicatesCollection(duplicates, uniquePropertyLongKeywords);
            AddToDuplicatesCollection(duplicates, uniqueCommandLongKeywords);

            if (duplicates.Any())
            {
                var errorMessage = "";

                foreach (var duplicate in duplicates)
                {
                    foreach (var arg in duplicate.Value)
                    {
                        errorMessage += $"Duplicate argument keyword detected for {arg.GetType().Name}: {duplicate.Key}{Environment.NewLine}";
                    }
                }

                throw new AppConfigurationException(errorMessage);
            }
        }

        private static void AddToDuplicatesCollection(Dictionary<string, List<Argument>> duplicates, IEnumerable<IGrouping<string, Argument>> arguments)
        {
            foreach (var group in arguments)
            {
                if (group != null && group.Count() > 1)
                {
                    foreach (var arg in group)
                    {
                        if (!duplicates.Keys.Contains(group.Key))
                        {
                            duplicates.Add(group.Key, new List<Argument>());
                        }
                        duplicates[group.Key].Add(arg);
                    }
                }
            }
        }
    }
}