using CliToolkit.TestApp;
using CliToolkit.Tests.Integration.Abstract;
using System;
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

        [Fact]
        public void DuplicateShortKeyTest()
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                App.Start(new[] { "short-key-duplicate" });
            });
        }
    }
}
