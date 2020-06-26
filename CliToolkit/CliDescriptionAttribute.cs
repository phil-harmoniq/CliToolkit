using System;

namespace CliToolkit
{
    public class CliDescriptionAttribute : Attribute
    {
        public string Description { get; }

        public CliDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
