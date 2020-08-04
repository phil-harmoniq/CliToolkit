using CliToolkit.TestApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CliToolkit.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CliAppBuilder<ApplicationRoot>()
                //.SetName("CLI Test App")
                .ShowHeaderAndFooter()
                .Configure(c => c.AddJsonFile("appsettings.json"))
                .RegisterServices(RegisterServices)
                .Start(args);
            //return app.AppInfo.ExitCode;
        }

        private static void RegisterServices(IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<FakeService1>();
            services.AddSingleton<FakeService2>();
        }
    }
}
