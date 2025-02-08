using ZEngine.Architecture.Communication.Events;
using ZEngine.Architecture.GameObjects;

namespace ZEngine.Systems.GameObjects.Events;

/// <summary>
///     Event message notifying about removing a game object from the game.
/// </summary>
public class GameObjectRemoved : IEventMessage
{
    public GameObjectRemoved(IGameObject gameObject)
    {
        GameObject = gameObject;
    }

    /// <summary>
    ///     Instance of removed game object.
    /// </summary>
    public IGameObject GameObject { get; }
}