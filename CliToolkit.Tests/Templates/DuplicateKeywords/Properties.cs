using CliToolkit.Arguments;
using CliToolkit.Arguments.Styles;

namespace CliToolkit.Tests.Templates.DuplicateKeywords
{
    public class Properties
    {
        public class DuplicateDoubleDashPropertyKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateDoubleDashProperty { get; } = new Property("Duplicate double-dash keyword", "doubleDashProperty", 'P', PropertyStyle.DoubleDash);
        }

        public class DuplicateDoubleDashPropertyShortKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateDoubleDashProperty { get; } = new Property("Duplicate double-dash short keyword", "uniqueDoubleDashProperty", 'D', PropertyStyle.DoubleDash);
        }

        public class DuplicateDoubleDashWithEqualPropertyKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateDoubleDashProperty { get; } = new Property("Duplicate double-dash with equal keyword", "doubleDashProperty", 'P', PropertyStyle.DoubleDashWithEqual);
        }

        public class DuplicateDoubleDashWithEqualPropertyShortKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateDoubleDashProperty { get; } = new Property("Duplicate double-dash with equal short keyword", "uniqueDoubleDashProperty", 'D', PropertyStyle.DoubleDashWithEqual);
        }
        
        public class DuplicateSingleDashPropertyKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateSingleDashProperty { get; } = new Property("Duplicate single-dash keyword", "singleDashProperty", 'P', PropertyStyle.SingleDash);
        }

        public class DuplicateSingleDashPropertyShortKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateSingleDashProperty { get; } = new Property("Duplicate single-dash short keyword", "uniqueSingleDashProperty", 'S', PropertyStyle.SingleDash);
        }

        public class DuplicateSingleDashWithEqualPropertyKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateSingleDashProperty { get; } = new Property("Duplicate single-dash with equal keyword", "singleDashProperty", 'P', PropertyStyle.SingleDashWithEqual);
        }

        public class DuplicateSingleDashWithEqualPropertyShortKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateSingleDashProperty { get; } = new Property("Duplicate single-dash with equal short keyword", "uniqueSingleDashProperty", 'D', PropertyStyle.SingleDashWithEqual);
        }
        
        public class DuplicateMsBuildPropertyKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateMsBuildDashProperty { get; } = new Property("Duplicate MsBuild keyword", "msbuildProperty", 'P', PropertyStyle.MsBuild);
        }

        public class DuplicateMsBuildPropertyShortKeywordApp : RuntimeExceptionApp
        {
            public Property DuplicateMsBuildDashProperty { get; } = new Property("Duplicate MsBuild short keyword", "uniqueMsbuildProperty", 'M', PropertyStyle.MsBuild);
        }
    }
}