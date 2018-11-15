using System;
using System.Linq;
using CliToolkit.Arguments;
using CliToolkit.Core;
using CliToolkit.Exceptions;

namespace CliToolkit
{
    /// <summary>
    /// Generic type for a CLI command.
    /// </summary>
    public abstract class Command : Argument, ICommand
    {
        private bool _parentIsCliApp = false;

        internal HelpMenu HelpMenu { get; }

        /// <summary>
        /// Contains meta-data about this application and its environment.
        /// </summary>
        public AppInfo AppInfo { get; }

        /// <summary>
        /// A reference to the upper-level CliApp or Command that contains this Command.
        /// </summary>
        public ICommand Parent { get; private set; }

        /// <summary>
        /// Defines the behaviour when this command is triggered.
        /// </summary>
        /// <param name="args">The arguments passed to this command.</param>
        public abstract void OnExecute(string[] args);

        /// <summary>
        /// Creates a new strongly-typed CLI command.
        /// </summary>
        /// <param name="description">The text description that will be displayed in the help menu</param>
        /// <param name="keyword">The keyword that will trigger this command's execution.</param>
        /// <returns></returns>
        protected Command(string description, string keyword) : base(description, keyword)
        {
            AppInfo = new AppInfo();
            HelpMenu = new HelpMenu("Displays the available options for this command.", "help", "h");
        }

        /// <summary>
        /// Prints the app header. The footer will also be printed after execution is complete.
        /// </summary>
        public void PrintHeader()
        {
            TextHelpers.PrintHeader(ResolveBaseApplication());
        }

        /// <summary>
        /// Prints a help menu that list all available commands and/or arguments contained in this command.
        /// </summary>
        public void PrintHelpMenu()
        {
            TextHelpers.PrintHelpMenu(this);
        }

        internal CliApp ResolveBaseApplication()
        {
            if (_parentIsCliApp)
            {
                return (CliApp) Parent;
            }
            var baseCommand = (Command) Parent;
            return baseCommand.ResolveBaseApplication();
        }

        internal override int IsMatchingKeyword(string[] args)
        {
            if (args[0].Equals(Keyword))
            {
                IsActive = true;
                return 1;
            }
            return 0;
        }
        
        internal void SetParent(ICommand parent)
        {
            if (parent == null)
            { 
                var message = $"{Description}:{Environment.NewLine}Parent object was null.";
                throw new AppConfigurationException(message);
            }

            if (parent is CliApp) { _parentIsCliApp = true; }
            
            Parent = parent;
        }
    }
}