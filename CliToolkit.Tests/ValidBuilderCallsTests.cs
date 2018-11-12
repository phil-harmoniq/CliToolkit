using CliToolkit.Exceptions;
using CliToolkit.Tests.Templates;
using Xunit;

namespace CliToolkit.Tests
{
    public class ValidBuilderCallsTests
    {
        public const string CustomAppName = "CustomAppName";
        public const string CustomVersionString = "2.1.0";
        public const int CustomWidth = 72;

        [Fact]
        public void SetName_ShouldOverwriteAppName()
        {
            var app = new AppBuilder<ValidApp>()
                .SetName(CustomAppName)
                .Build();
            
            Assert.Equal(app.AppInfo.Name, CustomAppName);
        }

        [Fact]
        public void SetVersion_ShouldOverwriteAppVersionString()
        {
            var app = new AppBuilder<ValidApp>()
                .SetVersion(CustomVersionString)
                .Build();
            
            Assert.Equal(app.AppInfo.Version, CustomVersionString);
        }

        [Fact]
        public void SetWidth_ShouldOverwriteAppWidth()
        {
            var app = new AppBuilder<ValidApp>()
                .SetWidth(CustomWidth)
                .Build();
            
            Assert.Equal(app.AppInfo.Width, CustomWidth);
        }
    }
}