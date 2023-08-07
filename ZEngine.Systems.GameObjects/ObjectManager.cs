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
    
    internal static void Create(GameObjectSystem gameObjectSystem)
    {
        Instance = new ObjectManager(gameObjectSystem);
    }
    
    /// <summary>
    /// Creates a new instance of game object.
    /// </summary>
    /// <returns></returns>
    public static IGameObject Instantiate()
    {
        return Instance._gameObjectSystem.Instantiate();
    }
}