using System;

namespace CliToolkit.TestApp.Commands
{
    public class ExplicitBoolCommand : CliCommand
    {
        public bool IsActive { get; set; }

        public override void OnExecute(string[] args)
        {
            PrintHelpMenu();
        }
    }
}
