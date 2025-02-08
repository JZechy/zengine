using ConcurrentCollections;
using Microsoft.Extensions.Logging;

namespace ZEngine.Architecture.Communication.Events;

/// <summary>
///     Collection of receivers for a specific message type.
/// </summary>
/// <typeparam name="TMessage"></typeparam>
public class ReceiverCollection<TMessage> : IReceiverCollection where TMessage : IEventMessage
{
    /// <summary>
    ///     Logs the collection's actions.
    /// </summary>
    private readonly ILogger<ReceiverCollection<TMessage>> _logger;

    /// <summary>
    ///     Collection of receivers.
    /// </summary>
    private readonly ConcurrentHashSet<Action<TMessage>> _receivers = new();

    /// <summary>
    /// </summary>
    /// <param name="logger"></param>
    public ReceiverCollection(ILogger<ReceiverCollection<TMessage>> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public int Count => _receivers.Count;

    /// <inheritdoc />
    public void Notify(object message)
    {
        foreach (Action<TMessage> action in _receivers)
            try
            {
                action.Invoke((TMessage)message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while notifying a receiver.");
            }
    }

    /// <inheritdoc />
    public void Add(object receiver)
    {
        _receivers.Add((Action<TMessage>)receiver);
    }

    /// <inheritdoc />
    public void Remove(object receiver)
    {
        _receivers.TryRemove((Action<TMessage>)receiver);
    }
}