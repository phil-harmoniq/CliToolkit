using System;
using System.Linq;

namespace CliToolkit.TestApp.Commands
{
    [CliOptions(Description = "Show console app help menu")]
    public class DefaultHelpMenuCommand : CliCommand
    {
        public string StringValue { get; set; }

        public int IntValue { get; set; }

        public bool BoolValue { get; set; }

        public override void OnExecute(string[] args)
        {
            if (args.Length == 0)
            {
                PrintHelpMenu();
                throw new Exception("");
            }
            if (args.Contains("--help"))
            {
                PrintHelpMenu();
            }
        }
    }
}
