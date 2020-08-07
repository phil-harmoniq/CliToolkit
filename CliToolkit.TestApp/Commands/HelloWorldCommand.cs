using System;

namespace CliToolkit.TestApp.Commands
{
    public class HelloWorldCommand : CliCommand
    {
        public override void OnExecute(string[] args)
        {
            Console.WriteLine("Hello, world!");
        }
    }
}
