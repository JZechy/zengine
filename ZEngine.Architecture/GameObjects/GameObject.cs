using System.Collections.Immutable;
using ZEngine.Architecture.Communication.Messages;
using ZEngine.Architecture.Components;

namespace ZEngine.Architecture.GameObjects;

/// <summary>
/// Implementation of standard game object.
/// </summary>
public class GameObject : IGameObject
{
    /// <summary>
    /// Internal hash set of existing components.
    /// </summary>
    private readonly Dictionary<Type, IGameComponent> _components = new();

    /// <summary>
    /// Internal message handler.
    /// </summary>
    private readonly MessageHandler _messageHandler;

    public GameObject()
    {
        _messageHandler = new MessageHandler(this);
    }

    public GameObject(string name)
    {
        _messageHandler = new MessageHandler(this);
        Name = name;
    }

    public GameObject(string name, bool active)
    {
        _messageHandler = new MessageHandler(this);
        Name = name;
        Active = active;
    }
    
    /// <inheritdoc />
    public string Name { get; set; } = "New Game Object";

    /// <inheritdoc />
    public bool Active { get; set; } = true;

    /// <inheritdoc />
    public IReadOnlyCollection<IGameComponent> Components => _components.Values.ToImmutableHashSet();

    /// <inheritdoc />
    public TComponent AddComponent<TComponent>() where TComponent : IGameComponent
    {
        return (TComponent) AddComponent(typeof(TComponent));
    }

    /// <inheritdoc />
    public object AddComponent(Type componentType)
    {
        if (!componentType.IsAssignableTo(typeof(IGameComponent)))
        {
            throw new ArgumentException("Component type must be assignable to IGameComponent.");
        }

        if (_components.ContainsKey(componentType))
        {
            throw new ArgumentException($"Component of type {componentType.FullName} already exists on {Name}.");
        }
        
        IGameComponent? component = (IGameComponent?) Activator.CreateInstance(componentType);
        if (component is null)
        {
            throw new ArgumentException($"Component of type {componentType.FullName} could not be created.");
        }
        _components.Add(componentType, component);
        
        return component;
    }

    /// <inheritdoc />
    public TComponent GetComponent<TComponent>() where TComponent : IGameComponent
    {
        return (TComponent) GetComponent(typeof(TComponent));
    }

    /// <inheritdoc />
    public object GetComponent(Type componentType)
    {
        if (!HasComponent(componentType))
        {
            throw new ArgumentException("Component of type {componentType.FullName} does not exist on {Name}.");
        }
        
        return _components[componentType];
    }

    /// <inheritdoc />
    public bool HasComponent<TComponent>() where TComponent : IGameComponent
    {
        return HasComponent(typeof(TComponent));
    }

    /// <inheritdoc />
    public bool HasComponent(Type componentType)
    {
        return _components.ContainsKey(componentType);
    }

    /// <inheritdoc />
    public bool RemoveComponent<TComponent>() where TComponent : IGameComponent
    {
        return _components.Remove(typeof(TComponent));
    }

    /// <inheritdoc />
    public bool RemoveComponent(Type type)
    {
        return _components.Remove(type);
    }

    /// <inheritdoc />
    public void SendMessage(string target)
    {
        throw new NotSupportedException("Game Object can be messaged only by System Methods.");
    }

    /// <inheritdoc />
    public void SendMessage(SystemMethod systemTarget)
    {
        _messageHandler.Handle(systemTarget);
    }
}