using System;

namespace CliToolkit.Example
{
    public class HelloWorldCommand : Command
    {
        public Flag VerboseFlag = new Flag("Shows header/footer and any additional information.", "verbose", 'v');

        public HelloWorldCommand(string description, string keyword) : base(description, keyword)
        {
        }

        public override void OnExecute(string[] args)
        {
            if (VerboseFlag.IsActive) { PrintHeader(); }
            Console.WriteLine("Hello, world!");
        }
    }
}