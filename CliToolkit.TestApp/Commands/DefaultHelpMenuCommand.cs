using System;
using System.Linq;

namespace CliToolkit.TestApp.Commands
{
    [CliDescription("Example of common help menu handling")]
    public class DefaultHelpMenuCommand : CliCommand
    {
        [CliDescription("Give me a string!")]
        public string StringValue { get; set; }

        [CliDescription("Give me an int!")]
        public int IntValue { get; set; }

        [CliDescription("Give me a bool!")]
        public bool BoolValue { get; set; }

        protected override void OnExecute(string[] args)
        {
            if (args.Length == 0)
            {
                throw new Exception("");
            }
            if (args.Contains("--help"))
            {
                PrintHelpMenu();
            }
        }
    }
}
