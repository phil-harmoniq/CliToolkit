using System;
using CliToolkit.Arguments;
using CliToolkit.Styles;

namespace CliToolkit.Tests.Templates
{
    public class ValidApp : CliApp
    {
        #region Flags

        public const string DoubleDashFlagKeyword = "DoubleDashFlag";
        public const char DoubleDashFlagShortKeyword = 'd';
        public const string DoubleDashFlagDescription = "Double-dash flag style";
        public Flag DoubleDashFlag = new Flag(DoubleDashFlagDescription, DoubleDashFlagKeyword, DoubleDashFlagShortKeyword, FlagStyle.DoubleDash);
        
        public const string SingleDashFlagKeyword = "SingleDashFlag";
        public const char SingleDashFlagShortKeyword = 's';
        public const string SingleDashFlagDescription = "Single-dash flag style";
        public Flag SingleDashFlag = new Flag(SingleDashFlagDescription, SingleDashFlagKeyword, SingleDashFlagShortKeyword, FlagStyle.SingleDash);
        
        public const string MsBuildFlagKeyword = "MsBuildDashFlag";
        public const char MsBuildFlagShortKeyword = 'm';
        public const string MsBuildFlagDescription = "MsBuild flag style";
        public Flag MsBuildFlag = new Flag(MsBuildFlagDescription, MsBuildFlagKeyword, MsBuildFlagShortKeyword, FlagStyle.MsBuild);

        #endregion
        
        #region Properties
        
        public const string DoubleDashPropertyKeyword = "DoubleDashProperty";
        public const char DoubleDashPropertyShortKeyword = 'D';
        public const string DoubleDashPropertyDescription = "Double-dash property style";
        public Property DoubleDashProperty = new Property(DoubleDashPropertyDescription, DoubleDashPropertyKeyword, DoubleDashPropertyShortKeyword, PropertyStyle.DoubleDash);
        
        public const string DoubleDashWithEqualPropertyKeyword = "DoubleDashWithEqualProperty";
        public const char DoubleDashWithEqualPropertyShortKeyword = 'E';
        public const string DoubleDashWithEqualPropertyDescription = "Double-dash with equal property style";
        public Property DoubleDashWithEqualProperty = new Property(DoubleDashWithEqualPropertyDescription, DoubleDashWithEqualPropertyKeyword, DoubleDashWithEqualPropertyShortKeyword, PropertyStyle.DoubleDashWithEqual);
        
        public const string SingleDashPropertyKeyword = "SingleDashProperty";
        public const char SingleDashPropertyShortKeyword = 'S';
        public const string SingleDashPropertyDescription = "Single-dash property style";
        public Property SingleDashProperty = new Property(SingleDashPropertyDescription, SingleDashPropertyKeyword, SingleDashPropertyShortKeyword, PropertyStyle.SingleDash);
        
        public const string SingleDashWithEqualPropertyKeyword = "SingleDashWithEqualProperty";
        public const char SingleDashWithEqualPropertyShortKeyword = 'W';
        public const string SingleDashWithEqualPropertyDescription = "Single-dash with equal property style";
        public Property SingleDashWithEqualProperty = new Property(SingleDashWithEqualPropertyDescription, SingleDashWithEqualPropertyKeyword, SingleDashWithEqualPropertyShortKeyword, PropertyStyle.SingleDashWithEqual);
        
        public const string MsBuildPropertyKeyword = "MsBuildProperty";
        public const char MsBuildPropertyShortKeyword = 'M';
        public const string MsBuildPropertyDescription = "MsBuild property style";
        public Property MsBuildProperty = new Property(MsBuildPropertyDescription, MsBuildPropertyKeyword, MsBuildPropertyShortKeyword, PropertyStyle.MsBuild);

        #endregion

        #region Commands

        public const string DefaultCommandKeyword = "Command";
        public const string DefaultCommandDescription = "Command default style";
        public Command DefaultCommand = new ValidCommand(DefaultCommandDescription, DefaultCommandKeyword);

        #endregion
        
        public override void OnExecute(string[] args)
        {
            Console.WriteLine("This application should execute without any runtime or configuration errors.");
        }
    }
}