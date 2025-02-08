using System.Collections.Concurrent;
using ZEngine.Core;
using ZEngine.Systems.ThreadSynchronization.Operations;

namespace ZEngine.Systems.ThreadSynchronization;

/// <summary>
///     This system synchronizes calls from other threads in a safer way to ensure multi-threading stability with the game loop.
/// </summary>
public class ThreadSynchronizationSystem : IGameSystem
{
    /// <summary>
    ///     Queue containing all registered operations.
    /// </summary>
    private readonly ConcurrentQueue<IAsyncOperation> _operations = new();

    /// <inheritdoc />
    public int Priority => 99;

    /// <inheritdoc />
    public void Initialize()
    {
        ThreadSynchronization.CreateInstance(this);
    }

    /// <inheritdoc />
    public void Update()
    {
        while (_operations.TryDequeue(out IAsyncOperation? operation)) operation.Invoke();
    }

    /// <inheritdoc />
    public void CleanUp()
    {
        while (_operations.TryDequeue(out IAsyncOperation? operation)) operation.Cancel();
    }

    /// <summary>
    ///     Registers new async operation.
    /// </summary>
    /// <param name="operation"></param>
    public void RegisterOperation(IAsyncOperation operation)
    {
        _operations.Enqueue(operation);
    }
}