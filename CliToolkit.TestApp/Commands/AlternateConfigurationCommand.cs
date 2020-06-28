namespace CliToolkit.TestApp.Commands
{
    public class AlternateConfigurationCommand : CliCommand
    {
        public AlternateConfigurationOptions Options { get; }

        public bool BoolFromJson { get; set; }
        public bool BoolFromConsole { get; set; }

        public AlternateConfigurationCommand(AlternateConfigurationOptions options)
        {
            Options = options;
        }

        protected override void OnExecute(string[] args)
        {
        }
    }

    public class AlternateConfigurationOptions
    {
        public string StringFromJson { get; set; }
        public string StringFromConsole { get; set; }
    }
}
