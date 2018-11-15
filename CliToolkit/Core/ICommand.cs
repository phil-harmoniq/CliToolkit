using CliToolkit.Arguments;
using CliToolkit.Core;

namespace CliToolkit.Core
{
    /// <summary>
    /// Contract for a executing a CLI command or appliation.
    /// </summary>
    public interface ICommand
    {
        AppInfo AppInfo { get; }
        
        /// <summary>
        /// Defines the behaviour when this command is triggered.
        /// </summary>
        /// <param name="args">The arguments passed to this command.</param>
        void OnExecute(string[] args);

        /// <summary>
        /// Prints the app header. The footer will also be printed after execution is complete.
        /// </summary>
        void PrintHeader();

        /// <summary>
        /// Prints a help menu that list all available commands and/or arguments contained in this command.
        /// </summary>
        void PrintHelpMenu();
    }
}