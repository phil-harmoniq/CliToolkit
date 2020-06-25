using System;

namespace CliToolkit.TestApp.Commands
{
    public class RuntimeErrorCommand : CliCommand
    {
        public bool IgnoreErrorFlag { get; set; }

        protected override void OnExecute(string[] args)
        {
            if (!IgnoreErrorFlag)
            {
                throw new System.NotImplementedException();
            }
            Console.WriteLine("Ignoring error...");
        }
    }
}
