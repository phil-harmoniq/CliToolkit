using System;
using System.Collections.Generic;
using System.Linq;

namespace CliToolkit.Styles
{
    /// <summary>
    /// A generic argument style type.
    /// </summary>
    public abstract class Style : IEquatable<Style>
    {
        /// <summary>
        /// The style value as a string.
        /// </summary>
        protected readonly string Value;
        private static IReadOnlyCollection<string> _validValues;

        internal abstract string GetPrefix(bool isShortValue);

        internal Style(string style, string[] validValues)
        {
            _validValues = validValues;

            if (!IsValid(style))
            {
                throw new ArgumentException($"The provided value {style} is not a valid FlagStyle: {string.Join(", ", _validValues)}");
            }

            Value = style;
        }

        /// <summary>
        /// Implicitly converts a Style type to its string representation.
        /// </summary>
        public static implicit operator string(Style style)
        {
            return style.Value;
        }

        /// <summary>
        /// Determines if the given style type matches this style.
        /// </summary>
        public bool Equals(Style style)
        {
            return Value == style.Value;
        }

        /// <summary>
        /// Determines if the given string matches this style's string value.
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public bool Equals(string style)
        {
            return Value == style;
        }

        private static bool IsValid(string value)
        {
            return _validValues.Contains(value, StringComparer.InvariantCulture);
        }
    }
}