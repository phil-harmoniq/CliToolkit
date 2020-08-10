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
            App.Start(new[] { "explicit-bool", "--is-active" });

            Assert.NotNull(App.ExplicitBool);
            Assert.True(App.ExplicitBool.IsActive);
        }
    }
}
