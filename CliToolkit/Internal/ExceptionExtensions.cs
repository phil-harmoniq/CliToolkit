namespace CliToolkit.Internal
{
    internal static class ExceptionExtensions
    {
        internal static bool HasDefaultExcepionMessage(this CliException ex)
        {
            var fullName = ex.GetType().FullName;
            return ex.Message.Equals($"Exception of type '{fullName}' was thrown.");
        }
    }
}
