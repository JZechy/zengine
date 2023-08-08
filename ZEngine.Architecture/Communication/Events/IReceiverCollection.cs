using System.Collections;

namespace ZEngine.Architecture.Communication.Events;

/// <summary>
/// Represents thread-safe collection of subscribers for a specific message type.
/// </summary>
public interface IReceiverCollection : ICollection
{
    /// <summary>
    /// Notifies all subscribers in the collection.
    /// </summary>
    /// <param name="message"></param>
    void Notify(object message);

    /// <summary>
    /// Adds a new subscriber to the collection.
    /// </summary>
    /// <param name="receiver"></param>
    void Add(object receiver);

    /// <summary>
    /// Removes a subscriber from the collection.
    /// </summary>
    /// <param name="receiver"></param>
    void Remove(object receiver);
}