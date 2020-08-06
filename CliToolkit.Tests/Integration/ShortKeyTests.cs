using CliToolkit.TestApp;
using CliToolkit.Tests.Integration.Abstract;
using Xunit;

namespace CliToolkit.Tests.Integration
{
    public class ShortKeyTests : IntegrationTestBase
    {
        [Fact]
        public void DefaultActionTest()
        {
            var stringValue = "String value";
            var intValue = 5;
            var boolValue = true;

            App.Start(new[] {
                "short-key",
                "-s", stringValue,
                $"-i={intValue}",
                "-b", boolValue.ToString()
            });

            Assert.NotNull(App.ShortKey);
            Assert.Equal(stringValue, App.ShortKey.StringOption);
            Assert.Equal(intValue, App.ShortKey.IntOption);
            Assert.True(App.ShortKey.BoolOption);
        }
    }
}
