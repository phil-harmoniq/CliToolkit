namespace CliToolkit.TestApp.Commands
{
    [CliOptions(Description = "Demonstrate how to setup short-key configuration")]
    public class ShortKeyCommand : CliCommand
    {
        [CliOptions(ShortKey = 's', Description = "String option")]
        public string StringOption { get; set; }

        [CliOptions(ShortKey = 'i', Description = "Integer option")]
        public int IntOption { get; set; }

        [CliOptions(ShortKey = 'b', Description = "Boolean option")]
        public bool BoolOption { get; set; }

        public override void OnExecute(string[] args)
        {
            PrintHelpMenu();
        }
    }
}
