namespace CliToolkit.Utilities
{
    public interface ICommand
    {
        void OnExecute(string[] args);
    }
}