using Microsoft.Extensions.Configuration;
using System;

namespace CliToolkit
{
    public abstract class CliApp : CliCommand
    {
        public IConfiguration Configuration { get; internal set; }
        public IServiceProvider ServiceProvider { get; internal set; }

        public int ExitCode { get; internal set; }
        public string Name { get; internal set; }
        public string Version { get; internal set; }
        public int Width { get; internal set; }
    }

    public abstract class CliApp<TOptions> : CliCommand<TOptions> where TOptions : class
    {
        public IConfiguration Configuration { get; internal set; }
        public IServiceProvider ServiceProvider { get; internal set; }

        public int ExitCode { get; internal set; }
        public string Name { get; internal set; }
        public string Version { get; internal set; }
        public int Width { get; internal set; }
    }
}
