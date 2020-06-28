using CliToolkit.TestApp;
using System;
using Xunit;

namespace CliToolkit.Tests
{
    public class RuntimeErrorTests
    {
        [Fact]
        public void DefaultActionTest()
        {
            var app = new CliAppBuilder<ApplicationRoot>()
                .Build();

            Assert.ThrowsAny<Exception>(() => app.Start(new[] { "error", "--ignore-error=false" }));
        }

        [Fact]
        public void IgnoreErrorFlagTest()
        {
            var app = new CliAppBuilder<ApplicationRoot>()
                .Start(new[] { "error", "--ignore-error=true" });

            Assert.NotNull(app.Error);
            Assert.True(app.Error.IgnoreError) ;
        }
    }
}
