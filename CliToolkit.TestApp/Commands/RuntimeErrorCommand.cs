using System;

namespace CliToolkit.TestApp.Commands
{
    public class RuntimeErrorCommand : CliCommand
    {
        public bool IgnoreError { get; set; }
        public bool Critical { get; set; }

        public override void OnExecute(string[] args)
        {
            if (Critical)
            {
                throw new Exception($"{nameof(RuntimeErrorCommand)} {nameof(Critical)}");
            }
            else if (!IgnoreError)
            {
                throw new CliException(nameof(RuntimeErrorCommand), 5);
            }
            Console.WriteLine("Ignoring error...");
        }
    }
}
