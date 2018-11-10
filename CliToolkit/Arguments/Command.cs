using System;
using System.Linq;
using CliToolkit.Core;
using CliToolkit.Exceptions;
using CliToolkit.Meta;

namespace CliToolkit.Arguments
{
    public abstract class Command : Argument, ICommand
    {
        private bool _parentIsCliApp = false;

        protected Command(string description, string keyword) : base(description, keyword)
        {
        }

        public ICommand Parent { get; private set; }

        public abstract void OnExecute(string[] args);

        /// <summary>
        /// Prints the app header. The footer will also be printed after execution is complete.
        /// </summary>
        public void PrintHeader()
        {
            TextHelpers.PrintHeader(ResolveBaseApplication());
        }

        public void PrintHelpMenu()
        {
            TextHelpers.PrintHelpMenu(this);
        }

        private CliApp ResolveBaseApplication()
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