namespace CliToolkit.TestApp.Commands
{
    [CliOptions(Description = "Demonstrate that attempting to set"
        + "duplicate short-keys will throw a builder exception")]
    public class ShortKeyDuplicateCommand : CliCommand
    {
        [CliOptions(ShortKey = 's', Description = "String option")]
        public string StringOption { get; set; }

        [CliOptions(ShortKey = 's', Description = "String option 2")]
        public string StringOption2 { get; set; }

        protected override void OnExecute(string[] args)
        {
            PrintHelpMenu();
        }
    }
}
