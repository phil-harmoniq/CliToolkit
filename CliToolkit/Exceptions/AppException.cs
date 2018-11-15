namespace CliToolkit.Exceptions
{
    /// <summary>
    /// Generic type for <see cref="CliApp" /> exceptions
    /// </summary>
    [System.Serializable]
    public class AppException : System.Exception
    {
        /// <summary>
        /// The exit code to be passed to the terminal for this exception.
        /// </summary>
        public int ExitCode { get; protected set; }

        /// <summary>
        /// Generate a new exception for this application.
        /// </summary>
        /// <param name="exitCode">The exit code to be passed to the terminal for this exception.</param>
        protected AppException(int exitCode = 1){ ExitCode = ValidateExitCode(exitCode); }

        /// <summary>
        /// Generate a new exception for this application.
        /// </summary>
        /// <param name="message">An additional message to be passed along with this exception.</param>
        /// <param name="exitCode">The exit code to be passed to the terminal for this exception.</param>
        /// <returns></returns>
        protected AppException(string message, int exitCode = 1) : base(message) { ExitCode = ValidateExitCode(exitCode); }

        /// <summary>
        /// Generate a new exception for this application.
        /// </summary>
        /// <param name="message">An additional message to be passed along with this exception.</param>
        /// <param name="inner">The original exception that caused this CLI app to fail.</param>
        /// <param name="exitCode">The exit code to be passed to the terminal for this exception.</param>
        /// <returns></returns>
        protected AppException(string message, System.Exception inner, int exitCode = 1) : base(message, inner) { ExitCode = ValidateExitCode(exitCode); }

        /// <summary>
        /// Makes sure that the given exit code is within the valid range. (1-255)
        /// </summary>
        /// <param name="exitcode"></param>
        /// <returns></returns>
        protected int ValidateExitCode(int exitcode)
        {
            if (exitcode < 1) { return 1; }
            if (exitcode > 255) { return 255; }
            return exitcode;
        }
    }

    // [System.Serializable]
    // public class CliAppException : System.Exception
    // {
    //     public CliAppException() { }
    //     public CliAppException(string message) : base(message) { }
    //     public CliAppException(string message, System.Exception inner) : base(message, inner) { }
    //     protected CliAppException(
    //         System.Runtime.Serialization.SerializationInfo info,
    //         System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    // }
}