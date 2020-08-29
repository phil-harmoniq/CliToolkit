using CliToolkit.TestApp.Commands;

namespace CliToolkit.TestApp
{
    [CliOptions("CliToolkit test application")]
    public class ApplicationRoot : CliApp
    {
        [CliOptions("Print a hello message to the console")]
        public HelloWorldCommand Hello { get; set; }

        [CliOptions("Simulate throwing an error")]
        public RuntimeErrorCommand Error { get; set; }

        [CliOptions("Demonstrate how to setup default help menu trigger")]
        public DefaultHelpMenuCommand HelpMenu { get; set; }

        [CliOptions("Test various configuration options")]
        public ConfigurationValueCommand Config { get; set; }

        [CliOptions("Test configuration through IOptions injection")]
        public AlternateConfigurationCommand Alternate { get; set; }

        [CliOptions("Test built-in dependency injection")]
        public DependencyInjectionCommand DependencyInjection { get; set; }

        [CliOptions("Test short-key configuration")]
        public ShortKeyCommand ShortKey { get; set; }

        [CliOptions("Test short-key duplication error")]
        public ShortKeyDuplicateCommand ShortKeyDuplicate { get; set; }

        [CliOptions("Simulate a long-running process")]
        public TimerCommand Timer { get; set; }

        [CliOptions("Test implicit/explicit boolean implementations")]
        public ExplicitBoolCommand ExplicitBool { get; set; }

        [CliOptions("Test parsed input argument filter")]
        public ArgFilterCommand ArgFilter { get; set; }

        [CliOptions("Display help menu", 'h')]
        public bool Help { get; set; }

        public override void OnExecute(string[] args)
        {
            PrintHelpMenu();
            if (Help) { return; }
            throw new CliException();
        }
    }
}
