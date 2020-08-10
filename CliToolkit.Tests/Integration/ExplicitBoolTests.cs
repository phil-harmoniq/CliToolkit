using CliToolkit.Tests.Integration.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CliToolkit.Tests.Integration
{
    public class ExplicitBoolTests : IntegrationTestBase
    {
        [Fact]
        public void BoolTest()
        {
            App.Start(new[] { "explicit-bool", "--implicit-bool", "--explicit-bool", "false" });

            Assert.NotNull(App.ExplicitBool);
            Assert.True(App.ExplicitBool.ImplicitBool);
            Assert.False(App.ExplicitBool.ExplicitBool);
        }
    }
}
