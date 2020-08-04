using CliToolkit.TestApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CliToolkit.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CliAppBuilder<ApplicationRoot>()
                .SetName("CliToolkit Test App")
                .SetVersion("1.2.3.4-alpha")
                .SetMenuWidth(84)
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
