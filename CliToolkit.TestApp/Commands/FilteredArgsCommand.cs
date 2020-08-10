using System;

namespace CliToolkit.TestApp.Commands
{
    public class FilteredArgsCommand : CliCommand
    {
        public string StringOption { get; set; }
        public int IntOption { get; set; }
        public bool BoolOption { get; set; }

        public override void OnExecute(string[] args)
        {
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }
        }
    }
}
