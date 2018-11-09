using CliToolkit.Meta;

namespace CliToolkit.Core
{
    public interface ICommand
    {
        void OnExecute(string[] args);
        void PrintHeader();
        void PrintHelpMenu();
    }
}