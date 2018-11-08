using CliToolkit.Exceptions;
using CliToolkit.Tests.Templates.DuplicateKeywords;
using Xunit;
using static CliToolkit.Tests.Templates.DuplicateKeywords.Properties;

namespace CliToolkit.Tests
{
    public class DuplicateKeywordsTests
    {
        // Flags
        [Fact]
        public void DuplicateDoubleDashFlagKeyword()
        {
            var app = new DuplicateDoubleDashFlagKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateDoubleDashFlagShortKeyword()
        {
            var app = new DuplicateDoubleDashFlagShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateSingleDashFlagKeyword()
        {
            var app = new DuplicateSingleDashFlagKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateSingleDashFlagShortKeyword()
        {
            var app = new DuplicateSingleDashFlagShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateMsBuildFlagKeyword()
        {
            var app = new DuplicateMsBuildFlagKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateMsBuildFlagShortKeyword()
        {
            var app = new DuplicateMsBuildFlagShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }
        

        // Properties
        [Fact]
        public void DuplicateDoubleDashPropertyKeyword()
        {
            var app = new DuplicateDoubleDashPropertyKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }
        
        [Fact]
        public void DuplicateDoubleDashPropertyShortKeyword()
        {
            var app = new DuplicateDoubleDashPropertyShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateDoubleDashWithEqualPropertyKeyword()
        {
            var app = new DuplicateDoubleDashWithEqualPropertyKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateDoubleDashWithEqualShortPropertyKeyword()
        {
            var app = new DuplicateDoubleDashWithEqualPropertyShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateSingleDashPropertyKeyword()
        {
            var app = new DuplicateSingleDashPropertyKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateSingleDashPropertyShortKeyword()
        {
            var app = new DuplicateSingleDashPropertyShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }
        
        [Fact]
        public void DuplicateSingleDashWithEqualPropertyKeyword()
        {
            var app = new DuplicateSingleDashWithEqualPropertyKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }
        
        [Fact]
        public void DuplicateSingleDashWithEqualPropertyShortKeyword()
        {
            var app = new DuplicateSingleDashWithEqualPropertyShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateMsBuildPropertyKeyword()
        {
            var app = new DuplicateMsBuildPropertyKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateMsBuildPropertyShortKeyword()
        {
            var app = new DuplicateMsBuildPropertyShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }
    }
}