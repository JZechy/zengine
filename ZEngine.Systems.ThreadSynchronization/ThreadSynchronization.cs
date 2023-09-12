using ZEngine.Systems.ThreadSynchronization.Operations;

namespace ZEngine.Systems.ThreadSynchronization;

/// <summary>
/// Exposed API of the thread synchronization styme.
/// </summary>
public class ThreadSynchronization
{
    /// <summary>
    /// Static instance.
    /// </summary>
    private static ThreadSynchronization? _instance;

    /// <summary>
    /// Instance of the <see cref="ThreadSynchronizationSystem"/>.
    /// </summary>
    private readonly ThreadSynchronizationSystem _threadSynchronizationSystem;
    
    private ThreadSynchronization(ThreadSynchronizationSystem threadSynchronizationSystem)
    {
        _threadSynchronizationSystem = threadSynchronizationSystem;
    }

    /// <summary>
    /// Gets instance of the thread synchronization.
    /// </summary>
    public static ThreadSynchronization Instance
    {
        get
        {
            if (_instance is null)
            {

                throw new InvalidOperationException("Thread synchronization system is not initialized.");
            }

            return _instance;
        }
        private set => _instance = value;
    }

    /// <summary>
    /// Factory method used to create a new instance of the thread synchronization system.
    /// </summary>
    /// <param name="threadSynchronizationSystem"></param>
    internal static void CreateInstance(ThreadSynchronizationSystem threadSynchronizationSystem)
    {
        Instance = new ThreadSynchronization(threadSynchronizationSystem);
    }

    /// <summary>
    /// Runs the callback on the main thread.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public static Task RunAsync(Action action)
    {
        AsyncOperation operation = new(action);
        Instance._threadSynchronizationSystem.RegisterOperation(operation);

        return operation.Task;
    }

    /// <summary>
    /// Runs the callback on the main thread.
    /// </summary>
    /// <param name="func"></param>
    /// <typeparam name="TResult">Type fo result from the function.</typeparam>
    /// <returns></returns>
    public static Task<TResult> RunAsync<TResult>(Func<TResult> func)
    {
        AsyncOperation<TResult> operation = new(func);
        Instance._threadSynchronizationSystem.RegisterOperation(operation);

        return operation.Task;
    }
}