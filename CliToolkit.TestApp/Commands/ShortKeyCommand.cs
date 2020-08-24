using System;

namespace CliToolkit.TestApp.Commands
{
    [CliOptions(Description = "Demonstrate how to setup short-key configuration")]
    public class ShortKeyCommand : CliCommand
    {
        [CliOptions(ShortKey = 's', Description = "String option")]
        public string StringOption { get; set; }

        [CliOptions(ShortKey = 'i', Description = "Integer option")]
        public int IntOption { get; set; }

        [CliOptions(ShortKey = 'b', Description = "Boolean option")]
        public bool BoolOption { get; set; }

        [CliOptions(ShortKey = 'h', Description = "Show help menu")]
        public bool Help { get; set; }

        public override void OnExecute(string[] args)
        {
            if (Help)
            {
                PrintHelpMenu();
                return;
            }

            Console.WriteLine($"String Option: {StringOption}");
            Console.WriteLine($"Integer Option: {IntOption}");
            Console.WriteLine($"Boolean Option: {BoolOption}");
        }
    }
}
