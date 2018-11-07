using CliToolkit.Tests.Templates;
using Xunit;

namespace CliToolkit.Tests
{
    public class KeywordMatchingTests
    {
        [Fact]
        public void DoubleDashFlagKeywordTest()
        {
            var app = new AppBuilder<ValidApp>().Start(new string[] { "/" });
        }
    }
}