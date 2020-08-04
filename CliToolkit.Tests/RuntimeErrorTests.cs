using CliToolkit.TestApp;
using Xunit;

namespace CliToolkit.Tests
{
    public class RuntimeErrorTests
    {
        [Fact]
        public void DefaultActionTest()
        {
            var app = new CliAppBuilder<ApplicationRoot>()
                .Start(new[] { "error" });

            Assert.Equal(5, app.ExitCode);
        }

        [Fact]
        public void IgnoreErrorFlagTest()
        {
            var app = new CliAppBuilder<ApplicationRoot>()
                .Start(new[] { "error", "--ignore-error=true" });

            Assert.NotNull(app.Error);
            Assert.True(app.Error.IgnoreError);
        }
    }
}
