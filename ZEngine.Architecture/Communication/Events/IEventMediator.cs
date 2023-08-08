namespace ZEngine.Architecture.Communication.Events;

/// <summary>
/// Event mediator is a global communication system that allows for events to be sent and received.
/// </summary>
public interface IEventMediator
{
    /// <summary>
    /// Subscribes a new receiver to the event.
    /// </summary>
    /// <param name="receiver"></param>
    /// <typeparam name="TMessage"></typeparam>
    void Subscribe<TMessage>(Action<TMessage> receiver) where TMessage : IEventMessage;
    
    /// <summary>
    /// Unsubscribes a receiver from the event.
    /// </summary>
    /// <param name="receiver"></param>
    /// <typeparam name="TMessage"></typeparam>
    void Unsubscribe<TMessage>(Action<TMessage> receiver) where TMessage : IEventMessage;
    
    /// <summary>
    /// Notifies all registered receivers for the event.
    /// </summary>
    /// <param name="message"></param>
    /// <typeparam name="TMessage"></typeparam>
    void Notify<TMessage>(TMessage message) where TMessage : IEventMessage;
}