using CliToolkit.TestApp;
using CliToolkit.TestApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace CliToolkit.Tests
{
    public class DependencyInjectionTests
    {
        [Fact]
        public void InjectServicesTests()
        {
            var app = new CliAppBuilder<ApplicationRoot>()
                .RegisterServices(RegisterServices)
                .Start(new[] { "dependency-injection" });

            Assert.NotNull(app.DependencyInjection.FakeService1);
            Assert.NotNull(app.DependencyInjection.FakeService2);
            Assert.Null(app.DependencyInjection.NullService1);
            Assert.Null(app.DependencyInjection.NullService2);
        }

        private void RegisterServices(IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<FakeService1>();
            services.AddSingleton<FakeService2>();
        }
    }
}
