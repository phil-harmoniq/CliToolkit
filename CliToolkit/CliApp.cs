using AnsiCodes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CliToolkit
{
    public abstract class CliApp : CliCommand, ICliApp
    {
        public IConfiguration Configuration { get; internal set; }
        public IServiceCollection ServiceCollection { get; internal set; }

        public int ExitCode { get; internal set; }
        public string Name { get; internal set; }
        public string Version { get; internal set; }
        public int Width { get; internal set; }

        public int Start(string[] args)
        {
            try
            {
                Parse(ServiceCollection, Configuration, args);
                return ExitCode = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Color.Red}{Format.Bold}{ex.Message}{Reset.All}");
                var logger = ServiceCollection.BuildServiceProvider().GetService<ILogger>();
                logger?.LogError(ex, ex.Message);
                return ExitCode = 1;
            }
        }
    }

    public abstract class CliApp<TOptions> : CliCommand<TOptions>, ICliApp where TOptions : class
    {
        public IConfiguration Configuration { get; internal set; }
        public IServiceCollection ServiceCollection { get; internal set; }

        public int ExitCode { get; internal set; }
        public string Name { get; internal set; }
        public string Version { get; internal set; }
        public int Width { get; internal set; }

        public int Start(string[] args)
        {
            try
            {
                Parse(ServiceCollection, Configuration, args);
                return ExitCode = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Color.Red}{Format.Bold}{ex.Message}{Reset.All}");
                var logger = ServiceCollection.BuildServiceProvider().GetService<ILogger>();
                logger.LogError(ex, ex.Message);
                return ExitCode = 1;
            }
        }
    }
}
