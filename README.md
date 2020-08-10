# CliToolkit [![Build status](https://dev.azure.com/phil-harmoniq/devops/_apis/build/status/CliToolkit)](https://dev.azure.com/phil-harmoniq/devops/_build/latest?definitionId=7) [![NuGet](https://img.shields.io/nuget/v/CliToolkit.svg)](https://www.nuget.org/packages/CliToolkit)

## Installing

Install using the .NET CLI or your IDE's package manager:

```bash
dotnet add package CliToolkit
```

## Usage

Begin by inheriting `CliApp` in your main class and overriding the default `OnExecute()` method. Public properties will have their values injected from command-line arguments detected using either `--PascalCase` or `--kebab-case`. Public properties types that derive from `CliCommand` will automatically be registered as a sub-command route.

Under the hood, CliToolkit uses familiar extensions available in ASP.NET Core like dependency injection and configuration. The command-line parser uses [`AddCommandLine()`](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1#command-line) and switch maps, so all values can also be supplied via other registered configuration providers. (like environment variables, user secrets, Azure key vault, etc) Dependency injection via constuctor is also available out of the box.

## Example

```c#
using CliToolkit;

public class Program : CliApp
{
    static int Main(string[] args)
    {
        var app = new CliAppBuilder<ApplicationRoot>()
            .Start(args);
        return app.ExitCode;
    }

    [CliOptions(Description = "Simulate a long-running process")]
    public TimerCommand Timer { get; set; }

    public override void OnExecute(string[] args)
    {
        PrintHelpMenu();
    }
}

public class TimerCommand : CliCommand
{
    public string Title { get; set; } = "Default";
    public int Seconds { get; set; } = 5;
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
```

```bash
$ dotnet run timer --title Custom --seconds 3 --time-stamp true
Custom timer start
1
2
3
8/9/2020 8:44:19 PM
```

This is a small portion of the example in [`CliToolkit.TestApp`](CliToolkit.TestApp). Go check it out for the full implementation.

## Supporting Links

- Check out the [CliToolkit wiki](https://github.com/phil-harmoniq/CliToolkit/wiki) for more usage details
- Check out some of my other projects on my [personal site](http://phil.harmoniq.dev/)
