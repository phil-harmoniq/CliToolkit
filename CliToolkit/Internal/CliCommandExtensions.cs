using System.Collections.Generic;
using System.Linq;

namespace CliToolkit.Internal
{
    internal static class CliCommandExtensions
    {
        internal static IList<string> GetCommandTree(this CliCommand command, List<string> list = null)
        {
            if (list == null) { list = new List<string>(); }

            if (command.Parent == null)
            {
                list.Insert(0, command.AppSettings.Name.ToLower());
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
