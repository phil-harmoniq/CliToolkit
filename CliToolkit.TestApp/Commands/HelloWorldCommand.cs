using System;

namespace CliToolkit.TestApp.Commands
{
    public class HelloWorldCommand : CliCommand<HelloWorldOptions>
    {
        protected override void OnExecute(HelloWorldOptions options, string[] args)
        {
            Console.WriteLine("Hello, world!");
        }
    }

    public class HelloWorldOptions
    {
    }
}
