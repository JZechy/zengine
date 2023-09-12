namespace ZEngine.Systems.ThreadSynchronization.Operations;

/// <summary>
/// Runs operation on the game loop.
/// </summary>
public readonly struct AsyncOperation : IAsyncOperation
{
    /// <summary>
    /// Callback that will be invoked.
    /// </summary>
    private readonly Action _action;
    
    /// <summary>
    /// Soruce to signalize the completion of the operation.
    /// </summary>
    private readonly TaskCompletionSource _source;
    
    public AsyncOperation(Action action)
    {
        _action = action;
        _source = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
    }
    
    /// <summary>
    /// Returns the task that will be completed when the operation is done.
    /// </summary>
    public Task Task => _source.Task;

    /// <inheritdoc />
    public void Invoke()
    {
        try
        {
            _action.Invoke();
            _source.TrySetResult();
        }
        catch (Exception e)
        {
            _source.TrySetException(e);
        }
    }

    /// <inheritdoc />
    public void Cancel()
    {
        _source.TrySetCanceled();
    }
}

/// <summary>
/// Runs operation on the game loop and returns the result.
/// </summary>
/// <typeparam name="TResult">Type of result from the function.</typeparam>
public readonly struct AsyncOperation<TResult> : IAsyncOperation
{
    /// <summary>
    /// Function that will be invoke on game loop.
    /// </summary>
    private readonly Func<TResult> _func;
    
    /// <summary>
    /// Source to signalize the completion.
    /// </summary>
    private readonly TaskCompletionSource<TResult> _source;
    
    public AsyncOperation(Func<TResult> func)
    {
        _func = func;
        _source = new TaskCompletionSource<TResult>(TaskCreationOptions.RunContinuationsAsynchronously);
    }

    /// <summary>
    /// Returns the task that will be completed when the operation is done.
    /// </summary>
    public Task<TResult> Task => _source.Task;

    /// <inheritdoc />
    public void Invoke()
    {
        try
        {
            TResult result = _func.Invoke();
            _source.TrySetResult(result);
        }
        catch (Exception e)
        {
            _source.TrySetException(e);
        }
    }

    /// <inheritdoc />
    public void Cancel()
    {
        _source.TrySetCanceled();
    }
}