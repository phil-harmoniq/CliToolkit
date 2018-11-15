using CliToolkit.Arguments;
using CliToolkit.Styles;

namespace CliToolkit.Tests.Templates.DuplicateKeywords
{
    public class Properties
    {
        public class DuplicateDoubleDashPropertyKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateDoubleDashProperty { get; } = new Property("Duplicate double-dash keyword", DoubleDashPropertyKeyword, 'P', PropertyStyle.DoubleDash);
        }

        public class DuplicateDoubleDashPropertyShortKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateDoubleDashProperty { get; } = new Property("Duplicate double-dash short keyword", "uniqueDoubleDashProperty", DoubleDashPropertyShortKeyword, PropertyStyle.DoubleDash);
        }

        public class DuplicateDoubleDashWithEqualPropertyKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateDoubleDashProperty { get; } = new Property("Duplicate double-dash with equal keyword", DoubleDashWithEqualPropertyKeyword, 'P', PropertyStyle.DoubleDashWithEqual);
        }

        public class DuplicateDoubleDashWithEqualPropertyShortKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateDoubleDashProperty { get; } = new Property("Duplicate double-dash with equal short keyword", "uniqueDoubleDashProperty", DoubleDashWithEqualPropertyShortKeyword, PropertyStyle.DoubleDashWithEqual);
        }
        
        public class DuplicateSingleDashPropertyKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateSingleDashProperty { get; } = new Property("Duplicate single-dash keyword", SingleDashPropertyKeyword, 'P', PropertyStyle.SingleDash);
        }

        public class DuplicateSingleDashPropertyShortKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateSingleDashProperty { get; } = new Property("Duplicate single-dash short keyword", "uniqueSingleDashProperty", SingleDashPropertyShortKeyword, PropertyStyle.SingleDash);
        }

        public class DuplicateSingleDashWithEqualPropertyKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateSingleDashProperty { get; } = new Property("Duplicate single-dash with equal keyword", SingleDashWithEqualPropertyKeyword, 'P', PropertyStyle.SingleDashWithEqual);
        }

        public class DuplicateSingleDashWithEqualPropertyShortKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateSingleDashProperty { get; } = new Property("Duplicate single-dash with equal short keyword", "uniqueSingleDashProperty", SingleDashWithEqualPropertyShortKeyword, PropertyStyle.SingleDashWithEqual);
        }
        
        public class DuplicateMsBuildPropertyKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateMsBuildProperty { get; } = new Property("Duplicate MsBuild keyword", MsBuildPropertyKeyword, 'P', PropertyStyle.MsBuild);
        }

        public class DuplicateMsBuildPropertyShortKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateMsBuildProperty { get; } = new Property("Duplicate MsBuild short keyword", "uniqueMsbuildProperty", MsBuildPropertyShortKeyword, PropertyStyle.MsBuild);
        }
    }
}