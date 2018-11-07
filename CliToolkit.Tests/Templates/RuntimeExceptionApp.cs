using CliToolkit.Exceptions;

namespace CliToolkit.Tests.Templates
{
    public class RuntimeExceptionApp : ValidApp
    {
        public override void OnExecute(string[] args)
        {
            throw new AppRuntimeException("This application should not reach run-time.");
        }
    }
}