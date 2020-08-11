using System;

namespace CliToolkit.TestApp.Commands
{
    public class ExplicitBoolCommand : CliCommand
    {
        [CliExplicitBool]
        public bool ExplicitBool { get; set; } = true;
        public bool ImplicitBool { get; set; }

        public override void OnExecute(string[] args)
        {
            PrintHelpMenu();
        }
    }
}
