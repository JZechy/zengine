using ZEngine.Architecture.GameObjects;
using ZEngine.Systems.GameObjects.Factory;

namespace ZEngine.Systems.GameObjects;

/// <summary>
/// Exposed API for managing game objects.
/// </summary>
public class GameObjectManager
{
    /// <summary>
    /// Current instance of <see cref="GameObjectManager"/>.
    /// </summary>
    private static GameObjectManager? _objectManager;

    /// <summary>
    /// Reference to the <see cref="GameObjectSystem"/> instance.
    /// </summary>
    private readonly GameObjectSystem _gameObjectSystem;

    /// <summary>
    /// Service provider is used to be passed to game objects, to satisfy game component dependencies.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    private GameObjectManager(GameObjectSystem gameObjectSystem, IServiceProvider serviceProvider)
    {
        _gameObjectSystem = gameObjectSystem;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Current instance of <see cref="GameObjectManager"/>.
    /// </summary>
    public static GameObjectManager Instance
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
    /// Factory method for creating a new instance of <see cref="GameObjectManager"/>.
    /// </summary>
    /// <remarks>
    /// Object manager is instantiated by <see cref="GameObjectSystem"/> during its initialization.
    /// </remarks>
    /// <param name="gameObjectSystem"></param>
    /// <param name="serviceProvider"></param>
    internal static void CreateInstance(GameObjectSystem gameObjectSystem, IServiceProvider serviceProvider)
    {
        Instance = new GameObjectManager(gameObjectSystem, serviceProvider);
    }

    /// <summary>
    /// Creates a new instance of game object.
    /// </summary>
    /// <param name="active">Controls whether the created game object will be active.</param>
    /// <returns></returns>
    public static IGameObject Create(bool active = true)
    {
        GameObject gameObject = new(Instance._serviceProvider, "New Game Object", active);
        Instance._gameObjectSystem.Register(gameObject);

        return gameObject;
    }

    /// <summary>
    /// Creates a new instance of game object as a child of another game object.
    /// </summary>
    /// <param name="parent">Parent game object.</param>
    /// <param name="active">Controls whether the created game object will be active.</param>
    /// <returns></returns>
    public static IGameObject Create(IGameObject parent, bool active = true)
    {
        GameObject gameObject = new(Instance._serviceProvider, "New Game Object", active);
        gameObject.Transform.SetParent(parent.Transform);
        Instance._gameObjectSystem.Register(gameObject);

        return gameObject;
    }

    /// <summary>
    /// Creates a game object from a prefab factory.
    /// </summary>
    /// <param name="gameObjectFactory"></param>
    /// <returns></returns>
    public static IGameObject FromFactory(IGameObjectFactory gameObjectFactory)
    {
        IGameObject gameObject = Create(false);
        gameObjectFactory.Configure(gameObject);

        return gameObject;
    }

    /// <summary>
    /// Creates a game object from a prefab factory as a child of another game object.
    /// </summary>
    /// <param name="gameObjectFactory"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static IGameObject FromFactory(IGameObjectFactory gameObjectFactory, IGameObject parent)
    {
        IGameObject gameObject = Create(parent, false);
        gameObjectFactory.Configure(gameObject);

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