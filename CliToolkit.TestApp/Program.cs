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
                //.Configure(Configure)
                //.RegisterServices(RegisterServices)
                .Start(new string[] { "runtime-error", "--ignore-error-flag=true"} );
            return app.ExitCode;
        }

        private static void Configure(IConfigurationBuilder obj)
        {
            throw new NotImplementedException();
        }

        private static void RegisterServices(IServiceCollection obj)
        {
            throw new NotImplementedException();
        }
    }
}
