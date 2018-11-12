using System;
using System.Collections.Generic;
using System.Linq;

namespace CliToolkit.Arguments.Styles
{
    /// <summary>
    /// Controls a Flag argument's style for parsing.
    /// </summary>
    public class FlagStyle : Style, IEquatable<FlagStyle>
    {
        internal const string DoubleDashValue = "DoubleDash";
        internal const string SingleDashValue = "SingleDash";
        internal const string MsBuildValue = "MsBuild";

        /// <summary>
        /// Use a double dash before a keyword to triger it. A single dash is still used for short keywords.
        /// </summary>
        /// <example>Example: --keyord or -k</example>
        public static readonly FlagStyle DoubleDash = new FlagStyle(DoubleDashValue);

        /// <summary>
        /// Use a single dash before a keyword to trigger it. A single dash is also used for short keywwrds
        /// </summary>
        /// <example>Example: -keyword or -k</example>
        public static readonly FlagStyle SingleDash = new FlagStyle(SingleDashValue);

        /// <summary>
        /// Use a forward slash before a keyword to trigger it. A forward slash is also used for short keywords
        /// </summary>
        /// <example>Example: /keyword or /k</example>
        public static readonly FlagStyle MsBuild = new FlagStyle(MsBuildValue);

        private FlagStyle(string style) : base(style, new string[] { DoubleDashValue, SingleDashValue, MsBuildValue }) {}

        /// <summary>
        /// Implicitly converts a string representation of a style to a FlagStyle object.
        /// </summary>
        public static implicit operator FlagStyle(string flagStyle)
        {
            return new FlagStyle(flagStyle);
        }

        /// <summary>
        /// Determines if the given style type matches this FlagStyle.
        /// </summary>
        public bool Equals(FlagStyle style)
        {
            return Value == style.Value;
        }

        internal override string GetPrefix(bool isShortValue)
        {
            if (isShortValue)
            {
                switch (Value)
                {
                    case FlagStyle.DoubleDashValue:
                        return "-";
                    case FlagStyle.SingleDashValue:
                        return "-";
                    case FlagStyle.MsBuildValue:
                        return "/";
                    default:
                        return "--";
                }
            }
            else
            {
                switch (Value)
                {
                    case FlagStyle.DoubleDashValue:
                        return "--";
                    case FlagStyle.SingleDashValue:
                        return "-";
                    case FlagStyle.MsBuildValue:
                        return "/";
                    default:
                        return "--";
                }
            }
        }
    }
}