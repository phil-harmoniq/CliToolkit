using CliToolkit.Exceptions;
using CliToolkit.Tests.Templates;
using Xunit;

namespace CliToolkit.Tests
{
    public class ValidBuilderCallsTests
    {
        [Fact]
        public void EmptyNameString_ShouldThrowAppConfigurationException()
        {
            var builder = new AppBuilder<ValidApp>();
            Assert.Throws<AppConfigurationException>(() => builder.SetName(""));
        }

        [Fact]
        public void EmptyVersionString_ShouldThrowAppConfigurationException()
        {
            var builder = new AppBuilder<ValidApp>();
            Assert.Throws<AppConfigurationException>(() => builder.SetVersion(""));
        }

        [Fact]
        public void EmptyHeaderString_ShouldThrowAppConfigurationException()
        {
            var builder = new AppBuilder<ValidApp>();
            Assert.Throws<AppConfigurationException>(() => builder.SetHeader(""));
        }

        [Fact]
        public void EmptyFooterString_ShouldThrowAppConfigurationException()
        {
            var builder = new AppBuilder<ValidApp>();
            Assert.Throws<AppConfigurationException>(() => builder.SetHeader("Non-empty header", ""));
        }
    }
}