using ZEngine.Architecture.Components;
using ZEngine.Architecture.GameObjects;
using ZEngine.Systems.GameObjects.Prefabs;
using ZEngine.Systems.GameObjects.Prefabs.Factory;

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
    /// <returns></returns>
    public static IGameObject Create()
    {
        GameObject gameObject = new(Instance._serviceProvider, "New Game Object", true);
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
        GameObject gameObject = new(Instance._serviceProvider, "New Game Object", true);
        gameObject.Transform.SetParent(parent.Transform);
        Instance._gameObjectSystem.Register(gameObject);

        return gameObject;
    }

    /// <summary>
    /// Creates a game object from a prefab factory.
    /// </summary>
    /// <param name="prefabFactory"></param>
    /// <returns></returns>
    public static IGameObject FromFactory(IPrefabFactory prefabFactory)
    {
        Prefab prefab = new(Instance._serviceProvider);
        prefabFactory.Configure(prefab);

        return FromPrefab(prefab);
    }

    /// <summary>
    /// Creates a game object from a prefab factory as a child of another game object.
    /// </summary>
    /// <param name="prefabFactory"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static IGameObject FromFactory(IPrefabFactory prefabFactory, IGameObject parent)
    {
        Prefab prefab = new(Instance._serviceProvider);
        prefabFactory.Configure(prefab);

        return FromPrefab(prefab, parent);
    }

    /// <summary>
    /// Allows to create a game object from a prefab using a fluent API.
    /// </summary>
    /// <param name="init">Initialization callback used compose prefab dependencies.</param>
    /// <returns></returns>
    public static IGameObject FromPrefab(Action<IPrefab> init)
    {
        Prefab prefab = new(Instance._serviceProvider);
        init.Invoke(prefab);

        return FromPrefab(prefab);
    }

    /// <summary>
    /// Creates a game object from a prefab using a fluent API as a child of another game object.
    /// </summary>
    /// <param name="init"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static IGameObject FromPrefab(Action<IPrefab> init, IGameObject parent)
    {
        Prefab prefab = new(Instance._serviceProvider);
        init.Invoke(prefab);

        return FromPrefab(prefab, parent);
    }
    
    /// <summary>
    /// Creates a new instance of game object from a prefab.
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static IGameObject FromPrefab(IPrefab prefab)
    {
        GameObject gameObject = new(Instance._serviceProvider)
        {
            Name = prefab.Name
        };
        
        foreach (IGameComponent component in prefab.PrefabComponents)
        {
            gameObject.SetComponent(component.Clone());
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