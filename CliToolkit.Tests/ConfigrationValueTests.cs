using CliToolkit.TestApp;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CliToolkit.Tests
{
    public class ConfigrationValueTests
    {
        [Fact]
        public void DefaultActionTest()
        {
            var app = new CliAppBuilder<ApplicationRoot>()
                .Configure(c => c.AddJsonFile("appsettings.json"))
                .Start(new[] { "config" });

            Assert.Equal("Wow!", app.Config.StringValue);
            Assert.Equal(42, app.Config.IntValue);
            Assert.True(app.Config.BoolValue);
        }
    }
}
