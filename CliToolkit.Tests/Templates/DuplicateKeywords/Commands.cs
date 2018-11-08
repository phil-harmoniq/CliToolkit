using CliToolkit.Arguments;

namespace CliToolkit.Tests.Templates.DuplicateKeywords
{
    public class DuplicateSubCommandKeywordApp : RuntimeExceptionApp
    {
        public Command DubplicateSubCommand = new ValidSubCommand("Duplicate default style subcommand", CommandKeyword);
    }
}