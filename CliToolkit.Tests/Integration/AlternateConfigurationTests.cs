using CliToolkit.TestApp.Commands;
using CliToolkit.Tests.Integration.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CliToolkit.Tests.Integration
{
    public class AlternateConfigurationTests : IntegrationTestBase
    {
        [Fact]
        public void DefaultActionTest()
        {
            App.Start(new[] { "alternate", "--bool-from-console=true",
                "--AlternateConfigurationOptions:StringFromConsole=this-is-console" });

            Assert.NotNull(App.Alternate);
            Assert.True(App.Alternate.BoolFromConsole);
            Assert.True(App.Alternate.BoolFromJson);
            Assert.Equal("this-is-console", App.Alternate.Options.StringFromConsole);
            Assert.Equal("this-is-json", App.Alternate.Options.StringFromJson);
        }

        protected override void RegisterServices(IServiceCollection services, IConfiguration config)
        {
            base.RegisterServices(services, config);
            services.AddConfig<AlternateConfigurationOptions>(config);
        }
    }
}
