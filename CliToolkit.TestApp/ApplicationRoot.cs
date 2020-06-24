using CliToolkit.TestApp.Commands;

namespace CliToolkit.TestApp
{
    public class ApplicationRoot : CliApp
    {
        public HelloWorldCommand HelloWorldCommand { get; internal set; }
        public RuntimeErrorCommand RuntimeErrorCommand { get; internal set; }

        protected override void OnExecute(string[] args)
        {
            throw new System.NotImplementedException();
        }
    }
}
