using System;

namespace CliToolkit.TestApp.Commands
{
    public class ConfigurationValueCommand : CliCommand
    {
        [CliDescription("Give me a string!")]
        public string StringValue { get; set; }

        [CliDescription("Give me an int!")]
        public int IntValue { get; set; }

        [CliDescription("Give me a bool!")]
        public bool BoolValue { get; set; }

        protected override void OnExecute(string[] args)
        {
            Console.WriteLine($"{nameof(StringValue)}: {StringValue}");
            Console.WriteLine($"{nameof(IntValue)}: {IntValue}");
            Console.WriteLine($"{nameof(BoolValue)}: {BoolValue}");
        }
    }
}
