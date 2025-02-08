using ZEngine.Core;
using ZEngine.Testing.System.Watchers;

namespace ZEngine.Testing.System;

/// <summary>
///     This is used to propagate calls from tests inside the engine flow.
/// </summary>
public class TestingSystem : IGameSystem
{
    /// <summary>
    ///     Object used to synchronize calls.
    /// </summary>
    private static readonly object SyncRoot = new();

    /// <summary>
    ///     Collection of watchers that are being awaited.
    /// </summary>
    private readonly HashSet<IWatcher> _watchers = new();

    /// <inheritdoc />
    public int Priority => 99;

    /// <inheritdoc />
    public void Initialize()
    {
        TestManager.CreateInstance(this);
    }

    /// <inheritdoc />
    public void Update()
    {
        List<IWatcher> completed = new();

        lock (SyncRoot)
        {
            foreach (IWatcher watcher in _watchers)
            {
                watcher.Check();

                if (watcher.Task.IsCompleted) completed.Add(watcher);
            }

            foreach (IWatcher watcher in completed) _watchers.Remove(watcher);
        }
    }

    /// <inheritdoc />
    public void CleanUp()
    {
        lock (SyncRoot)
        {
            foreach (IWatcher watcher in _watchers) watcher.Cancel();

            _watchers.Clear();
        }
    }

    /// <summary>
    ///     Registers a new watcher.
    /// </summary>
    /// <param name="watcher"></param>
    public void RegisterWatcher(IWatcher watcher)
    {
        lock (SyncRoot)
        {
            _watchers.Add(watcher);
        }
    }
}