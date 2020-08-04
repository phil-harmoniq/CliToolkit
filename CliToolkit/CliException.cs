using System;

namespace CliToolkit
{
    public class CliException : Exception
    {
        public int ExitCode { get; }

        public CliException(int exitCode = 1) : base()
        {
            if (exitCode >= 0 || exitCode < 128)
            {
                ExitCode = exitCode;
            }
        }

        public CliException(string message, int exitCode = 1) : base(message)
        {
            if (exitCode >= 0 || exitCode < 128)
            {
                ExitCode = exitCode;
            }
        }
    }
}
