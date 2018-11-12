using CliToolkit.Exceptions;
using CliToolkit.Tests.Templates;
using Xunit;

namespace CliToolkit.Tests
{
    public class InvalidBuilderCallsTests
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
        public void WidthSmallerThanMinimum_ShouldThrowAppConfigurationException()
        {
            var builder = new AppBuilder<ValidApp>();
            Assert.Throws<AppConfigurationException>(() => builder.SetWidth(12));
        }

        [Fact]
        public void WidthLargerThanMaximum_ShouldThrowAppConfigurationException()
        {
            var builder = new AppBuilder<ValidApp>();
            Assert.Throws<AppConfigurationException>(() => builder.SetWidth(256));
        }
    }
}