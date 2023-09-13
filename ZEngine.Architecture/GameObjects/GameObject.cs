using ZEngine.Architecture.Communication.Messages;
using ZEngine.Architecture.Components;
using ZEngine.Architecture.Components.Model;

namespace ZEngine.Architecture.GameObjects;

/// <summary>
/// Implementation of standard game object.
/// </summary>
public class GameObject : GameComponentModel, IGameObject
{
    /// <summary>
    /// Internal message handler.
    /// </summary>
    private readonly MessageHandler _messageHandler;

    /// <summary>
    /// Current state of game object.
    /// </summary>
    private bool _active;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="active"></param>
    public GameObject(string name = "New Game Object", bool active = false)
    {
        _messageHandler = new MessageHandler(this);
        Name = name;
        _active = active;
        
        // Register callbacks
        ComponentAdded += OnComponentAdded;
        ComponentRemoved += OnComponentRemoved;
        
        // Add basic components
        AddComponent<Transform>();
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
    public Transform Transform { get; private set; } = null!;

    /// <summary>
    /// For the update of children, we want only active game objects.
    /// </summary>
    private IEnumerable<IGameObject> ActiveChildren => Transform.Select(x => x.GameObject).Where(x => x.Active);
    
    /// <summary>
    /// For the update of components, we want only enabled components.
    /// </summary>
    private IEnumerable<IGameComponent> EnabledComponents => Components.Where(x => x.Enabled);

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
    
    private void OnComponentRemoved(object? sender, IGameComponent component)
    {
        component.SendMessage(SystemMethod.OnDestroy);
    }

    private void OnComponentAdded(object? sender, IGameComponent component)
    {
        component.GameObject = this;

        // If we are overriding (or adding during init) transform, we need to update it.
        if (component is Transform transform)
        {
            Transform = transform;
        }

        if (!Active)
        {
            return;
        }
        
        component.SendMessage(SystemMethod.Awake);
        component.SendMessage(SystemMethod.OnEnable);
    }

    /// <summary>
    /// If the game object was created with existing components, we need to call Awake on them.
    /// </summary>
    private void Awake()
    {
        foreach (IGameComponent component in Components)
        {
            component.SendMessage(SystemMethod.Awake);
        }
    }

    /// <summary>
    /// Enables all registered components.
    /// </summary>
    private void OnEnable()
    {
        foreach (IGameComponent component in Components)
        {
            component.Enabled = true;
        }
    }

    /// <summary>
    /// Disables all registered components.
    /// </summary>
    private void OnDisable()
    {
        foreach (IGameComponent component in Components)
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

        foreach (IGameComponent gameComponent in Components)
        {
            gameComponent.SendMessage(SystemMethod.OnDestroy);
        }
        
        ClearComponents();
    }
}