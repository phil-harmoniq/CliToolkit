using CliToolkit.TestApp;
using CliToolkit.TestApp.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CliToolkit.Tests
{
    public class AlternateConfigurationTests
    {
        [Fact]
        public void DefaultActionTest()
        {
            var app = new CliAppBuilder<ApplicationRoot>()
                .Configure(c => c.AddJsonFile("appsettings.json"))
                .RegisterServices(Register)
                .Start(new[] { "alternate", "--bool-from-console=true",
                    "--AlternateConfigurationOptions:StringFromConsole=this-is-console" });

            Assert.True(app.Alternate.BoolFromConsole);
            Assert.True(app.Alternate.BoolFromJson);
            Assert.Equal("this-is-console", app.Alternate.Options.StringFromConsole);
            Assert.Equal("this-is-json", app.Alternate.Options.StringFromJson);
        }

        private void Register(IServiceCollection sc, IConfiguration config)
        {
            sc.AddConfig<AlternateConfigurationOptions>(config);
        }
    }
}
