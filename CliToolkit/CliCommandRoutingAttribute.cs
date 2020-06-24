using System;

namespace CliToolkit
{
    public class CliCommandRouteAttribute : Attribute
    {
        public string Keyword { get; }
        public string AlternateKeyword { get; }
        public bool HasShortKeyword { get; }

        public CliCommandRouteAttribute(string keyword, string alternateKeyword = null)
        {
            Keyword = keyword;
            AlternateKeyword = alternateKeyword;
            HasShortKeyword = string.IsNullOrEmpty(AlternateKeyword);
        }
    }
}
