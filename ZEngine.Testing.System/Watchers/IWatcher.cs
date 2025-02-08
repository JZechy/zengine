namespace ZEngine.Testing.System.Watchers;

/// <summary>
///     Watcher is a testing component, that creates a task awaiting certain condition to be met.
/// </summary>
public interface IWatcher
{
    /// <summary>
    ///     Task that is being awaited inside the test.
    /// </summary>
    Task Task { get; }

    /// <summary>
    ///     Checks if the condition are met.
    /// </summary>
    void Check();

    /// <summary>
    ///     Indicates that the watcher is no longer needed.
    /// </summary>
    void Cancel();
}