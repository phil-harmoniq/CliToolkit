using CliToolkit.Exceptions;
using CliToolkit.Tests.Templates;
using Xunit;

namespace CliToolkit.Tests
{
    public class ValidBuilderCallsTests
    {
        public const string CustomeAppName = "CustomAppName";

        [Fact]
        public void EmptyNameString_ShouldThrowAppConfigurationException()
        {
            var app = new AppBuilder<ValidApp>()
                .SetName(CustomeAppName)
                .Build();
            
            Assert.Equal(app.AppInfo.Name = CustomeAppName);
        }

        [Fact]
        public void EmptyVersionString_ShouldThrowAppConfigurationException()
        {
            var builder = new AppBuilder<ValidApp>();
            Assert.Throws<AppConfigurationException>(() => builder.SetVersion(""));
        }
    }
}