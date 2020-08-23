using System;

namespace CliToolkit
{
    /// <summary>
    /// Used to exit the application and report an error to the system.
    /// </summary>
    public class CliException : Exception
    {
        /// <summary>
        /// The exit code that will be reported to the system.
        /// </summary>
        public int ExitCode { get; }

        /// <summary>
        /// Used to exit the application and report an error to the system.
        /// </summary>
        /// <param name="exitCode">The exit code that will be reported to the system.</param>
        public CliException(int exitCode = 1) : base()
        {
            if (exitCode >= 0 || exitCode < 128)
            {
                ExitCode = exitCode;
            }
        }

        /// <summary>
        /// Used to exit the application and report an error to the system.
        /// </summary>
        /// <param name="message">The error message to display in the console.</param>
        /// <param name="exitCode">The exit code that will be reported to the system.</param>
        public CliException(string message, int exitCode = 1) : base(message)
        {
            if (exitCode >= 0 || exitCode < 128)
            {
                ExitCode = exitCode;
            }
        }
    }
}
