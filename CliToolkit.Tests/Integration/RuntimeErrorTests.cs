using CliToolkit.Tests.Integration.Abstract;
using Xunit;

namespace CliToolkit.Tests.Integration
{
    public class RuntimeErrorTests : IntegrationTestBase
    {
        [Fact]
        public void DefaultActionTest()
        {
            App.Start(new[] { "error" });

            Assert.Equal(5, App.ExitCode);
        }

        [Fact]
        public void IgnoreErrorFlagTest()
        {
            App.Start(new[] { "error", "--ignore-error=true" });

            Assert.NotNull(App.Error);
            Assert.True(App.Error.IgnoreError);
            Assert.True(App.ExitCode == 0);
        }
    }
}
