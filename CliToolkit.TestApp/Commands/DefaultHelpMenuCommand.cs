namespace CliToolkit.TestApp.Commands
{
    [CliOptions(Description = "Demonstrate how to setup help menu argument common in most CLI apps")]
    public class DefaultHelpMenuCommand : CliCommand
    {
        [CliOptions(ShortKey = 'h', Description = "Explicitly call the help menu")]
        public bool Help { get; set; }

        public override void OnExecute(string[] args)
        {
            if (Help)
            {
                PrintHelpMenu();
                return;
            }

            throw new CliException("The help menu wasn't explicitly called");
        }
    }
}
