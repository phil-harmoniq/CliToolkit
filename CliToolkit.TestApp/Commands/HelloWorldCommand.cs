using System;

namespace CliToolkit.TestApp.Commands
{
    public class HelloWorldCommand : CliCommand
    {
        protected override void OnExecute(string[] args)
        {
            Console.WriteLine("Hello, world!");
        }
    }
}
