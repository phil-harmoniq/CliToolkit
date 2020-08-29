using CliToolkit.Tests.Integration.Abstract;
using Xunit;

namespace CliToolkit.Tests.Integration
{
    public class ArgFilterTests : IntegrationTestBase
    {
        [Fact]
        public void CombinedValuesTest()
        {
            var file1 = "file-1.txt";
            var file2 = "file-2.txt";
            var file3 = "file-3.txt";
            var stringOption = "Wow";
            var intOption = 6;

            App.Start(new[] { "arg-filter",
                "--bool-option",
                file1,
                $"--string-option={stringOption}",
                file2,
                $"--int-option={intOption}",
                file3 });

            Assert.NotNull(App.ArgFilter);
            Assert.Equal(App.ArgFilter.StringOption, stringOption);
            Assert.Equal(App.ArgFilter.IntOption, intOption);
            Assert.True(App.ArgFilter.BoolOption);
            Assert.Equal(3, App.ArgFilter.Args.Length);
            Assert.Contains(file1, App.ArgFilter.Args);
            Assert.Contains(file2, App.ArgFilter.Args);
            Assert.Contains(file3, App.ArgFilter.Args);
        }

        [Fact]
        public void DiscreteValuesTest()
        {
            var file1 = "file-1.txt";
            var file2 = "file-2.txt";
            var file3 = "file-3.txt";
            var stringOption = "Wow";
            var intOption = 6;

            App.Start(new[] { "arg-filter",
                "--bool-option",
                file1,
                "--string-option",
                stringOption,
                file2,
                "--int-option",
                intOption.ToString(),
                file3 });

            Assert.NotNull(App.ArgFilter);
            Assert.Equal(App.ArgFilter.StringOption, stringOption);
            Assert.Equal(App.ArgFilter.IntOption, intOption);
            Assert.True(App.ArgFilter.BoolOption);
            Assert.Equal(3, App.ArgFilter.Args.Length);
            Assert.Contains(file1, App.ArgFilter.Args);
            Assert.Contains(file2, App.ArgFilter.Args);
            Assert.Contains(file3, App.ArgFilter.Args);
        }
    }
}
