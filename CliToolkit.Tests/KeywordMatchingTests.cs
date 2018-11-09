using CliToolkit.Tests.Templates;
using Xunit;

namespace CliToolkit.Tests
{
    public class KeywordMatchingTests
    {
        public const string ValueString = "ValueString";

        #region Flags

        [Fact]
        public void DoubleDashFlagKeyword_ShouldBeActive()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"--{ValidApp.DoubleDashFlagKeyword}" });
            Assert.True(app.DoubleDashFlag.IsActive);
        }

        [Fact]
        public void DoubleDashFlagShortKeyword_ShouldBeActive()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.DoubleDashFlagShortKeyword}" });
            Assert.True(app.DoubleDashFlag.IsActive);
        }

        [Fact]
        public void SingleDashFlagKeyword_ShouldBeActive()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.SingleDashFlagKeyword}" });
            Assert.True(app.SingleDashFlag.IsActive);
        }

        [Fact]
        public void SingleDashFlagShortKeyword_ShouldBeActive()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.SingleDashFlagShortKeyword}" });
            Assert.True(app.SingleDashFlag.IsActive);
        }

        [Fact]
        public void MsBuildFlagKeyword_ShouldBeActive()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"/{ValidApp.MsBuildFlagKeyword}" });
            Assert.True(app.MsBuildFlag.IsActive);
        }

        [Fact]
        public void MsBuildFlagShortKeyword_ShouldBeActive()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"/{ValidApp.MsBuildFlagShortKeyword}" });
            Assert.True(app.MsBuildFlag.IsActive);
        }

        #endregion

        #region Properties
        
        [Fact]
        public void DoubleDashPropertyKeyword_ShouldBeActiveAndContainCorrectValue()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"--{ValidApp.DoubleDashPropertyKeyword}", ValueString });
            Assert.True(app.DoubleDashProperty.IsActive);
            Assert.Equal(app.DoubleDashProperty.Value, ValueString);
        }
        
        [Fact]
        public void DoubleDashPropertyShortKeyword_ShouldBeActiveAndContainCorrectValue()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.DoubleDashPropertyShortKeyword}", ValueString });
            Assert.True(app.DoubleDashProperty.IsActive);
            Assert.Equal(app.DoubleDashProperty.Value, ValueString);
        }

        [Fact]
        public void DoubleDashWithEqualPropertyKeyword_ShouldBeActiveAndContainCorrectValue()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"--{ValidApp.DoubleDashWithEqualPropertyKeyword}={ValueString}" });
            Assert.True(app.DoubleDashWithEqualProperty.IsActive);
            Assert.Equal(app.DoubleDashWithEqualProperty.Value, ValueString);
        }

        [Fact]
        public void DoubleDashWithEqualPropertyShortKeyword_ShouldBeActiveAndContainCorrectValue()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.DoubleDashWithEqualPropertyShortKeyword}={ValueString}" });
            Assert.True(app.DoubleDashWithEqualProperty.IsActive);
            Assert.Equal(app.DoubleDashWithEqualProperty.Value, ValueString);
        }
        
        [Fact]
        public void SingleDashPropertyKeyword_ShouldBeActiveAndContainCorrectValue()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.SingleDashPropertyKeyword}", ValueString });
            Assert.True(app.SingleDashProperty.IsActive);
            Assert.Equal(app.SingleDashProperty.Value, ValueString);
        }
        
        [Fact]
        public void SingleDashPropertyShortKeyword_ShouldBeActiveAndContainCorrectValue()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.SingleDashPropertyShortKeyword}", ValueString });
            Assert.True(app.SingleDashProperty.IsActive);
            Assert.Equal(app.SingleDashProperty.Value, ValueString);
        }

        [Fact]
        public void SingleDashWithEqualPropertyKeyword_ShouldBeActiveAndContainCorrectValue()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.SingleDashWithEqualPropertyKeyword}={ValueString}" });
            Assert.True(app.SingleDashWithEqualProperty.IsActive);
            Assert.Equal(app.SingleDashWithEqualProperty.Value, ValueString);
        }

        [Fact]
        public void SingleDashWithEqualPropertyShortKeyword_ShouldBeActiveAndContainCorrectValue()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"-{ValidApp.SingleDashWithEqualPropertyShortKeyword}={ValueString}" });
            Assert.True(app.SingleDashWithEqualProperty.IsActive);
            Assert.Equal(app.SingleDashWithEqualProperty.Value, ValueString);
        }
        
        [Fact]
        public void MsBuildPropertyKeyword_ShouldBeActiveAndContainCorrectValue()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"/{ValidApp.MsBuildPropertyKeyword}={ValueString}" });
            Assert.True(app.MsBuildProperty.IsActive);
            Assert.Equal(app.MsBuildProperty.Value, ValueString);
        }
        
        [Fact]
        public void MsBuildPropertyShortKeyword_ShouldBeActiveAndContainCorrectValue()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { $"/{ValidApp.MsBuildPropertyShortKeyword}={ValueString}" });
            Assert.True(app.MsBuildProperty.IsActive);
            Assert.Equal(app.MsBuildProperty.Value, ValueString);
        }
        
        #endregion

        #region Commands
        
        [Fact]
        public void CommandKeyword_ShouldBeActiveAndContainCorrectValue()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { ValidApp.DefaultCommandKeyword });
            Assert.True(app.DefaultCommand.IsActive);
        }

        #endregion
    }
}