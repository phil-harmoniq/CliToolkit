using CliToolkit.Exceptions;
using CliToolkit.Tests.Templates;
using Xunit;

namespace CliToolkit.Tests.ConfigurationExceptions
{
    public class InvalidBuilderCallsTests
    {
        [Fact]
        public void EmptyNameStringThrowsAppConfigurationException()
        {
            var builder = new AppBuilder<ValidApp>();
            Assert.Throws<AppConfigurationException>(() => builder.SetName(""));
        }

        [Fact]
        public void EmptyVersionStringThrowsAppConfigurationException()
        {
            var builder = new AppBuilder<ValidApp>();
            Assert.Throws<AppConfigurationException>(() => builder.SetVersion(""));
        }

        [Fact]
        public void EmptyHeaderStringThrowsAppConfigurationException()
        {
            var builder = new AppBuilder<ValidApp>();
            Assert.Throws<AppConfigurationException>(() => builder.SetHeader(""));
        }

        [Fact]
        public void EmptyFooterStringThrowsAppConfigurationException()
        {
            var builder = new AppBuilder<ValidApp>();
            Assert.Throws<AppConfigurationException>(() => builder.SetHeader("Non-empty header", ""));
        }
    }
}