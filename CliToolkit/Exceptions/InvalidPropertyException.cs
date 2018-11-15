namespace CliToolkit.Exceptions
{
    /// <summary>
    /// Thrown when invalid input for properties are detected.
    /// </summary>
    [System.Serializable]
    internal sealed class InvalidPropertyException : AppRuntimeException
    {
        internal InvalidPropertyException(string message, int exitCode = 1) : base(message, exitCode)
        {
        }
    }
}