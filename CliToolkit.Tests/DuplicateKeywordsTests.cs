using CliToolkit.Exceptions;
using CliToolkit.Tests.Templates.DuplicateKeywords;
using Xunit;
using static CliToolkit.Tests.Templates.DuplicateKeywords.Properties;

namespace CliToolkit.Tests
{
    public class DuplicateKeywordsTests
    {
        #region Flags

        [Fact]
        public void DuplicateDoubleDashFlagKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateDoubleDashFlagKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateDoubleDashFlagShortKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateDoubleDashFlagShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateSingleDashFlagKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateSingleDashFlagKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateSingleDashFlagShortKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateSingleDashFlagShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateMsBuildFlagKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateMsBuildFlagKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateMsBuildFlagShortKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateMsBuildFlagShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        #endregion
        
        #region Properties

        [Fact]
        public void DuplicateDoubleDashPropertyKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateDoubleDashPropertyKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }
        
        [Fact]
        public void DuplicateDoubleDashPropertyShortKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateDoubleDashPropertyShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateDoubleDashWithEqualPropertyKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateDoubleDashWithEqualPropertyKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateDoubleDashWithEqualShortPropertyKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateDoubleDashWithEqualPropertyShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateSingleDashPropertyKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateSingleDashPropertyKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateSingleDashPropertyShortKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateSingleDashPropertyShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }
        
        [Fact]
        public void DuplicateSingleDashWithEqualPropertyKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateSingleDashWithEqualPropertyKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }
        
        [Fact]
        public void DuplicateSingleDashWithEqualPropertyShortKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateSingleDashWithEqualPropertyShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateMsBuildPropertyKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateMsBuildPropertyKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        [Fact]
        public void DuplicateMsBuildPropertyShortKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateMsBuildPropertyShortKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        #endregion

        #region Commands

        [Fact]
        public void DuplicateCommandKeyword_ShouldThrowAppConfigurationException()
        {
            var app = new DuplicateCommandKeywordApp();
            Assert.Throws<AppConfigurationException>(() => app.Start(new string[0]));
        }

        #endregion
    }
}