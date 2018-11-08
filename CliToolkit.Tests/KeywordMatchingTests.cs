using CliToolkit.Tests.Templates;
using Xunit;

namespace CliToolkit.Tests
{
    public class KeywordMatchingTests
    {
        public static readonly string ValueString = "ValueString";

        #region Flags

        [Fact]
        public void DoubleDashFlagKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"--{ValidApp.DoubleDashFlagKeyword}" });
            Assert.True(app.DoubleDashFlag.IsActive);
        }

        [Fact]
        public void DoubleDashFlagShortKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.DoubleDashFlagShortKeyword}" });
            Assert.True(app.DoubleDashFlag.IsActive);
        }

        [Fact]
        public void SingleDashFlagKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.SingleDashFlagKeyword}" });
            Assert.True(app.SingleDashFlag.IsActive);
        }

        [Fact]
        public void SingleDashFlagShortKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.SingleDashFlagShortKeyword}" });
            Assert.True(app.SingleDashFlag.IsActive);
        }

        [Fact]
        public void MsBuildFlagKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"/{ValidApp.MsBuildFlagKeyword}" });
            Assert.True(app.MsBuildFlag.IsActive);
        }

        [Fact]
        public void MsBuildFlagShortKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"/{ValidApp.MsBuildFlagShortKeyword}" });
            Assert.True(app.MsBuildFlag.IsActive);
        }

        #endregion

        #region Properties
        
        [Fact]
        public void DoubleDashPropertyKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"--{ValidApp.DoubleDashPropertyKeyword}", ValueString });
            Assert.True(app.DoubleDashProperty.IsActive);
            Assert.Equal(app.DoubleDashProperty.Value, ValueString);
        }
        
        [Fact]
        public void DoubleDashPropertyShortKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.DoubleDashPropertyShortKeyword}", ValueString });
            Assert.True(app.DoubleDashProperty.IsActive);
        }

        [Fact]
        public void DoubleDashWithEqualPropertyKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"--{ValidApp.DoubleDashWithEqualPropertyKeyword}={ValueString}" });
            Assert.True(app.DoubleDashWithEqualProperty.IsActive);
        }

        [Fact]
        public void DoubleDashWithEqualPropertyShortKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.DoubleDashWithEqualPropertyShortKeyword}={ValueString}" });
            Assert.True(app.DoubleDashWithEqualProperty.IsActive);
        }
        
        [Fact]
        public void SingleDashPropertyKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.SingleDashPropertyKeyword}", ValueString });
            Assert.True(app.SingleDashProperty.IsActive);
        }
        
        [Fact]
        public void SingleDashPropertyShortKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.SingleDashPropertyShortKeyword}", ValueString });
            Assert.True(app.SingleDashProperty.IsActive);
        }

        [Fact]
        public void SingleDashWithEqualPropertyKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.SingleDashWithEqualPropertyKeyword}={ValueString}" });
            Assert.True(app.SingleDashWithEqualProperty.IsActive);
        }

        [Fact]
        public void SingleDashWithEqualPropertyShortKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.SingleDashWithEqualPropertyShortKeyword}={ValueString}" });
            Assert.True(app.SingleDashWithEqualProperty.IsActive);
        }
        
        [Fact]
        public void MsBuildPropertyKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"/{ValidApp.MsBuildPropertyKeyword}={ValueString}" });
            Assert.True(app.MsBuildProperty.IsActive);
        }
        
        [Fact]
        public void MsBuildPropertyShortKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"/{ValidApp.MsBuildPropertyShortKeyword}={ValueString}" });
            Assert.True(app.MsBuildProperty.IsActive);
        }
        
        #endregion
    }
}