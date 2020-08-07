using CliToolkit.TestApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CliToolkit.Tests.Integration.Abstract
{
    public class IntegrationTestBase
    {
        public ApplicationRoot App { get; }

        public IntegrationTestBase()
        {
            App = new CliAppBuilder<ApplicationRoot>()
                .Configure(c => c.AddJsonFile("appsettings.json"))
                .RegisterServices(RegisterServices)
                .Build();
        }

        protected virtual void RegisterServices(IServiceCollection services, IConfiguration config) { }
    }
}
