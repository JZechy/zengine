using System.Collections;
using ZEngine.Systems.Inputs.Events.Paths;

namespace ZEngine.Systems.Inputs.Events.Collections;

/// <summary>
/// Generic collection of registered callbacks for a specific input path.
/// </summary>
/// <typeparam name="TDelegate"></typeparam>
/// <typeparam name="TContext"></typeparam>
public class GenericInputPathCollection<TDelegate, TContext> : IInputPathCollection where TDelegate : MulticastDelegate
{
    /// <summary>
    /// Collection of registered callbacks for a specific input path.
    /// </summary>
    private readonly HashSet<TDelegate> _callbacks = new();

    public GenericInputPathCollection(InputPath inputPath)
    {
        InputPath = inputPath;
    }

    /// <inheritdoc />
    public int Count
    {
        get
        {
            lock (SyncRoot)
            {
                return _callbacks.Count;
            }
        }
    }

    /// <inheritdoc />
    public bool IsSynchronized => true;

    /// <inheritdoc />
    public object SyncRoot { get; } = new();

    /// <inheritdoc />
    public InputPath InputPath { get; }

    /// <inheritdoc />
    public IEnumerator GetEnumerator()
    {
        lock (SyncRoot)
        {
            return _callbacks.GetEnumerator();
        }
    }

    /// <inheritdoc />
    public void CopyTo(Array array, int index)
    {
        lock (SyncRoot)
        {
            _callbacks.CopyTo((TDelegate[]) array, index);
        }
    }

    /// <inheritdoc />
    public void Add(MulticastDelegate callback)
    {
        lock (SyncRoot)
        {
            _callbacks.Add((TDelegate) callback);
        }
    }

    /// <inheritdoc />
    public void Remove(MulticastDelegate callback)
    {
        lock (SyncRoot)
        {
            _callbacks.Remove((TDelegate) callback);
        }
    }

    public void Invoke(object context)
    {
        lock (SyncRoot)
        {
            foreach (TDelegate callback in _callbacks)
            {
                callback.DynamicInvoke(new InputContext<TContext>
                {
                    InputPath = InputPath,
                    Context = (TContext) context
                });
            }
        }
    }
}