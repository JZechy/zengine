using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace ZEngine.Systems.Inputs.Events.Collections;

/// <summary>
/// Generic base implementation for different kinds of callback managers.
/// </summary>
/// <typeparam name="TContext">Type of the context inside <see cref="InputContext{TContext}"/></typeparam>
public abstract class GenericCallbackManager<TContext> : IDeviceCallbackManager
{
    /// <summary>
    /// Dictionary mapping input path to callbacks.
    /// </summary>
    private readonly ConcurrentDictionary<string, HashSet<Action<InputContext<TContext>>>> _callbacks = new();

    private ILogger<IDeviceCallbackManager> _logger;

    public GenericCallbackManager(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<IDeviceCallbackManager>();
    }

    /// <inheritdoc />
    public Type ContextType { get; } = typeof(InputContext<TContext>);

    /// <inheritdoc />
    public void Register(string path, Delegate callback)
    {
        if (!_callbacks.ContainsKey(path))
        {
            _callbacks.TryAdd(path, new HashSet<Action<InputContext<TContext>>>());
        }

        _callbacks[path].Add((Action<InputContext<TContext>>) callback);
    }

    /// <inheritdoc />
    public void Unregister(string path, Delegate callback)
    {
        if (!_callbacks.ContainsKey(path))
        {
            return;
        }

        _callbacks[path].Remove((Action<InputContext<TContext>>) callback);
    }

    /// <inheritdoc />
    public void Invoke(InputContext inputContext)
    {
        if (_callbacks.TryGetValue(inputContext.InputPath.Path, out HashSet<Action<InputContext<TContext>>>? callbacks))
        {
            foreach (Action<InputContext<TContext>> callback in callbacks)
            {
                try
                {
                    callback.Invoke((InputContext<TContext>) inputContext);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "An error occurred while invoking input callback for {Path}.", inputContext.InputPath.Path);
                }
            }
        }
    }
}