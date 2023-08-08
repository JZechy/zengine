using ZEngine.Architecture.Communication.Messages;
using ZEngine.Architecture.GameObjects;
using ZEngine.Core;

namespace ZEngine.Systems.GameObjects;

/// <summary>
/// System responsible for managing game objects.
/// </summary>
public class GameObjectSystem : IGameSystem
{
    /// <summary>
    /// Collection of all game objects available in the game.
    /// </summary>
    private readonly HashSet<IGameObject> _gameObjects = new();
    
    /// <summary>
    /// Collection of all game objects, that were created in the current frame.
    /// </summary>
    private readonly HashSet<IGameObject> _newGameObjects = new();
    
    /// <summary>
    /// Collection of all game objects, that were destroyed in the current frame.
    /// </summary>
    private readonly HashSet<IGameObject> _destroyedGameObjects = new();
    
    /// <summary>
    /// From the native systems, this system has the highest priority.
    /// </summary>
    public int Priority => 1;
    
    /// <summary>
    /// Every update, we wnat to iterate over active game objects and those, who are not children of any other game object.
    /// </summary>
    /// <remarks>
    /// Children should be updated by their parents.
    /// </remarks>
    private IEnumerable<IGameObject> ActiveRootObjects => _gameObjects.Where(x => x is { Active: true, Transform.Parent: null });

    /// <inheritdoc />
    public void Initialize()
    {
        ObjectManager.Create(this);
    }

    /// <inheritdoc />
    public void Update()
    {
        DestroyObjects();
        AddNewObjects();
        
        foreach (IGameObject gameObject in ActiveRootObjects)
        {
            gameObject.SendMessage(SystemMethod.Update);
        }
    }

    /// <inheritdoc />
    public void CleanUp()
    {
        foreach (IGameObject gameObject in ActiveRootObjects)
        {
            gameObject.SendMessage(SystemMethod.OnDestroy);
        }
    }

    /// <summary>
    /// Registers a game object in the system.
    /// </summary>
    /// <param name="gameObject"></param>
    public void Register(IGameObject gameObject)
    {
        gameObject.SendMessage(SystemMethod.Awake);
        _newGameObjects.Add(gameObject);
    }
    
    /// <summary>
    /// Marks a game object as destroyed.
    /// </summary>
    /// <param name="gameObject"></param>
    public void Unregister(IGameObject gameObject)
    {
        _destroyedGameObjects.Add(gameObject);
    }
    
    /// <summary>
    /// Adds new game objects to the collection of all game objects.
    /// </summary>
    private void AddNewObjects()
    {
        foreach (IGameObject gameObject in _newGameObjects)
        {
            gameObject.Active = true;
            _gameObjects.Add(gameObject);
        }
        
        _newGameObjects.Clear();
    }

    /// <summary>
    /// Destroyes game objects, that were marked as destroyed.
    /// </summary>
    private void DestroyObjects()
    {
        foreach (IGameObject gameObject in _destroyedGameObjects)
        {
            gameObject.SendMessage(SystemMethod.OnDestroy);
            _gameObjects.Remove(gameObject);
        }
        
        _destroyedGameObjects.Clear();
    }
}