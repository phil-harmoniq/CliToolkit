using CliToolkit.TestApp.Commands;

namespace CliToolkit.TestApp
{
    [CliOptions(Description = "CliToolkit test application")]
    public class ApplicationRoot : CliApp
    {
        [CliOptions(Description = "Print a hello message to the console")]
        public HelloWorldCommand Hello { get; set; }

        [CliOptions(Description = "Simulate throwing an error")]
        public RuntimeErrorCommand Error { get; set; }

        [CliOptions(Description = "Demonstrate how to setup default help menu trugger")]
        public DefaultHelpMenuCommand HelpMenu { get; set; }

        [CliOptions(Description = "Test various configuration options")]
        public ConfigurationValueCommand Config { get; set; }

        [CliOptions(Description = "Test configuration through IOptions injection")]
        public AlternateConfigurationCommand Alternate { get; set; }

        [CliOptions(Description = "Test built-in dependency injection")]
        public DependencyInjectionCommand DependencyInjection { get; set; }

        [CliOptions(Description = "Test short-key configuration")]
        public ShortKeyCommand ShortKey { get; set; }

        [CliOptions(Description = "Test short-key duplication error")]
        public ShortKeyDuplicateCommand ShortKeyDuplicate { get; set; }

        [CliOptions(Description = "Simulate a long-running process")]
        public TimerCommand Timer { get; set; }

        public ExplicitBoolCommand ExplicitBool { get; set; }
        public ArgFilterCommand ArgFilter { get; set; }

        [CliOptions(Description = "Display help menu")]
        public bool Help { get; set; }

        public override void OnExecute(string[] args)
        {
            if (Help)
            {
                PrintHelpMenu();
                return;
            }

            throw new CliException("Please specify a sub-command.");
        }
    }
}
