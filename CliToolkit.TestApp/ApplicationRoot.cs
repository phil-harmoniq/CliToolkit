using CliToolkit.TestApp.Commands;
using System;

namespace CliToolkit.TestApp
{
    [CliDescription("Test application for the CliToolkit library")]
    public class ApplicationRoot : CliApp
    {
        [CliDescription("Print hello world")]
        public HelloWorldCommand HelloCommand { get; set; }

        [CliDescription("Simulate a run-time error")]
        public RuntimeErrorCommand ErrorCommand { get; set; }

        [CliDescription("Example of common help paramater parsing")]
        public DefaultHelpMenuCommand HelpMenuCommand { get; set; }

        [CliDescription("Show detected configuration variables")]
        public ConfigurationValueCommand ConfigCommand { get; set;}

        protected override void OnExecute(string[] args)
        {
            PrintHelpMenu();
            throw new Exception("");
        }
    }
}
