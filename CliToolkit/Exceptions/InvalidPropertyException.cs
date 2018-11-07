namespace CliToolkit.Exceptions
{
    /// <summary>
    /// Thrown when the configuration of the CLI application is invalid.
    /// </summary>
    [System.Serializable]
    internal sealed class InvalidPropertyException : AppRuntimeException
    {
        internal InvalidPropertyException(string message, int exitCode = 1) : base(message, exitCode)
        {
        }
    }
}