namespace ZEngine.Architecture.Communication.Messages;

/// <summary>
/// Thrown when an exception is thrown by target of invocation.
/// </summary>
public class MessageHandlerException : Exception
{
    /// <summary>
    /// Wording of the exception message.
    /// </summary>
    private const string ExceptionMessage = "An expection has been thrown by target of invocation.";
    
    public MessageHandlerException(Exception innerException) : base(ExceptionMessage, innerException)
    {
    }
}