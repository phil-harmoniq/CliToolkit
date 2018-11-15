namespace CliToolkit.AppTest
{
    class Program : CliApp
    {
        static int Main(string[] args)
        {
            var app = new AppBuilder<Program>()
                .Start(args);
            return app.ExitCode;
        }

        public Command ListFiles = new ListFilesCommand("List files in the given directory", "list");
        public Command ThrowError = new RuntimeErrorCommand("Throws a runtime error but exits gracefully", "error");

        public override void OnExecute(string[] args)
        {
            PrintHelpMenu();
        }
    }
}
