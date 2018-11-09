namespace CliToolkit.Core
{
    public interface ICommand
    {
        void OnExecute(string[] args);
    }
}