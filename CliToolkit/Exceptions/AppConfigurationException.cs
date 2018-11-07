namespace CliToolkit.Exceptions
{
    /// <summary>
    /// Thrown when the configuration of the CLI application is invalid.
    /// </summary>
    [System.Serializable]
    public sealed class AppConfigurationException : AppException
    {
        internal AppConfigurationException(string message, int exitCode = 1) : base(message, exitCode)
        {
        }
    }
}