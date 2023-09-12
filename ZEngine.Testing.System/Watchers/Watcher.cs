namespace ZEngine.Testing.System.Watchers;

/// <summary>
/// Abstract implementation of the <see cref="IWatcher"/> interface.
/// </summary>
public abstract class Watcher : IWatcher
{
    /// <summary>
    /// Completion source that is waiting until the predicate is not met.
    /// </summary>
    private readonly TaskCompletionSource _source = new();

    /// <summary>
    /// Cancellation token source is used to prevent never-ending watcher run.
    /// </summary>
    private readonly CancellationTokenSource _cancellation;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timeoutMs">Watcher timeout in ms.</param>
    protected Watcher(int timeoutMs)
    {
        _cancellation = new CancellationTokenSource(timeoutMs);
        _cancellation.Token.Register(() =>
        {
            if (_source.Task.IsCompleted)
            {
                return;
            }

            _source.TrySetCanceled();
        });
    }
    
    /// <summary>
    /// Gets instance of the source that is used to complete the task.
    /// </summary>
    protected TaskCompletionSource Source => _source;

    /// <inheritdoc />
    public Task Task => _source.Task;

    /// <inheritdoc />
    public abstract void Check();

    /// <inheritdoc />
    public void Cancel()
    {
        _source.TrySetCanceled();
    }
}