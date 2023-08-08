using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace ZEngine.Architecture.Communication.Events;

/// <summary>
/// Default implementation of the <see cref="IEventMediator"/> interface.
/// </summary>
public class EventMediator : IEventMediator
{
    /// <summary>
    /// Creates a logger for receiver collections.
    /// </summary>
    private readonly ILoggerFactory _loggerFactory;

    /// <summary>
    /// Dictionary assigning a collection of receivers to a specific message type.
    /// </summary>
    private readonly ConcurrentDictionary<Type, IReceiverCollection> _receivers = new();

    public EventMediator(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    /// <inheritdoc />
    public void Subscribe<TMessage>(Action<TMessage> receiver) where TMessage : IEventMessage
    {
        Type messageType = typeof(TMessage);
        if (!_receivers.ContainsKey(messageType))
        {
            _receivers.TryAdd(messageType, new ReceiverCollection<TMessage>(_loggerFactory.CreateLogger<ReceiverCollection<TMessage>>()));
        }

        _receivers[messageType].Add(receiver);
    }

    /// <inheritdoc />
    public void Unsubscribe<TMessage>(Action<TMessage> receiver) where TMessage : IEventMessage
    {
        Type messageType = typeof(TMessage);
        if (!_receivers.TryGetValue(messageType, out IReceiverCollection? collection))
        {
            return;
        }

        collection.Remove(receiver);
        if (collection.Count == 0)
        {
            _receivers.TryRemove(messageType, out IReceiverCollection? _);
        }
    }

    /// <inheritdoc />
    public void Notify<TMessage>(TMessage message) where TMessage : IEventMessage
    {
        Type messageType = typeof(TMessage);
        if (!_receivers.TryGetValue(messageType, out IReceiverCollection? collection))
        {
            return;
        }

        collection.Notify(message);
    }
}