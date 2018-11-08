using CliToolkit.Utilities;

namespace CliToolkit.Arguments
{
    public abstract class Command : Argument, ICommand
    {
        internal Command(string description, string keyword, char? shortKeyword = null) : base(description, keyword, shortKeyword)
        {
        }

        public abstract void OnExecute(string[] args);
    }
}