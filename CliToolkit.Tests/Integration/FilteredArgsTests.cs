using CliToolkit.Tests.Integration.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CliToolkit.Tests.Integration
{
    public class FilteredArgsTests : IntegrationTestBase
    {
        [Fact]
        public void ArgFilterWithCombinedValuesTest()
        {
            var stringOption = "Wow";
            var intOption = 6;
            App.Start(new[] { "filtered-args",
                "--bool-option",
                "file-1.txt",
                $"--string-option={stringOption}",
                "file-2.txt",
                $"--int-option={intOption}",
                "file-3.txt" });

            Assert.NotNull(App.FilteredArgs);
            Assert.Equal(App.FilteredArgs.StringOption, stringOption);
            Assert.Equal(App.FilteredArgs.IntOption, intOption);
            Assert.True(App.FilteredArgs.BoolOption);
        }

        [Fact]
        public void ArgFilterWithDiscreetValuesTest()
        {
            var stringOption = "Wow";
            var intOption = 6;
            App.Start(new[] { "filtered-args",
                "--bool-option",
                "file-1.txt",
                "--string-option",
                stringOption,
                "file-2.txt",
                "--int-option",
                intOption.ToString(),
                "file-3.txt" });

            Assert.NotNull(App.FilteredArgs);
            Assert.Equal(App.FilteredArgs.StringOption, stringOption);
            Assert.Equal(App.FilteredArgs.IntOption, intOption);
            Assert.True(App.FilteredArgs.BoolOption);
        }
    }
}
