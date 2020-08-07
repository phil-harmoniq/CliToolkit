using CliToolkit.Tests.Integration.Abstract;
using Xunit;

namespace CliToolkit.Tests.Integration
{
    public class ConfigrationValueTests : IntegrationTestBase
    {
        [Fact]
        public void DefaultActionTest()
        {
            App.Start(new[] { "config" });

            Assert.Equal("Wow!", App.Config.StringValue);
            Assert.Equal(42, App.Config.IntValue);
            Assert.True(App.Config.BoolValue);
        }
    }
}
