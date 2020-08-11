using System;
using System.Threading;

namespace CliToolkit.TestApp.Commands
{
    [CliOptions(Description = "Long-running process simulation")]
    public class TimerCommand : CliCommand
    {
        [CliOptions(Description = "Timer display name")]
        public string Title { get; set; } = "Default";

        [CliOptions(Description = "Timer duration")]
        public int Seconds { get; set; } = 5;

        [CliOptions(Description = "Log current time")]
        public bool TimeStamp { get; set; }

        public override void OnExecute(string[] args)
        {
            Console.WriteLine($"{Title} timer start");

            for (var i = 1; i <= Seconds; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.WriteLine(i);
            }

            if (TimeStamp)
            {
                Console.WriteLine(DateTime.Now);
            }
        }
    }
}
