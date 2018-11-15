using CliToolkit.Arguments;

namespace CliToolkit.Tests.Templates.DuplicateKeywords
{
    public class DuplicateCommandKeywordApp : RuntimeExceptionApp
    {
        public Command DuplicateCommand = new ValidCommand("Duplicate default style command", DefaultCommandKeyword);
    }
}