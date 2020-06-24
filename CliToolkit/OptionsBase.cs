using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CliToolkit
{
    public class OptionsBase
    {
        internal Dictionary<string, string> GetSwitchMappings()
        {
            var type = GetType();
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead)
                .ToDictionary(p => p.Name, p => $"{type.Name}:{p.Name}");
        }
    }
}
