using System;

namespace CliToolkit
{
    public class CliMenuAttribute : Attribute
    {
        public string Description { get; }
        public char ShortFlag { get; }

        public CliMenuAttribute(string description, char shortFlag = default)
        {
            Description = description;
            ShortFlag = shortFlag;
        }
    }
}
