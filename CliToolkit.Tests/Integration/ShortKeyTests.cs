using CliToolkit.Tests.Integration.Abstract;
using Xunit;

namespace CliToolkit.Tests.Integration
{
    public class ShortKeyTests : IntegrationTestBase
    {
        [Fact]
        public void ValidShortKeyTest()
        {
            var stringValue = "String value";
            var intValue = 5;

            App.Start(new[] {
                "short-key",
                "-s", stringValue,
                $"-i={intValue}",
                "-b"
            });

            Assert.NotNull(App.ShortKey);
            Assert.Equal(stringValue, App.ShortKey.StringOption);
            Assert.Equal(intValue, App.ShortKey.IntOption);
            Assert.True(App.ShortKey.BoolOption);
        }

        [Fact]
        public void DuplicateShortKeyTest()
        {
            App.Start(new[] { "short-key-duplicate" });

            Assert.True(App.ExitCode > 0);
        }
    }
}
