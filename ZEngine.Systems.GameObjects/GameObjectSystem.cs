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
    /// Thread-safe lock for adding new game objects.
    /// </summary>
    private static readonly object AddingLock = new();
    
    /// <summary>
    /// Thread-safe lock for removing game objects.
    /// </summary>
    private static readonly object RemovingLock = new();
    
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
        ObjectManager.CreateInstance(this);
    }

    /// <inheritdoc />
    public void Update()
    {
        DestroyObjects();
        AddNewObjects();
        
        foreach (IGameObject gameObject in ActiveRootObjects)
        {
            try
            {
                gameObject.SendMessage(SystemMethod.Update);
            }
            catch (Exception)
            {
                // TODO: Log exception.
            }
        }
    }

    /// <inheritdoc />
    public void CleanUp()
    {
        foreach (IGameObject gameObject in ActiveRootObjects)
        {
            try
            {
                gameObject.SendMessage(SystemMethod.OnDestroy);
            }
            catch (Exception)
            {
                // TODO: Log exception.
            }
        }
    }

    /// <summary>
    /// Registers a game object in the system.
    /// </summary>
    /// <param name="gameObject"></param>
    public void Register(IGameObject gameObject)
    {
        try
        {
            gameObject.SendMessage(SystemMethod.Awake);
        }
        catch (Exception)
        {
            // TODO: Log exception.
        }

        lock (AddingLock)
        {
            _newGameObjects.Add(gameObject);
        }
    }
    
    /// <summary>
    /// Marks a game object as destroyed.
    /// </summary>
    /// <param name="gameObject"></param>
    public void Unregister(IGameObject gameObject)
    {
        lock (RemovingLock)
        {
            _destroyedGameObjects.Add(gameObject);
        }
    }
    
    /// <summary>
    /// Adds new game objects to the collection of all game objects.
    /// </summary>
    private void AddNewObjects()
    {
        lock (AddingLock)
        {
            foreach (IGameObject gameObject in _newGameObjects)
            {
                try
                {
                    gameObject.Active = true;
                }
                catch (Exception)
                {
                    // TODO: Log exception.
                }

                _gameObjects.Add(gameObject);
            }

            _newGameObjects.Clear();
        }
    }

    /// <summary>
    /// Destroyes game objects, that were marked as destroyed.
    /// </summary>
    private void DestroyObjects()
    {
        lock (RemovingLock)
        {
            foreach (IGameObject gameObject in _destroyedGameObjects)
            {
                try
                {
                    gameObject.SendMessage(SystemMethod.OnDestroy);
                }
                catch (Exception)
                {
                    // TODO: Log exception.
                }

                _gameObjects.Remove(gameObject);
            }

            _destroyedGameObjects.Clear();
        }
    }
}