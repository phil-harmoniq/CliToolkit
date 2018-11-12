namespace CliToolkit.Arguments
{
    internal class HelpMenuCommand : Command
    {
        public HelpMenuCommand(string description, string keyword) : base(description, keyword)
        {
        }

        public override void OnExecute(string[] args)
        {
            PrintHelpMenu();
        }
    }
}