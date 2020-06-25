using CliToolkit.TestApp.Commands;
using System;

namespace CliToolkit.TestApp
{
    public class ApplicationRoot : CliApp
    {
        [CliDescription("Print hello world")]
        public HelloWorldCommand HelloCommand { get; internal set; }

        [CliDescription("Simulate a run-time error")]
        public RuntimeErrorCommand ErrorCommand { get; internal set; }

        [CliDescription("Give me a string!")]
        public string StringValue { get; set; }

        [CliDescription("Give me an int!")]
        public int IntValue { get; set; }

        [CliDescription("Give me a bool!")]
        public bool BoolValue { get; set; }

        protected override void OnExecute(string[] args)
        {
            PrintHelpMenu();
            throw new Exception();
        }
    }
}
