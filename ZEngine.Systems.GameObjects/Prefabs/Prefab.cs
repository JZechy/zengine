using ZEngine.Architecture.Components;
using ZEngine.Architecture.GameObjects;

namespace ZEngine.Systems.GameObjects.Prefabs;

/// <summary>
/// Prefab is a class that serves for programatic creation of a <see cref="GameObject"/>
/// </summary>
public class Prefab : IPrefab
{
    /// <summary>
    /// List of components to add to the <see cref="GameObject"/> described by this <see cref="IPrefab"/>
    /// </summary>
    private readonly Dictionary<Type, IGameComponent> _components = new();

    /// <summary>
    /// Gets/sets the name of the <see cref="GameObject"/> described by this <see cref="IPrefab"/>
    /// </summary>
    public string Name { get; set; } = "Prefab Clone";

    /// <inheritdoc />
    public TComponent AddComponent<TComponent>() where TComponent : class, IGameComponent
    {
        return (TComponent) AddComponent(typeof(TComponent));
    }

    /// <inheritdoc />
    public void AddComponent<TComponent>(Action<TComponent> init) where TComponent : class, IGameComponent
    {
        TComponent component = (TComponent) AddComponent(typeof(TComponent));
        init(component);
    }

    /// <inheritdoc />
    public IGameComponent AddComponent(Type type)
    {
        if (_components.ContainsKey(type))
        {
            throw new ArgumentException($"Prefab already contains a component of type {type.Name}");
        }

        IGameComponent? component = (IGameComponent?) Activator.CreateInstance(type);
        if (component is null)
        {
            throw new ArgumentException($"Could not create component of type {type.Name}");
        }

        _components.Add(type, component);

        return component;
    }

    /// <inheritdoc />
    public bool TryGetComponent<TComponent>(out TComponent? component) where TComponent : class, IGameComponent
    {
        if (TryGetComponent(typeof(TComponent), out IGameComponent? gameComponent))
        {
            component = (TComponent) gameComponent;
            return true;
        }

        component = null;
        return false;
    }

    /// <inheritdoc />
    public bool TryGetComponent(Type type, out IGameComponent component)
    {
        if (_components.TryGetValue(type, out IGameComponent? gameComponent))
        {
            component = gameComponent;
            return true;
        }

        component = null!;
        return false;
    }

    /// <inheritdoc />
    public bool TryRemoveComponent<TComponent>() where TComponent : class, IGameComponent
    {
        return TryRemoveComponent(typeof(TComponent));
    }

    /// <inheritdoc />
    public bool TryRemoveComponent(Type type)
    {
        return _components.Remove(type);
    }

    /// <inheritdoc />
    public IGameObject Instantiate()
    {
        GameObject gameObject = new()
        {
            Name = Name
        };

        foreach (IGameComponent component in _components.Values)
        {
            gameObject.AddComponent(component.Clone());
        }
        
        return gameObject;
    }
}