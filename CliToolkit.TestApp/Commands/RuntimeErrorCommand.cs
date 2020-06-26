using System;

namespace CliToolkit.TestApp.Commands
{
    [CliNamespace("CoolGuy")]
    public class RuntimeErrorCommand : CliCommand
    {
        [CliDescription("Don't try it!")]
        public bool IgnoreErrorFlag { get; set; }

        protected override void OnExecute(string[] args)
        {
            if (!IgnoreErrorFlag)
            {
                throw new NotImplementedException();
            }
            Console.WriteLine("Ignoring error...");
        }
    }
}
