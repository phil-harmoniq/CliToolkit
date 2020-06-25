using System;

namespace CliToolkit
{
    public class CliDescriptionAttribute : Attribute
    {
        public string Description { get; }
        public char ShortFlag { get; }

        public CliDescriptionAttribute(string description, char shortFlag = default)
        {
            Description = description;
            ShortFlag = shortFlag;
        }
    }
}
