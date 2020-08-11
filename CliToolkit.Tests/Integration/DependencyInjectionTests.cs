using CliToolkit.TestApp.Services;
using CliToolkit.Tests.Integration.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CliToolkit.Tests.Integration
{
    public class DependencyInjectionTests : IntegrationTestBase
    {
        [Fact]
        public void InjectServicesTests()
        {
            App.Start(new[] { "dependency-injection" });

            Assert.NotNull(App.DependencyInjection);
            Assert.NotNull(App.DependencyInjection.FakeService1);
            Assert.NotNull(App.DependencyInjection.FakeService2);
            Assert.Null(App.DependencyInjection.NullService1);
            Assert.Null(App.DependencyInjection.NullService2);
        }

        protected override void RegisterServices(IServiceCollection services, IConfiguration config)
        {
            base.RegisterServices(services, config);
            services.AddSingleton<FakeService1>();
            services.AddSingleton<FakeService2>();
        }
    }
}
