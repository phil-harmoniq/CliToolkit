using CliToolkit.TestApp.Commands;

namespace CliToolkit.TestApp
{
    public class ApplicationRoot : CliApp
    {
        [CliMenu("Print hello world")]
        public HelloWorldCommand HelloWorldCommand { get; internal set; }

        [CliMenu("Simulate a run-time error")]
        public RuntimeErrorCommand RuntimeErrorCommand { get; internal set; }

        protected override void OnExecute(string[] args)
        {
            throw new System.NotImplementedException();
        }
    }
}
