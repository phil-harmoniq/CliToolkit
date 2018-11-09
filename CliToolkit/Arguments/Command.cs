using System;
using System.Linq;
using CliToolkit.Core;
using CliToolkit.Meta;

namespace CliToolkit.Arguments
{
    public abstract class Command : Argument, ICommand
    {
        private bool _parentIsCliApp = false;

        public ICommand Parent { get; }

        protected Command(string description, string keyword, CliApp parent) : base(description, keyword)
        {
            Parent = parent;
            _parentIsCliApp = true;
        }

        protected Command(string description, string keyword, ICommand parent) : base(description, keyword)
        {
            Parent = parent;
        }

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
    }
}