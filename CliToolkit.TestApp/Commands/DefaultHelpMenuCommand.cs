using System;
using System.Linq;

namespace CliToolkit.TestApp.Commands
{
    public class DefaultHelpMenuCommand : CliCommand
    {
        public string StringValue { get; set; }

        public int IntValue { get; set; }

        public bool BoolValue { get; set; }

        protected override void OnExecute(string[] args)
        {
            if (args.Length == 0)
            {
                throw new Exception("");
            }
            if (args.Contains("--help"))
            {
                //PrintHelpMenu();
            }
        }
    }
}
