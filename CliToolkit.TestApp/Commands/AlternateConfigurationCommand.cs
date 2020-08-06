namespace CliToolkit.TestApp.Commands
{
    public class AlternateConfigurationCommand : CliCommand
    {
        public AlternateConfigurationOptions Options { get; }

        [CliOptions(Description = "Show the boolean value provided by appsettings.json")]
        public bool BoolFromJson { get; set; }

        [CliOptions(Description = "Show the boolean value provided by the console")]
        public bool BoolFromConsole { get; set; }

        public AlternateConfigurationCommand(AlternateConfigurationOptions options)
        {
            Options = options;
        }

        public override void OnExecute(string[] args)
        {
        }
    }

    public class AlternateConfigurationOptions
    {
        public string StringFromJson { get; set; }
        public string StringFromConsole { get; set; }
    }
}
