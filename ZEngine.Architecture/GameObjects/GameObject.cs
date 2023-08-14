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

    /// <summary>
    /// Current state of game object.
    /// </summary>
    private bool _active;

    /// <summary>
    /// Instance of transform object.
    /// </summary>
    private Transform? _transform;

    public GameObject(string name = "New Game Object", bool active = false)
    {
        _messageHandler = new MessageHandler(this);
        Name = name;
        _active = active;
    }

    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public bool Active
    {
        get => _active;
        set
        {
            _active = value;
            SendMessage(_active ? SystemMethod.OnEnable : SystemMethod.OnDisable);
        }
    }

    /// <inheritdoc />
    public Transform Transform => _transform ??= GetRequiredComponent<Transform>();

    /// <summary>
    /// For the update of children, we want only active game objects.
    /// </summary>
    private IEnumerable<IGameObject> ActiveChildren => Transform.Select(x => x.GameObject).Where(x => x.Active);
    
    /// <summary>
    /// For the update of components, we want only enabled components.
    /// </summary>
    private IEnumerable<IGameComponent> EnabledComponents => _components.Values.Where(x => x.Enabled);

    /// <inheritdoc />
    public IReadOnlyCollection<IGameComponent> Components => _components.Values.ToImmutableHashSet();

    /// <inheritdoc />
    public TComponent AddComponent<TComponent>() where TComponent : IGameComponent
    {
        return (TComponent) AddComponent(typeof(TComponent));
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
            throw new ArgumentException($"Component of type {componentType.FullName} already exists on {Name}.");
        }

        IGameComponent? component = (IGameComponent?) Activator.CreateInstance(componentType);
        if (component is null)
        {
            throw new ArgumentException($"Component of type {componentType.FullName} could not be created.");
        }

        component.GameObject = this;
        _components.Add(componentType, component);
        if (Active)
        {
            component.SendMessage(SystemMethod.Awake);
            component.SendMessage(SystemMethod.OnEnable);
        }

        return component;
    }

    /// <inheritdoc />
    public void AddComponent(IGameComponent component)
    {
        Type componentType = component.GetType();
        if (_components.ContainsKey(componentType))
        {
            throw new ArgumentException($"Component of type {componentType.FullName} already exists on {Name}.");
        }
        
        component.GameObject = this;
        _components.Add(componentType, component);
        if (!Active)
        {
            return;
        }
        
        component.SendMessage(SystemMethod.Awake);
        component.SendMessage(SystemMethod.OnEnable);
    }

    /// <inheritdoc />
    public TComponent? GetComponent<TComponent>() where TComponent : IGameComponent
    {
        return (TComponent?) GetComponent(typeof(TComponent));
    }

    /// <inheritdoc />
    public IGameComponent? GetComponent(Type componentType)
    {
        _components.TryGetValue(componentType, out IGameComponent? component);
        return component;
    }

    /// <inheritdoc />
    public TComponent GetRequiredComponent<TComponent>() where TComponent : IGameComponent
    {
        return (TComponent) GetRequiredComponent(typeof(TComponent));
    }

    /// <inheritdoc />
    public IGameComponent GetRequiredComponent(Type componentType)
    {
        if (!_components.ContainsKey(componentType))
        {
            throw new ArgumentException($"Component of type {componentType.FullName} does not exist on {Name}.");
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
        return RemoveComponent(typeof(TComponent));
    }

    /// <inheritdoc />
    public bool RemoveComponent(Type type)
    {
        IGameComponent component = GetRequiredComponent(type);
        component.SendMessage(SystemMethod.OnDestroy);

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

    /// <summary>
    /// If the game object was created with existing components, we need to call Awake on them.
    /// </summary>
    private void Awake()
    {
        foreach (IGameComponent component in _components.Values)
        {
            component.SendMessage(SystemMethod.Awake);
        }
    }

    /// <summary>
    /// Enables all registered components.
    /// </summary>
    private void OnEnable()
    {
        foreach (IGameComponent component in _components.Values)
        {
            component.Enabled = true;
        }
    }

    /// <summary>
    /// Disables all registered components.
    /// </summary>
    private void OnDisable()
    {
        foreach (IGameComponent component in _components.Values)
        {
            component.Enabled = false;
        }
    }

    /// <summary>
    /// Updates all child game objects and components.
    /// </summary>
    private void Update()
    {
        foreach (IGameObject gameObject in ActiveChildren)
        {
            gameObject.SendMessage(SystemMethod.Update);
        }

        foreach (IGameComponent gameComponent in EnabledComponents)
        {
            gameComponent.SendMessage(SystemMethod.Update);
        }
    }

    /// <summary>
    /// Detroys all child game objects and components.
    /// </summary>
    private void OnDestroy()
    {
        foreach (IGameObject gameObject in Transform.Select(x => x.GameObject))
        {
            gameObject.SendMessage(SystemMethod.OnDestroy);
        }

        foreach (IGameComponent gameComponent in _components.Values)
        {
            gameComponent.SendMessage(SystemMethod.OnDestroy);
        }
        
        _components.Clear();
    }
}