using System;
using CliToolkit.Arguments;
using CliToolkit.Arguments.Styles;

namespace CliToolkit.Tests.Templates
{
    public class ValidApp : CliApp
    {
        #region Flags

        public const string DoubleDashFlagKeyword = "DoubleDashFlag";
        public const char DoubleDashFlagShortKeyword = 'd';
        public Flag DoubleDashFlag = new Flag("Double-dash flag style", DoubleDashFlagKeyword, DoubleDashFlagShortKeyword, FlagStyle.DoubleDash);
        
        public const string SingleDashFlagKeyword = "SingleDashFlag";
        public const char SingleDashFlagShortKeyword = 's';
        public Flag SingleDashFlag = new Flag("Single-dash flag style", SingleDashFlagKeyword, SingleDashFlagShortKeyword, FlagStyle.SingleDash);
        
        public const string MsBuildFlagKeyword = "MsBuildDashFlag";
        public const char MsBuildFlagShortKeyword = 'm';
        public Flag MsBuildFlag = new Flag("MsBuild flag style", MsBuildFlagKeyword, MsBuildFlagShortKeyword, FlagStyle.MsBuild);

        #endregion
        
        #region Properties
        
        public const string DoubleDashPropertyKeyword = "DoubleDashProperty";
        public const char DoubleDashPropertyShortKeyword = 'D';
        public Property DoubleDashProperty = new Property("Double-dash property style", DoubleDashPropertyKeyword, DoubleDashPropertyShortKeyword, PropertyStyle.DoubleDash);
        
        public const string DoubleDashWithEqualPropertyKeyword = "DoubleDashWithEqualProperty";
        public const char DoubleDashWithEqualPropertyShortKeyword = 'E';
        public Property DoubleDashWithEqualProperty = new Property("Double-dash property style", DoubleDashWithEqualPropertyKeyword, DoubleDashWithEqualPropertyShortKeyword, PropertyStyle.DoubleDashWithEqual);
        
        public const string SingleDashPropertyKeyword = "SingleDashProperty";
        public const char SingleDashPropertyShortKeyword = 'S';
        public Property SingleDashProperty = new Property("Single-dash property style", SingleDashPropertyKeyword, SingleDashPropertyShortKeyword, PropertyStyle.SingleDash);
        
        public const string SingleDashWithEqualPropertyKeyword = "SingleDashWithEqualProperty";
        public const char SingleDashWithEqualPropertyShortKeyword = 'W';
        public Property SingleDashWithEqualProperty = new Property("Single-dash property style", SingleDashWithEqualPropertyKeyword, SingleDashWithEqualPropertyShortKeyword, PropertyStyle.SingleDashWithEqual);
        
        public const string MsBuildPropertyKeyword = "MsBuildProperty";
        public const char MsBuildPropertyShortKeyword = 'M';
        public Property MsBuildProperty = new Property("MsBuild property style", MsBuildPropertyKeyword, MsBuildPropertyShortKeyword, PropertyStyle.MsBuild);

        #endregion

        #region Commands

        public const string DefaultCommandKeyword = "Command";
        public Command DefaultCommand = new ValidCommand("Command default style", DefaultCommandKeyword, this);

        #endregion
        
        public override void OnExecute(string[] args)
        {
            Console.WriteLine("This application should execute without any runtime or configuration errors.");
        }
    }
}