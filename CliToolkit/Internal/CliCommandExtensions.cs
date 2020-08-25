using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace CliToolkit.Internal
{
    internal static class CliCommandExtensions
    {
        private static readonly bool _isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        internal static IList<string> GetCommandTree(this CliCommand command, List<string> list = null)
        {
            if (list == null) { list = new List<string>(); }

            if (command.Parent == null)
            {
                var name = !command.AppSettings.CustomName
                    ? $"{command.AppSettings.Name}.exe"
                    : command.AppSettings.Name;
                list.Insert(0, _isWindows && !command.AppSettings.CustomName
                    ? $"{command.AppSettings.Name}.exe"
                    : command.AppSettings.Name);
                return list;
            }

            var matchingCommand = command.Parent.CommandProperties
                .First(prop => prop.PropertyType == command.Type);
            
            var commandName = matchingCommand.Name.KebabConvert().ToLower();
            list.Insert(0, commandName);
            return GetCommandTree(command.Parent, list);
        }
    }
}
