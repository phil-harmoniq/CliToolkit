using System;
using CliToolkit.Arguments;

namespace CliToolkit.Tests.Templates
{
    public class ValidSubCommand : Command
    {
        public ValidSubCommand(string description, string keyword) : base(description, keyword)
        {
        }

        public override void OnExecute(string[] args)
        {
            Console.WriteLine("This valid command should execute without errors.");
        }
    }
}