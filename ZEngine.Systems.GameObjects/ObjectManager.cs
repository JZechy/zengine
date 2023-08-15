using ZEngine.Architecture.Components;
using ZEngine.Architecture.GameObjects;
using ZEngine.Systems.GameObjects.Prefabs;

namespace ZEngine.Systems.GameObjects;

/// <summary>
/// Exposed API for managing game objects.
/// </summary>
public class ObjectManager
{
    /// <summary>
    /// Current instance of <see cref="ObjectManager"/>.
    /// </summary>
    private static ObjectManager? _objectManager;
    
    /// <summary>
    /// Reference to the <see cref="GameObjectSystem"/> instance.
    /// </summary>
    private readonly GameObjectSystem _gameObjectSystem;

    private ObjectManager(GameObjectSystem gameObjectSystem)
    {
        _gameObjectSystem = gameObjectSystem;
    }

    /// <summary>
    /// Current instance of <see cref="ObjectManager"/>.
    /// </summary>
    public static ObjectManager Instance
    {
        get
        {
            if (_objectManager is null)
            {
                throw new InvalidOperationException("Object manager is not initialized.");
            }

            return _objectManager;
        }
        private set => _objectManager = value;
    }
    
    /// <summary>
    /// Factory method for creating a new instance of <see cref="ObjectManager"/>.
    /// </summary>
    /// <remarks>
    /// Object manager is instantiated by <see cref="GameObjectSystem"/> during its initialization.
    /// </remarks>
    /// <param name="gameObjectSystem"></param>
    internal static void CreateInstance(GameObjectSystem gameObjectSystem)
    {
        Instance = new ObjectManager(gameObjectSystem);
    }
    
    /// <summary>
    /// Creates a new instance of game object.
    /// </summary>
    /// <returns></returns>
    public static IGameObject Create()
    {
        GameObject gameObject = new("New Game Object", true);
        gameObject.AddComponent<Transform>();
        Instance._gameObjectSystem.Register(gameObject);

        return gameObject;
    }
    
    /// <summary>
    /// Creates a new instance of game object as a child of another game object.
    /// </summary>
    /// <param name="parent">Parent game object.</param>
    /// <returns></returns>
    public static IGameObject Create(IGameObject parent)
    {
        GameObject gameObject = new("New Game Object", true);
        gameObject.AddComponent<Transform>();
        gameObject.Transform.SetParent(parent.Transform);
        Instance._gameObjectSystem.Register(gameObject);

        return gameObject;
    }

    /// <summary>
    /// Creates a new instance of game object from a prefab.
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static IGameObject FromPrefab(IPrefab prefab)
    {
        GameObject gameObject = new()
        {
            Name = prefab.Name
        };
        
        foreach (IGameComponent component in prefab.Components)
        {
            gameObject.AddComponent(component.Clone());
        }
        
        if (!gameObject.HasComponent<Transform>())
        {
            gameObject.AddComponent<Transform>();
        }
        
        Instance._gameObjectSystem.Register(gameObject);

        return gameObject;
    }

    /// <summary>
    /// Creates a new instance of game object from a prefab as a child of another game object.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static IGameObject FromPrefab(IPrefab prefab, IGameObject parent)
    {
        IGameObject gameObject = FromPrefab(prefab);
        gameObject.Transform.SetParent(parent.Transform);

        return gameObject;
    }

    /// <summary>
    /// Destroys a game object.
    /// </summary>
    /// <param name="gameObject"></param>
    public static void Destroy(IGameObject gameObject)
    {
        Instance._gameObjectSystem.Unregister(gameObject);
    }
}