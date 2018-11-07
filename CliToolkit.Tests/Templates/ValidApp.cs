using System;
using CliToolkit.Arguments;
using CliToolkit.Arguments.Styles;

namespace CliToolkit.Tests.Templates
{
    public class ValidApp : CliApp
    {
        public Flag DoubleDashFlag { get; } = new Flag("Double-dash flag style", "doubleDashFlag", 'd', FlagStyle.DoubleDash);
        public Flag SingleDashFlag { get; } = new Flag("Single-dash flag style", "singleDashFlag", 's', FlagStyle.SingleDash);
        public Flag MsBuildDashFlag { get; } = new Flag("MsBuild flag style", "msbuildFlag", 'm', FlagStyle.MsBuild);

        public Property DoubleDashProperty { get; } = new Property("Double-dash property style", "doubleDashProperty", 'D', PropertyStyle.DoubleDash);
        public Property DoubleDashWithEqualProperty { get; } = new Property("Double-dash property style", "doubleDashWithEqualProperty", 'E', PropertyStyle.DoubleDashWithEqual);
        public Property SingleDashProperty { get; } = new Property("Single-dash property style", "singleDashProperty", 'S', PropertyStyle.SingleDash);
        public Property SingleDashWithEqualProperty { get; } = new Property("Single-dash property style", "singleDashWithEqualProperty", 'W', PropertyStyle.SingleDashWithEqual);
        public Property MsBuildProperty { get; } = new Property("MsBuild property style", "msbuildProperty", 'M', PropertyStyle.MsBuild);

        public override void OnExecute(string[] args)
        {
            Console.WriteLine("This application should execute without any runtime or configuration errors.");
        }
    }
}