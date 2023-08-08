using ZEngine.Architecture.GameObjects;

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
        set => _objectManager = value;
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
        GameObject gameObject = new();
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
        GameObject gameObject = new();
        gameObject.Transform.SetParent(parent.Transform);
        Instance._gameObjectSystem.Register(gameObject);

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