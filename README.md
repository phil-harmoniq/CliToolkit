# CliToolkit [![Build status](https://dev.azure.com/phil-harmoniq/devops/_apis/build/status/CliToolkit)](https://dev.azure.com/phil-harmoniq/devops/_build/latest?definitionId=7) [![NuGet](https://img.shields.io/nuget/v/CliToolkit.svg)](https://www.nuget.org/packages/CliToolkit)

## Installing

Install using the .NET CLI or your IDE's package manager:

```bash
dotnet add package CliToolkit
```

## Usage

Begin by inheriting `CliApp` in your main class and overriding the default `OnExecute()` method. Add flags and properties to this class and use an `AppBuilder` to customize the functionality of your command-line application.

## Example

```c#
public class Program : CliApp
{
    static int Main(string[] args)
    {
        var app = new AppBuilder<Program>()
            .Start(args);
        return app.ExitCode;
    }

    public Command HelloWorld = new HelloWorldCommand("Say hello world", "hello-world");

    public override void OnExecute(string[] args)
    {
        PrintHelpMenu();
    }
}

public class HelloWorldCommand : Command
{
    public Flag VerboseFlag = new Flag("Shows header/footer and any additional information.", "verbose", 'v');

    public HelloWorldCommand(string description, string keyword) : base(description, keyword)
    {
    }

    public override void OnExecute(string[] args)
    {
        if (VerboseFlag.IsActive) { PrintHeader(); }
        Console.WriteLine("Hello, world!");
    }
}
```

This is a shortened version of the [`CliToolkit.Example` app](https://github.com/phil-harmoniq/CliToolkit/blob/master/CliToolkit.Example/Program.cs). Go check it out for the full implementation.

## Contributing

PRs, issue reports, and advice are always welcome! It is recommended you use VS Code to work with the CliToolkit source code. The workspace includes build tasks, editor settings, extension recommendations, and launch configurations to aid development in VS Code, but any editor that supports C# will do.

Type Ctrl + Shift + B (or Cmd + Shift + B on Macs) to bring up the build tasks menu:

![Tasks Menu](https://imgur.com/Fah7i33.jpg)

## Supporting Links

- Check out the [CliToolkit wiki](https://github.com/phil-harmoniq/CliToolkit/wiki) for more usage details
- Check out some of my other projects on my [personal site](http://phil-hawkins.me/)
- Icon made by [Freepik](http://www.freepik.com) from [www.flaticon.com](https://www.flaticon.com) is licensed by [CC 3.0 BY](http://creativecommons.org/licenses/by/3.0/)