using System.Collections.Concurrent;

namespace ZEngine.Architecture.Components.Model;

/// <summary>
/// Implementation of the game component model.
/// </summary>
public abstract class GameComponentModel : IGameComponentModel
{
    /// <inheritdoc />
    public event EventHandler<IGameComponent>? ComponentAdded;

    /// <inheritdoc />
    public event EventHandler<IGameComponent>? ComponentRemoved;

    /// <summary>
    /// Dictionary of existing components.
    /// </summary>
    private readonly ConcurrentDictionary<Type, IGameComponent> _components = new();

    /// <summary>
    /// Gets all components attached to this game object.
    /// </summary>
    protected ICollection<IGameComponent> Components => _components.Values;

    protected void ClearComponents()
    {
        _components.Clear();
    }

    /// <inheritdoc />
    public IGameComponent AddComponent(Type componentType)
    {
        if (!componentType.IsAssignableTo(typeof(IGameComponent)))
        {
            throw new ArgumentException($"Component of type {componentType.FullName} must be assignable to IGameComponent.");
        }

        if (_components.ContainsKey(componentType))
        {
            throw new ArgumentException($"Component of type {componentType.FullName} already exists.");
        }

        IGameComponent? component = (IGameComponent?) Activator.CreateInstance(componentType);
        if (component is null)
        {
            throw new ArgumentException($"Component of type {componentType.FullName} could not be created.");
        }

        _components.TryAdd(componentType, component);
        ComponentAdded?.Invoke(this, component);

        return component;
    }

    /// <inheritdoc />
    public TComponent AddComponent<TComponent>() where TComponent : IGameComponent
    {
        return (TComponent) AddComponent(typeof(TComponent));
    }

    /// <inheritdoc />
    public void SetComponent(IGameComponent component)
    {
        Type componentType = component.GetType();
        if (HasComponent(componentType))
        {
            RemoveComponent(componentType);
        }

        _components.TryAdd(componentType, component);
        ComponentAdded?.Invoke(this, component);
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
        if (!_components.ContainsKey(componentType))
        {
            throw new ArgumentException($"Component of type {componentType.FullName} does not exists.");
        }

        return _components[componentType];
    }

    /// <inheritdoc />
    public TComponent? GetComponent<TComponent>() where TComponent : IGameComponent
    {
        return (TComponent?) GetComponent(typeof(TComponent));
    }

    /// <inheritdoc />
    public TComponent GetRequiredComponent<TComponent>() where TComponent : IGameComponent
    {
        return (TComponent) GetRequiredComponent(typeof(TComponent));
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
        if (!removed)
        {
            return false;
        }

        if (component is not null)
        {
            ComponentRemoved?.Invoke(this, component);
        }

        return true;
    }

    /// <inheritdoc />
    public bool RemoveComponent<TComponent>() where TComponent : IGameComponent
    {
        return RemoveComponent(typeof(TComponent));
    }
}