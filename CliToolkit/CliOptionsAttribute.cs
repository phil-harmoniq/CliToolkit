using CliToolkit.Internal;
using System;

namespace CliToolkit
{
    public class CliOptionsAttribute : Attribute
    {
        private char _shortKey;

        public char ShortKey
        {
            get => _shortKey;
            set
            {
                if (value.IsValidShortKey()) { _shortKey = value; }
                else { throw new CliAppBuilderException($"Invalid short-key: {value}"); }
            }
        }

        public string Description { get; set; }
        public string Namespace { get; set; }
    }
}
