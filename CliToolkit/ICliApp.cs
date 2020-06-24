using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CliToolkit
{
    public interface ICliApp : ICliCommand
    {
        IConfiguration Configuration { get; }
        IServiceCollection ServiceCollection { get; }
        int ExitCode { get; }
        string Name { get; }
        string Version { get; }
        int Width { get; }

        int Start(string[] args);
    }
}
