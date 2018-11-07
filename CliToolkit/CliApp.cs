using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CliToolkit.Arguments;
using CliToolkit.Exceptions;
using CliToolkit.Meta;

namespace CliToolkit
{
    /// <summary>
    /// Inherit this class to build a new CLI application.
    /// </summary>
    public abstract class CliApp
    {
        private bool _headerWasShown = false;
        private int _width = 64;

        internal string _headerText;
        internal string _footerText;
        
        /// <summary>
        /// This application's name.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// This application's assembly version.
        /// </summary>
        public string Version { get; internal set; }

        /// <summary>
        /// Contains meta-data about this application and its environment.
        /// </summary>
        public AppInfo AppInfo { get; private set; }

        /// <summary>
        /// The exit code after running <see cref="OnExecute" />
        /// </summary>
        /// <value>Any non-zero value indicates an error during execution.</value>
        public int ExitCode { get; internal set; }

        /// <summary>
        /// Constructs a new <see cref="CliApp" /> instance. This should not be called directly.
        /// </summary>
        protected CliApp()
        {
            AppInfo = new AppInfo();
            Name = AppInfo.Assembly.GetName().Name;
            Version = AppInfo.FileVersionInfo.ProductVersion;

            _headerText = CompileHeaderText();
            _footerText = CompileFooterText();
        }

        /// <summary>
        /// <param name="args">The arguments passed to this application.</param>
        /// Defines the default behavior when this application is executed.
        /// </summary>
        public abstract void OnExecute(string[] args);

        // /// <summary>
        // /// Prints the auto-generated help menu. Override this method for a custom menu.
        // /// </summary>
        // public virtual void PrintHelpMenu()
        // {
        // }

        /// <summary>
        /// Begins the application's execution cycle.
        /// </summary>
        /// <param name="args">The arguments passed to this application.</param>
        /// <example>Call using the application's main entrypoint:
        /// <code>
        /// static void Main(string[] args)
        /// {
        ///     var app = new AppBuilder&lt;Program&gt;().Build();
        ///     app.Start(args);
        /// }
        /// </code>
        /// </example>
        /// <returns></returns>
        public CliApp Start(string[] args)
        {
            try
            {
                ParseArgs(args);
            }
            catch (AppRuntimeException exception)
            {
                Console.WriteLine($"Error:{AppInfo.NewLine}{exception.Message}");
            }
            finally
            {
                if (_headerWasShown) { PrintFooter(); }
            }
            return this;
        }

        /// <summary>
        /// Prints the app header. The footer will also be printed after execution is complete.
        /// </summary>
        public void PrintHeader()
        {
            _headerWasShown = true;
            Console.WriteLine(_headerText);
        }

        private void PrintFooter()
        {
            if (!string.IsNullOrEmpty(_footerText))
            {
                Console.WriteLine(_footerText);
            }
        }

        private void ParseArgs(string[] args)
        {
            var classFields = this.GetType().GetFields();
            var classProperties = this.GetType().GetProperties();

            var flags = classFields
                .Where(i => i.FieldType == typeof(Flag))
                .Select(i => (Flag)i.GetValue(this))
                .Concat(classProperties
                .Where(i => i.PropertyType == typeof(Flag))
                .Select(i => (Flag)i.GetValue(this)));
            var properties = classFields
                .Where(i => i.FieldType == typeof(Property))
                .Select(i => (Property)i.GetValue(this))
                .Concat(classProperties
                .Where(i => i.PropertyType == typeof(Property))
                .Select(i => (Property)i.GetValue(this)));

            CheckForDuplicateKeywords(flags, properties);

            var allDeclaredArgs = flags.Concat<Argument>(properties);
            var onExecuteArgs = new List<string>();

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
            }

            OnExecute(onExecuteArgs.ToArray());
        }

        private static void CheckForDuplicateKeywords(IEnumerable<Flag> flags, IEnumerable<Property> properties)
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

            AddToDuplicatesCollection(duplicates, uniqueFlagShortKeywords);
            AddToDuplicatesCollection(duplicates, uniqueFlagLongKeywords);
            AddToDuplicatesCollection(duplicates, uniquePropertyShortKeywords);
            AddToDuplicatesCollection(duplicates, uniquePropertyLongKeywords);

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

        private string CompileHeaderText()
        {
            var title = $" {Name} {Version} ";
            if (title.Length >= _width) { return $"-{title}-"; }

            var titleLengthIsOdd = title.Length % 2 == 1;
            var barLength = (_width - title.Length) / 2;

            var leftBar = new string('-', barLength);
            var rightBar = new string('-', titleLengthIsOdd ? barLength + 1 : barLength);

            return $"{AppInfo.NewLine}{leftBar}{title}{rightBar}";
        }

        private string CompileFooterText()
        {
            return $"{new string('-', _width)}{AppInfo.NewLine}";
        }
    }
}