using CliToolkit.Arguments;
using CliToolkit.Arguments.Styles;
using CliToolkit.Exceptions;

namespace CliToolkit.Tests.Templates.DuplicateKeywords
{
    public class DuplicateDoubleDashFlagKeywordApp : RuntimeExceptionApp
    {
        public Flag DuplicateDoubleDashFlag { get; } = new Flag("Duplicate double-dash keyword", "doubleDashFlag", 'P', FlagStyle.DoubleDash);
    }

    public class DuplicateDoubleDashFlagShortKeywordApp : RuntimeExceptionApp
    {
        public Flag DuplicateDoubleDashFlag { get; } = new Flag("Duplicate double-dash short keyword", "uniqueDoubleDashFlag", 'd', FlagStyle.DoubleDash);
    }
    
    public class DuplicateSingleDashFlagKeywordApp : RuntimeExceptionApp
    {
        public Flag DuplicateSingleDashFlag { get; } = new Flag("Duplicate single-dash keyword", "singleDashFlag", 'P', FlagStyle.SingleDash);
    }

    public class DuplicateSingleDashFlagShortKeywordApp : RuntimeExceptionApp
    {
        public Flag DuplicateSingleDashFlag { get; } = new Flag("Duplicate double-dash short keyword", "uniqueSingleDashFlag", 's', FlagStyle.SingleDash);
    }
    
    public class DuplicateMsBuildFlagKeywordApp : RuntimeExceptionApp
    {
        public Flag DuplicateMsBuildDashFlag { get; } = new Flag("Duplicate MsBuild keyword", "msbuildFlag", 'P', FlagStyle.MsBuild);
    }

    public class DuplicateMsBuildFlagShortKeywordApp : RuntimeExceptionApp
    {
        public Flag DuplicateMsBuildDashFlag { get; } = new Flag("Duplicate MsBuild short keyword", "uniqueMsbuildFlag", 'm', FlagStyle.MsBuild);
    }
}