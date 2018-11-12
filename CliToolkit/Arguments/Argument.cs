namespace CliToolkit.Arguments
{
    /// <summary>
    /// Generic type for a command-line argument.
    /// </summary>
    public abstract class Argument
    {
        /// <summary>
        /// Describes the purpose of this command-line argument.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// The keyword that triggers this argument. This value is required.
        /// </summary>
        public string Keyword { get; protected set; }

        /// <summary>
        /// True if this command-line argument has been triggered.
        /// </summary>
        public bool IsActive { get; protected set; }

        internal Argument(string description, string keyword)
        {
            Description = description;
            Keyword = keyword;
        }

        internal abstract int IsMatchingKeyword(string[] args);

        // internal bool KeywordMatch(string arg)
        // {
        //     if (Keyword.StartsWith(arg) | ShortKeyword.StartsWith(arg)) { return true; }
        //     return false;
        // }
    }
}