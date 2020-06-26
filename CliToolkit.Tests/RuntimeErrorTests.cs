using CliToolkit.TestApp;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using Xunit;

namespace CliToolkit.Tests
{
    public class RuntimeErrorTests
    {
        [Fact]
        public void Test1()
        {
            var app = new CliAppBuilder<ApplicationRoot>()
                .Configure(c => c.AddJsonFile("appsettings.json"))
                //.RegisterServices(RegisterServices)
                .Start(new[] { "error" });

            Assert.NotNull(app.ErrorCommand);
        }
    }
}
