namespace ZEngine.Architecture.Communication.Messages;

/// <summary>
/// Defines methods for sending messages to other objects.
/// </summary>
public interface IMessageReceiver
{
    /// <summary>
    /// Sends an empty message to <paramref name="target"/>.
    /// </summary>
    /// <param name="target"></param>
    void SendMessage(string target);
}