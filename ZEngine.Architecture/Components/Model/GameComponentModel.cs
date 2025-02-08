using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace ZEngine.Architecture.Components.Model;

/// <summary>
///     Implementation of the game component model.
/// </summary>
public abstract class GameComponentModel : IGameComponentModel
{
    /// <summary>
    ///     Dictionary of existing components.
    /// </summary>
    private readonly ConcurrentDictionary<Type, IGameComponent> _components = new();

    /// <summary>
    ///     Lock for the ordered list of types.
    /// </summary>
    private readonly object _listLock = new();

    /// <summary>
    ///     Instance of service provider to satisfy component dependencies.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    ///     Order of types as they were added to the dictionary.
    /// </summary>
    private readonly List<Type> _typesOrder = new();

    /// <summary>
    /// </summary>
    /// <param name="serviceProvider"></param>
    protected GameComponentModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    ///     Gets all components attached to this game object.
    /// </summary>
    protected ICollection<IGameComponent> Components
    {
        get
        {
            lock (_listLock)
            {
                return _typesOrder.Select(x => _components[x]).ToList();
            }
        }
    }

    /// <inheritdoc />
    public event EventHandler<IGameComponent>? ComponentAdded;

    /// <inheritdoc />
    public event EventHandler<IGameComponent>? ComponentRemoved;

    /// <inheritdoc />
    public IGameComponent AddComponent(Type componentType)
    {
        if (!componentType.IsAssignableTo(typeof(IGameComponent))) throw new ArgumentException($"Component of type {componentType.FullName} must be assignable to IGameComponent.");

        if (_components.ContainsKey(componentType)) throw new ArgumentException($"Component of type {componentType.FullName} already exists.");

        IGameComponent? component = (IGameComponent?)ActivatorUtilities.CreateInstance(_serviceProvider, componentType);
        if (component is null) throw new ArgumentException($"Component of type {componentType.FullName} could not be created.");

        _components.TryAdd(componentType, component);
        lock (_listLock)
        {
            _typesOrder.Add(componentType);
        }

        ComponentAdded?.Invoke(this, component);

        return component;
    }

    /// <inheritdoc />
    public TComponent AddComponent<TComponent>() where TComponent : IGameComponent
    {
        return (TComponent)AddComponent(typeof(TComponent));
    }

    /// <inheritdoc />
    public TComponent AddComponent<TComponent>(Action<TComponent> configure) where TComponent : IGameComponent
    {
        TComponent component = (TComponent)AddComponent(typeof(TComponent));
        configure.Invoke(component);

        return component;
    }

    /// <inheritdoc />
    public IGameComponent? GetComponent(Type componentType)
    {
        _components.TryGetValue(componentType, out IGameComponent? component);
        return component;
    }

    /// <inheritdoc />
    public IGameComponent GetRequiredComponent(Type componentType)
    {
        if (!_components.ContainsKey(componentType)) throw new ArgumentException($"Component of type {componentType.FullName} does not exists.");

        return _components[componentType];
    }

    /// <inheritdoc />
    public TComponent? GetComponent<TComponent>() where TComponent : IGameComponent
    {
        return (TComponent?)GetComponent(typeof(TComponent));
    }

    /// <inheritdoc />
    public TComponent GetRequiredComponent<TComponent>() where TComponent : IGameComponent
    {
        return (TComponent)GetRequiredComponent(typeof(TComponent));
    }

    /// <inheritdoc />
    public bool HasComponent(Type componentType)
    {
        return _components.ContainsKey(componentType);
    }

    /// <inheritdoc />
    public bool HasComponent<TComponent>()
    {
        return HasComponent(typeof(TComponent));
    }

    /// <inheritdoc />
    public bool RemoveComponent(Type type)
    {
        bool removed = _components.TryRemove(type, out IGameComponent? component);
        if (!removed) return false;

        lock (_listLock)
        {
            _typesOrder.Remove(type);
        }

        if (component is not null) ComponentRemoved?.Invoke(this, component);

        return true;
    }

    /// <inheritdoc />
    public bool RemoveComponent<TComponent>() where TComponent : IGameComponent
    {
        return RemoveComponent(typeof(TComponent));
    }

    /// <summary>
    ///     Clears the components collection.
    /// </summary>
    protected void ClearComponents()
    {
        _components.Clear();
        lock (_listLock)
        {
            _typesOrder.Clear();
        }
    }
}