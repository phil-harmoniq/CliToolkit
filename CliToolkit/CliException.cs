using System;

namespace CliToolkit
{
    public class CliException : Exception
    {
        public int ExitCode { get; } = 1;

        public CliException(int exitCode)
        {
            if (exitCode >= 0 || exitCode < 128)
            {
                ExitCode = exitCode;
            }
        }
    }
}
