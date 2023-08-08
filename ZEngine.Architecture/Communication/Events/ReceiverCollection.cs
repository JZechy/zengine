using System.Collections;
using Microsoft.Extensions.Logging;

namespace ZEngine.Architecture.Communication.Events;

/// <summary>
/// Collection of receivers for a specific message type.
/// </summary>
/// <typeparam name="TMessage"></typeparam>
public class ReceiverCollection<TMessage> : IReceiverCollection where TMessage : IEventMessage
{
    /// <summary>
    /// Logs the collection's actions.
    /// </summary>
    private readonly ILogger<ReceiverCollection<TMessage>> _logger;

    /// <summary>
    /// Collection of receivers.
    /// </summary>
    private readonly HashSet<Action<TMessage>> _receivers = new();

    public ReceiverCollection(ILogger<ReceiverCollection<TMessage>> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public int Count
    {
        get
        {
            lock (SyncRoot)
            {
                return _receivers.Count;
            }
        }
    }

    /// <inheritdoc />
    public bool IsSynchronized => true;

    /// <inheritdoc />
    public object SyncRoot { get; } = new();

    /// <inheritdoc />
    public void Notify(object message)
    {
        lock (SyncRoot)
        {
            foreach (Action<TMessage> action in _receivers)
            {
                try
                {
                    action.Invoke((TMessage) message);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "An error occurred while notifying a receiver.");
                }
            }
        }
    }

    /// <inheritdoc />
    public void Add(object receiver)
    {
        lock (SyncRoot)
        {
            _receivers.Add((Action<TMessage>) receiver);
        }
    }

    /// <inheritdoc />
    public void Remove(object receiver)
    {
        lock (SyncRoot)
        {
            _receivers.Remove((Action<TMessage>) receiver);
        }
    }

    /// <inheritdoc />
    public IEnumerator GetEnumerator()
    {
        lock (SyncRoot)
        {
            return _receivers.GetEnumerator();
        }
    }

    /// <inheritdoc />
    public void CopyTo(Array array, int index)
    {
        lock (SyncRoot)
        {
            _receivers.CopyTo((Action<TMessage>[]) array, index);
        }
    }
}