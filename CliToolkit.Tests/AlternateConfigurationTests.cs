﻿using CliToolkit.TestApp;
using CliToolkit.TestApp.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CliToolkit.Tests
{
    public class AlternateConfigurationTests
    {
        [Fact]
        public void DefaultActionTest()
        {
            var app = new CliAppBuilder<ApplicationRoot>()
                .Configure(c => c.AddJsonFile("appsettings.json"))
                .RegisterServices(Register)
                .Start(new[] { "alternate", "--AlternateConfigurationOptions:StringFromConsole=this-is-console" });

            Assert.Equal("this-is-console", app.Alternate.Options.StringFromConsole);
            Assert.Equal("this-is-json", app.Alternate.Options.StringFromJson);
        }

        private void Register(IServiceCollection sc, IConfiguration config)
        {
            sc.AddConfig<AlternateConfigurationOptions>(config);
            sc.AddConfig<CliOptions>(config);
        }
    }
}
