using System;
using System.Linq;
using CliToolkit.Styles;
using CliToolkit.Core;
using CliToolkit.Exceptions;
using CliToolkit.Arguments;

namespace CliToolkit.Core
{
    internal class HelpMenu : Argument, ICommand
    {
        private bool _parentIsCliApp = false;

        internal string ShortKeyword { get; }
        internal ICommand Parent { get; private set; }

        public AppInfo AppInfo => throw new NotImplementedException();

        internal HelpMenu(string description, string keyword, string shortKeyword) : base(description, keyword)
        {
            ShortKeyword = shortKeyword;
            Style = FlagStyle.DoubleDash;
        }

        public void OnExecute(string[] args)
        {
            PrintHelpMenu();
        }

        public void PrintHeader()
        {
            TextHelpers.PrintHeader(ResolveBaseApplication());
        }

        public void PrintHelpMenu()
        {
            TextHelpers.PrintHelpMenu(Parent);
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
            if (args[0].Equals($"{Style.GetPrefix(false)}{Keyword}") || args[0].Equals($"{Style.GetPrefix(true)}{ShortKeyword}"))
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