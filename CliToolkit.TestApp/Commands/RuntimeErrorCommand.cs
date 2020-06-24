namespace CliToolkit.TestApp.Commands
{
    [CliCommandRoute("error")]
    public class RuntimeErrorCommand : CliCommand<RuntimeErrorOptions>
    {
        protected override void OnExecute(RuntimeErrorOptions options, string[] args)
        {
            throw new System.NotImplementedException();
        }
    }

    public class RuntimeErrorOptions
    {

    }
}
