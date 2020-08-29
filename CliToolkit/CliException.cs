using System;

namespace CliToolkit
{
    /// <summary>
    /// Used to exit the app and report an error gracefully to the system and the console.
    /// </summary>
    public class CliException : Exception
    {
        /// <summary>
        /// The exit code that will be reported to the system.
        /// </summary>
        public int ExitCode { get; }

        /// <summary>
        /// Determines whether a traditional stack-trace error message will be shown.
        /// </summary>
        public bool ShowStackTrace { get; }

        /// <summary>
        /// Used to exit the application and report an error to the system.
        /// </summary>
        /// <param name="exitCode">The exit code that will be reported to the system.</param>
        /// <param name="showStackTrace">Enable a traditional stack-trace error message.</param>
        public CliException(int exitCode = 1, bool showStackTrace = false) : base()
        {
            if (exitCode >= 0 || exitCode < 128)
            {
                ExitCode = exitCode;
            }
            ShowStackTrace = showStackTrace;
        }

        /// <summary>
        /// Used to exit the application and report an error to the system.
        /// </summary>
        /// <param name="message">The error message to display in the console.</param>
        /// <param name="exitCode">The exit code that will be reported to the system.</param>
        /// <param name="showStackTrace">Enable a traditional stack-trace error message.</param>
        public CliException(string message, int exitCode = 1, bool showStackTrace = false) : base(message)
        {
            if (exitCode >= 0 || exitCode < 128)
            {
                ExitCode = exitCode;
            }
            ShowStackTrace = showStackTrace;
        }
    }
}
