using System;

namespace CliToolkit.TestApp.Commands
{
    public class ConfigurationValueCommand : CliCommand
    {
        public string StringValue { get; set; }

        public int IntValue { get; set; }

        public bool BoolValue { get; set; }

        public override void OnExecute(string[] args)
        {
            Console.WriteLine($"{nameof(StringValue)}: {StringValue}");
            Console.WriteLine($"{nameof(IntValue)}: {IntValue}");
            Console.WriteLine($"{nameof(BoolValue)}: {BoolValue}");
        }
    }
}
