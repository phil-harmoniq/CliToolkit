using CliToolkit.Arguments;
using CliToolkit.Styles;
using CliToolkit.Exceptions;

namespace CliToolkit.Tests.Templates.DuplicateKeywords
{
    public class DuplicateDoubleDashFlagKeywordApp : RuntimeExceptionApp
    {
        public Flag DuplicateDoubleDashFlag { get; } = new Flag("Duplicate double-dash keyword", DoubleDashFlagKeyword, 'P', FlagStyle.DoubleDash);
    }

    public class DuplicateDoubleDashFlagShortKeywordApp : RuntimeExceptionApp
    {
        public Flag DuplicateDoubleDashFlag { get; } = new Flag("Duplicate double-dash short keyword", "uniqueDoubleDashFlag", DoubleDashFlagShortKeyword, FlagStyle.DoubleDash);
    }
    
    public class DuplicateSingleDashFlagKeywordApp : RuntimeExceptionApp
    {
        public Flag DuplicateSingleDashFlag { get; } = new Flag("Duplicate single-dash keyword", SingleDashFlagKeyword, 'P', FlagStyle.SingleDash);
    }

    public class DuplicateSingleDashFlagShortKeywordApp : RuntimeExceptionApp
    {
        public Flag DuplicateSingleDashFlag { get; } = new Flag("Duplicate double-dash short keyword", "uniqueSingleDashFlag", SingleDashFlagShortKeyword, FlagStyle.SingleDash);
    }
    
    public class DuplicateMsBuildFlagKeywordApp : RuntimeExceptionApp
    {
        public Flag DuplicateMsBuildFlag { get; } = new Flag("Duplicate MsBuild keyword", MsBuildFlagKeyword, 'P', FlagStyle.MsBuild);
    }

    public class DuplicateMsBuildFlagShortKeywordApp : RuntimeExceptionApp
    {
        public Flag DuplicateMsBuildFlag { get; } = new Flag("Duplicate MsBuild short keyword", "uniqueMsbuildFlag", MsBuildFlagShortKeyword, FlagStyle.MsBuild);
    }
}