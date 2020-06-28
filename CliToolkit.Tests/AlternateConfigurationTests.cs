using CliToolkit.TestApp;
using CliToolkit.TestApp.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
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
                .Start(new[] { "alternate", "--AlternateConfigurationOptions:StringFromConsole=plzzz" });

            Assert.Equal("Wow!", app.Config.StringValue);
            Assert.Equal(42, app.Config.IntValue);
            Assert.True(app.Config.BoolValue);
        }

        private void Register(IServiceCollection sc, IConfiguration config)
        {
            sc.AddConfig<AlternateConfigurationOptions>(config);
            sc.AddConfig<CliOptions>(config);
        }
    }
}
