using System;
using System.Collections.Generic;
using System.Text;

namespace CliToolkit.Utilities
{
    internal class IgnoreCaseComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y) => x.Equals(y, StringComparison.OrdinalIgnoreCase);
        public int GetHashCode(string obj) => throw new NotImplementedException();
    }
}
