using CliToolkit.Arguments;
using CliToolkit.Styles;

namespace CliToolkit
{
    /// <summary>
    /// A strongly-typed command line Flag argument that records if it was triggered.
    /// </summary>
    public sealed class Flag : Argument
    {
        /// <summary>
        /// A single letter that will also trigger this flag. This value is optional.
        /// </summary>
        public string ShortKeyword { get; private set; }
        
        /// <summary>
        /// Creates a new strongly-typed Flag argument.
        /// </summary>
        /// <param name="description">The text description that will be displayed in the help menu.</param>
        /// <param name="keyword">The primary keyword that will trigger this Flag argument.</param>
        /// <param name="shortKeyword">An optional single character that will also trigger this Flag argument.</param>
        /// <param name="style">Sets the particular argument style to be used to parse this Flag argument.</param>
        public Flag(string description, string keyword, char? shortKeyword = null, FlagStyle style = null)
            : base(description, keyword)
        {
            if (shortKeyword != null) { ShortKeyword = shortKeyword.ToString(); }
            if (style == null) { style = FlagStyle.DoubleDash; }
            Style = style;
        }

        internal override int IsMatchingKeyword(string[] args)
        {
            switch (Style)
            {
                case FlagStyle.DoubleDashValue:
                    return ParseDoubleDashStyleArg(args[0]);
                case FlagStyle.SingleDashValue:
                    return ParseSingleDashStyleArg(args[0]);
                case FlagStyle.MsBuildValue:
                    return ParseMsBuildStyleArg(args[0]);
            }
            return 0; 
        }

        private int ParseDoubleDashStyleArg(string arg)
        {
            if (arg == $"--{Keyword}" || (string.IsNullOrEmpty(ShortKeyword) ? false  : arg == $"-{ShortKeyword}"))
            {
                IsActive = true;
                return 1;
            }
            return 0;
        }

        private int ParseSingleDashStyleArg(string arg)
        {
            if (arg == $"-{Keyword}" || (string.IsNullOrEmpty(ShortKeyword) ? false : arg == $"-{ShortKeyword}"))
            {
                IsActive = true;
                return 1;
            }
            return 0;
        }

        private int ParseMsBuildStyleArg(string arg)
        {
            if (arg == $"/{Keyword}" || (string.IsNullOrEmpty(ShortKeyword) ? false : arg == $"/{ShortKeyword}"))
            {
                IsActive = true;
                return 1;
            }
            return 0;
        }
    }
}