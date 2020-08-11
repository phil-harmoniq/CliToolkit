using System;

namespace CliToolkit.TestApp.Commands
{
    public class ArgFilterCommand : CliCommand
    {
        public string StringOption { get; set; }
        public int IntOption { get; set; }
        public bool BoolOption { get; set; }

        public string[] Args { get; private set; }

        public override void OnExecute(string[] args)
        {
            Args = args;
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }
        }
    }
}
