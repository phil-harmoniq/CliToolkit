using CliToolkit.Tests.Integration.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CliToolkit.Tests.Integration
{
    public class DefaultHelpMenuTests : IntegrationTestBase
    {
        [Fact]
        public void WithHelpFlagWontThrow()
        {
            App.Start(new[] { "help-menu", "--help" });
            Assert.Equal(0, App.ExitCode);
        }

        [Fact]
        public void WithoutHelpFlagWillThrow()
        {
            App.Start(new[] { "help-menu" });
            Assert.False(App.ExitCode == 0);
        }
    }
}
