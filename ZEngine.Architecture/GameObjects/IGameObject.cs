using ZEngine.Architecture.Communication.Messages;
using ZEngine.Architecture.Components;
using ZEngine.Architecture.Components.Model;

namespace ZEngine.Architecture.GameObjects;

/// <summary>
///     Interface providing basic game object functionality.
/// </summary>
public interface IGameObject : IMessageReceiver, IGameComponentModel
{
    /// <summary>
    ///     Name of this game object.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    ///     Enables or disables the active state of this game object.
    /// </summary>
    /// <remarks>
    ///     If game object is not active, it will not be updated or rendered.
    /// </remarks>
    bool Active { get; set; }

    /// <summary>
    ///     Gets access to the transform component.
    /// </summary>
    Transform Transform { get; }
}