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
                throw new CliException(nameof(RuntimeErrorCommand), 5);
            }
            Console.WriteLine("Ignoring error...");
        }
    }
}
