using ZEngine.Architecture.Communication.Events;
using ZEngine.Architecture.GameObjects;

namespace ZEngine.Systems.GameObjects.Events;

/// <summary>
/// Event message notifying about adding a new game object to the game.
/// </summary>
public class GameObjectAdded : IEventMessage
{
    public GameObjectAdded(IGameObject gameObject)
    {
        GameObject = gameObject;
    }
    
    /// <summary>
    /// Instance of added game object.
    /// </summary>
    public IGameObject GameObject { get; }
}