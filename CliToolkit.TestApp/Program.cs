using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CliToolkit.TestApp
{
    class Program
    {
        static int Main(string[] args)
        {
            var app = new CliAppBuilder<ApplicationRoot>()
                .SetName("CLI Test App")
                //.Configure(Configure)
                //.RegisterServices(RegisterServices)
                .Start(args);
            return app.ExitCode;
        }
    }
}
