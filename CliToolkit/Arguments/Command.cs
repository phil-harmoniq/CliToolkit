using CliToolkit.Utilities;

namespace CliToolkit.Arguments
{
    public abstract class Command : Argument, ICommand
    {
        protected Command(string description, string keyword) : base(description, keyword)
        {
        }

        public abstract void OnExecute(string[] args);

        internal override int IsMatchingKeyword(string[] args)
        {
            if (args[0].Equals(Keyword)) { return 1; }
            return 0;
        }
    }
}