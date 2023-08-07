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
        foreach (IGameObject gameObject in ActiveRootObjects)
        {
            gameObject.SendMessage(SystemMethod.Update);
        }
    }

    /// <summary>
    /// Creates a new instance of game object.
    /// </summary>
    /// <returns></returns>
    public IGameObject Instantiate()
    {
        GameObject gameObject = new();
        _gameObjects.Add(gameObject);

        return gameObject;
    }
}