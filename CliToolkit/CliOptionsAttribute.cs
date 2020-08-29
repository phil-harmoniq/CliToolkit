using CliToolkit.Internal;
using System;

namespace CliToolkit
{
    /// <summary>
    /// Assign custom meta-data for a command/option type or property.
    /// </summary>
    public class CliOptionsAttribute : Attribute
    {
        private char _shortKey;

        /// <summary>
        /// A single character that will trigger this option using a single dash.
        /// </summary>
        public char ShortKey
        {
            get => _shortKey;
            set
            {
                if (value.IsValidShortKey()) { _shortKey = value; }
                else { throw new CliAppBuilderException($"Invalid short-key: {value}"); }
            }
        }

        /// <summary>
        /// The description for the command or option to be displayed in the help menu.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A custom namespace for the configuration section
        /// </summary>
        public string Namespace { get; set; }

        public CliOptionsAttribute() { }

        public CliOptionsAttribute(string description)
        {
            Description = description;
        }

        public CliOptionsAttribute(string description, char shortKey)
        {
            Description = description;
            ShortKey = shortKey;
        }
    }
}
