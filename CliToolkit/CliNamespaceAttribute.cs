using System;

namespace CliToolkit
{
    public class CliNamespaceAttribute : Attribute
    {
        public string Namespace { get; }

        public CliNamespaceAttribute(string name)
        {
            Namespace = name;
        }
    }
}
