using System;
using System.Collections.Generic;
using System.Linq;

namespace CliToolkit.Arguments.Styles
{
    /// <summary>
    /// Controls a Property argument's style for parsing.
    /// </summary>
    public class PropertyStyle : Style, IEquatable<PropertyStyle>
    {
        internal const string DoubleDashValue = "DoubleDash";
        internal const string SingleDashValue = "SingleDash";
        internal const string DoubleDashWithEqualValue = "DoubleDashWithEqual";
        internal const string SingleDashWithEqualValue = "SingleDashWithEqual";
        internal const string MsBuildValue = "MsBuild";

        /// <summary>
        /// Use a double dash before a keyword to triger it. A single dash is still used for short keywords.
        /// </summary>
        /// <example>--keyord value or -k value</example>
        public static readonly PropertyStyle DoubleDash = new PropertyStyle(DoubleDashValue);
        
        /// <summary>
        /// Use a single dash before a keyword to trigger it. A single dash is also used for short keywords.
        /// </summary>
        /// <example>-keyword value or -k value</example>
        public static readonly PropertyStyle SingleDash = new PropertyStyle(SingleDashValue);

        /// <summary>
        /// Use a double dash before a keyword to trigger it. A single dash is still used for short keywords.
        /// </summary>
        /// <example>--keyword=value or -k=value</example>
        public static readonly PropertyStyle DoubleDashWithEqual = new PropertyStyle(DoubleDashWithEqualValue);

        /// <summary>
        /// Use a single dash before a keyword to triger it. A single dash is also used for short keywords.
        /// </summary>
        /// <example>-keyord=value or -k=value</example>
        public static readonly PropertyStyle SingleDashWithEqual = new PropertyStyle(SingleDashWithEqualValue);

        /// <summary>
        /// Use a forward slash before a keyword to trigger it. A forward slash is also used for short keywords
        /// </summary>
        /// <example>/keyword=value or /k=value</example>
        public static readonly PropertyStyle MsBuild = new PropertyStyle(MsBuildValue);

        private PropertyStyle(string style) : base(style, new string[] { DoubleDashValue, SingleDashValue, DoubleDashWithEqualValue, SingleDashWithEqualValue, MsBuildValue }) {}

        /// <summary>
        /// Implicitly converts a string representation of a style to a PropertyStyle object.
        /// </summary>
        public static implicit operator PropertyStyle(string propertyStyle)
        {
            return new PropertyStyle(propertyStyle);
        }

        /// <summary>
        /// Determines if the given style type matches this PropertyStyle.
        /// </summary>
        public bool Equals(PropertyStyle propertyStyle)
        {
            return Value == propertyStyle.Value;
        }
    }
}