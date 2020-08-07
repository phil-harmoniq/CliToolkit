using CliToolkit.TestApp;
using CliToolkit.TestApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CliToolkit.Tests.Integration.Abstract
{
    public class IntegrationTestFixture
    {
        public ApplicationRoot App { get; }

        public IntegrationTestFixture()
        {
            App = new CliAppBuilder<ApplicationRoot>()
                .Configure(c => c.AddJsonFile("appsettings.json"))
                .RegisterServices(RegisterServices)
                .Build();
        }

        protected void RegisterServices(IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<FakeService1>();
            services.AddSingleton<FakeService2>();
        }
    }
}
