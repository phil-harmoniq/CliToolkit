using Microsoft.Extensions.Configuration;
using System;

namespace CliToolkit
{
    public interface ICliApp : ICliCommand
    {
        IConfiguration Configuration { get; }
        IServiceProvider ServiceProvider { get; }
        int ExitCode { get; }
        string Name { get; }
        string Version { get; }
        int Width { get; }
    }
}
