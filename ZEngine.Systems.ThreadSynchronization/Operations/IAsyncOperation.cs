namespace ZEngine.Systems.ThreadSynchronization.Operations;

/// <summary>
///     Encapsules the asynchronous operation that needs to be synchronised with the game loop call.
/// </summary>
public interface IAsyncOperation
{
    /// <summary>
    ///     Invokes the operation.
    /// </summary>
    void Invoke();

    /// <summary>
    ///     Cancels the operation.
    /// </summary>
    void Cancel();
}