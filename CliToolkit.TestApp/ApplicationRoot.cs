using CliToolkit.TestApp.Commands;
using System;

namespace CliToolkit.TestApp
{
    [CliDescription("Test application for the CliToolkit library")]
    public class ApplicationRoot : CliApp
    {
        [CliDescription("Print hello world")]
        public HelloWorldCommand Hello { get; set; }

        [CliDescription("Simulate a run-time error")]
        public RuntimeErrorCommand Error { get; set; }

        [CliDescription("Example of common help paramater parsing")]
        public DefaultHelpMenuCommand HelpMenu { get; set; }

        [CliDescription("Show detected configuration variables")]
        public ConfigurationValueCommand Config { get; set;}

        protected override void OnExecute(string[] args)
        {
            PrintHelpMenu();
            throw new Exception("");
        }
    }
}
