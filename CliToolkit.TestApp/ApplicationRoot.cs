using CliToolkit.TestApp.Commands;
using System;

namespace CliToolkit.TestApp
{
    [CliOptions(Description = "CliToolkit test application")]
    public class ApplicationRoot : CliApp
    {
        [CliOptions(Description = "Print a hello message to the console")]
        public HelloWorldCommand Hello { get; set; }

        [CliOptions(Description = "Simulate throwing an error")]
        public RuntimeErrorCommand Error { get; set; }

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

        protected override void OnExecute(string[] args)
        {
            PrintHelpMenu();
            throw new Exception("");
        }
    }
}
