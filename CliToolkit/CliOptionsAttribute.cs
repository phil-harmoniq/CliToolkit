using System;

namespace CliToolkit
{
    public class CliOptionsAttribute : Attribute
    {
        public char ShortKey { get; set; }
        public string Description { get; set; }
        public string Namespace { get; set; }
    }
}
