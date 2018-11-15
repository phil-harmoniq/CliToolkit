using System;
using System.Runtime.Serialization;
using CliToolkit.Exceptions;

namespace CliToolkit
{
    /// <summary>
    /// Thrown when the method <see cref="CliApp.OnExecute" /> fails during runtime.
    /// </summary>
    [Serializable]
    public class AppRuntimeException : AppException
    {
        /// <summary>
        /// Generate a new exception for this application.
        /// </summary>
        /// <param name="exitCode">The exit code to be passed to the terminal for this exception.</param>
        public AppRuntimeException(int exitCode = 1) : base(exitCode) {}

        /// <summary>
        /// Generate a new exception for this application.
        /// </summary>
        /// <param name="message">An additional message to be passed along with this exception.</param>
        /// <param name="exitCode">The exit code to be passed to the terminal for this exception.</param>
        /// <returns></returns>
        public AppRuntimeException(string message, int exitCode = 1) : base(message, exitCode) {}

        /// <summary>
        /// Generate a new exception for this application.
        /// </summary>
        /// <param name="message">An additional message to be passed along with this exception.</param>
        /// <param name="inner">The original exception that caused this CLI app to fail.</param>
        /// <param name="exitCode">The exit code to be passed to the terminal for this exception.</param>
        /// <returns></returns>
        public AppRuntimeException(string message, System.Exception inner, int exitCode = 1) : base(message, inner, exitCode) {}
    }
}