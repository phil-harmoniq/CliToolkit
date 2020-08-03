using CliToolkit.TestApp.Services;
using System;

namespace CliToolkit.TestApp.Commands
{
    public class DependencyInjectionCommand : CliCommand
    {
        public FakeService1 FakeService1 { get; }
        public FakeService2 FakeService2 { get; }

        public DependencyInjectionCommand(
            FakeService1 fakeService1,
            FakeService2 fakeService2)
        {
            FakeService1 = fakeService1;
            FakeService2 = fakeService2;
        }

        protected override void OnExecute(string[] args)
        {
            Console.WriteLine($"{nameof(FakeService1)}: {FakeService1}");
            Console.WriteLine($"{nameof(FakeService2)}: {FakeService2}");
        }
    }
}
