using System;
using CliToolkit.Arguments;
using CliToolkit.Arguments.Styles;

namespace CliToolkit.Tests.Templates
{
    public class ValidApp : CliApp
    {
        public static readonly string DoubleDashFlagKeyword = "DoubleDashFlag";
        public static readonly char DoubleDashFlagShortKeyword = 'd';
        public Flag DoubleDashFlag { get; } = new Flag("Double-dash flag style", DoubleDashFlagKeyword, DoubleDashFlagShortKeyword, FlagStyle.DoubleDash);
        
        public static readonly string SingleDashFlagKeyword = "SingleDashFlag";
        public static readonly char SingleDashFlagShortKeyword = 's';
        public Flag SingleDashFlag { get; } = new Flag("Single-dash flag style", SingleDashFlagKeyword, SingleDashFlagShortKeyword, FlagStyle.SingleDash);
        
        public static readonly string MsBuildFlagKeyword = "MsBuildDashFlag";
        public static readonly char MsBuildFlagShortKeyword = 'm';
        public Flag MsBuildFlag { get; } = new Flag("MsBuild flag style", MsBuildFlagKeyword, MsBuildFlagShortKeyword, FlagStyle.MsBuild);

        
        public static readonly string DoubleDashPropertyKeyword = "DoubleDashProperty";
        public static readonly char DoubleDashPropertyShortKeyword = 'D';
        public Property DoubleDashProperty = new Property("Double-dash property style", DoubleDashPropertyKeyword, DoubleDashPropertyShortKeyword, PropertyStyle.DoubleDash);
        
        public static readonly string DoubleDashWithEqualPropertyKeyword = "DoubleDashWithEqualProperty";
        public static readonly char DoubleDashWithEqualPropertyShortKeyword = 'E';
        public Property DoubleDashWithEqualProperty = new Property("Double-dash property style", DoubleDashWithEqualPropertyKeyword, DoubleDashWithEqualPropertyShortKeyword, PropertyStyle.DoubleDashWithEqual);
        
        public static readonly string SingleDashPropertyKeyword = "SingleDashProperty";
        public static readonly char SingleDashPropertyShortKeyword = 'S';
        public Property SingleDashProperty = new Property("Single-dash property style", SingleDashPropertyKeyword, SingleDashPropertyShortKeyword, PropertyStyle.SingleDash);
        
        public static readonly string SingleDashWithEqualPropertyKeyword = "SingleDashWithEqualProperty";
        public static readonly char SingleDashWithEqualPropertyShortKeyword = 'W';
        public Property SingleDashWithEqualProperty = new Property("Single-dash property style", SingleDashWithEqualPropertyKeyword, SingleDashWithEqualPropertyShortKeyword, PropertyStyle.SingleDashWithEqual);
        
        public static readonly string MsBuildPropertyKeyword = "MsBuildProperty";
        public static readonly char MsBuildPropertyShortKeyword = 'M';
        public Property MsBuildProperty = new Property("MsBuild property style", MsBuildPropertyKeyword, MsBuildPropertyShortKeyword, PropertyStyle.MsBuild);

        public static readonly string CommandKeyword = "SubCommand";
        public Command SubCommand = new ValidSubCommand("Subcommand default style", CommandKeyword);

        public override void OnExecute(string[] args)
        {
            Console.WriteLine("This application should execute without any runtime or configuration errors.");
        }
    }
}