using System;
using CliToolkit.Styles;
using CliToolkit.Exceptions;
using CliToolkit.Arguments;

namespace CliToolkit
{
    /// <summary>
    /// A strongly-typed command line Property argument that stores a value.
    /// </summary>
    public sealed class Property : Argument
    {
        /// <summary>
        /// A single letter that will also trigger this property. This value is optional.
        /// </summary>
        public string ShortKeyword { get; private set; }

        /// <summary>
        /// The property value captured by this argument if triggered.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Creates a new strongly-typed Property argument.
        /// </summary>
        /// <param name="description">The text description that will be displayed in the help menu.</param>
        /// <param name="keyword">The primary keyword that will trigger this Property argument.</param>
        /// <param name="shortKeyword">An optional single character that will also trigger this Property argument.</param>
        /// <param name="style">Sets the particular argument style to be used to parse this Property argument.</param>
        public Property(string description, string keyword, char? shortKeyword = null, PropertyStyle style = null)
            : base(description, keyword)
        {
            if (shortKeyword != null) { ShortKeyword = shortKeyword.ToString(); }
            if (style == null) { style = PropertyStyle.DoubleDash; }
            Style = style;
        }

        internal override int IsMatchingKeyword(string[] args)
        {
            switch (Style)
            {
                case PropertyStyle.DoubleDashValue:
                    return ParseDoubleDashStyleArg(args[0], args[1]);
                case PropertyStyle.DoubleDashWithEqualValue:
                    return ParseDoubleDashWithEqualStyleArg(args[0]);
                case PropertyStyle.SingleDashValue:
                    return ParseSingleDashStyleArg(args[0], args[1]);
                case PropertyStyle.SingleDashWithEqualValue:
                    return ParseSingleDashWithEqualStyleArg(args[0]);
                case PropertyStyle.MsBuildValue:
                    return ParseMsBuildStyleArg(args[0]);
            }
            return 0; 
        }

        private int ParseDoubleDashStyleArg(string property, string value = null)
        {
            if (property.Equals($"--{Keyword}") || (string.IsNullOrEmpty(ShortKeyword) ? false : property.Equals($"-{ShortKeyword}")))
            {
                if (string.IsNullOrEmpty(value)) { throw new InvalidPropertyException($"The property arg '${property}' is missing a value."); }
                IsActive = true;
                Value = value;
                return 2;
            }

            return 0;
        }

        private int ParseDoubleDashWithEqualStyleArg(string arg)
        {
            if (arg.StartsWith($"--{Keyword}") || (string.IsNullOrEmpty(ShortKeyword) ? false : (arg.StartsWith($"-{ShortKeyword}") && arg.Contains("="))))
            {
                var split = SplitAndValidatePropertyArgs(arg);
                Value = split[1];
                IsActive = true;
                return 1;
            }

            return 0;
        }

        private int ParseSingleDashStyleArg(string property, string value = null)
        {
            if (property.Equals($"-{Keyword}") || (string.IsNullOrEmpty(ShortKeyword) ? false : property.Equals($"-{ShortKeyword}")))
            {
                if (string.IsNullOrEmpty(value)) { throw new InvalidPropertyException($"The property arg '${property}' is missing a value."); }
                IsActive = true;
                Value = value;
                return 2;
            }

            return 0;
        }

        private int ParseSingleDashWithEqualStyleArg(string arg)
        {
            if (arg.StartsWith($"-{Keyword}") || (string.IsNullOrEmpty(ShortKeyword) ? false : (arg.StartsWith($"-{ShortKeyword}") && arg.Contains("="))))
            {
                var split = SplitAndValidatePropertyArgs(arg);
                Value = split[1];
                IsActive = true;
                return 1;
            }

            return 0;
        }

        private int ParseMsBuildStyleArg(string arg)
        {
            if (arg.StartsWith($"/{Keyword}") || (string.IsNullOrEmpty(ShortKeyword) ? false : (arg.StartsWith($"/{ShortKeyword}") && arg.Contains("="))))
            {
                var split = SplitAndValidatePropertyArgs(arg);
                Value = split[1];
                IsActive = true;
                return 1;
            }

            return 0;
        }

        private string[] SplitAndValidatePropertyArgs(string arg)
        {
            var split = arg.Split(new char[] {'='}, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length < 2)
            {
                throw new InvalidPropertyException($"The property '${arg}' does not contain a value defined by '='.");
            }
            if (split.Length > 2)
            {
                throw new InvalidPropertyException($"The value of property '${arg}' cannot contain extra equal signs.");
            }
            
            return split;
        }
    }
}