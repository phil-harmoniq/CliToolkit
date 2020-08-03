using System;

namespace CliToolkit
{
    public class CliRuntimeException : Exception
    {
        public int ExitCode { get; }

        public CliRuntimeException(string message, int exitCode = 1) : base(message)
        {
            if (exitCode < 0) { exitCode = 1; }
            ExitCode = exitCode;
        }
    }
}
