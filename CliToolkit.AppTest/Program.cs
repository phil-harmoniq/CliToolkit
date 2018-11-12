using CliToolkit.Arguments;

namespace CliToolkit.AppTest
{
    class Program : CliApp
    {
        static void Main(string[] args)
        {
            var app = new AppBuilder<Program>()
                .Start(args);
        }

        public Command ListFiles = new ListFilesCommand("List files in the given directory", "list");

        public override void OnExecute(string[] args)
        {
            PrintHelpMenu();
        }
    }
}
