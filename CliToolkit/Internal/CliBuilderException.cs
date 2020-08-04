using System;

namespace CliToolkit.Internal
{
    internal class CliBuilderException : Exception
    {
        internal CliBuilderException(string message) : base(message) { }
    }
}
