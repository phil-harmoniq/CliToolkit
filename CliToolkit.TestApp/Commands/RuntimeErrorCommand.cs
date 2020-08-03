using System;

namespace CliToolkit.TestApp.Commands
{
    public class RuntimeErrorCommand : CliCommand
    {
        public bool IgnoreError { get; set; }

        protected override void OnExecute(string[] args)
        {
            if (!IgnoreError)
            {
                throw new NotImplementedException();
            }
            Console.WriteLine("Ignoring error...");
        }
    }
}
