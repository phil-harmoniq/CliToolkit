using CliToolkit.Tests.Integration.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CliToolkit.Tests.Integration
{
    public class ApplicationRootTests : IntegrationTestBase
    {
        [Fact]
        public void HelpMenu()
        {
            App.Start(new[] { "--help" });
        }
    }
}
