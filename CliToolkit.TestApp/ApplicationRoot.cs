using CliToolkit.TestApp.Commands;
using System;

namespace CliToolkit.TestApp
{
    public class ApplicationRoot : CliApp
    {
        [CliMenu("Print hello world")]
        public HelloWorldCommand HelloWorldCommand { get; internal set; }

        [CliMenu("Simulate a run-time error")]
        public RuntimeErrorCommand RuntimeErrorCommand { get; internal set; }

        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public bool BoolValue { get; set; }

        protected override void OnExecute(string[] args)
        {
            PrintHelpMenu();
            throw new Exception();
        }
    }
}
