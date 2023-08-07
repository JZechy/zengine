using ZEngine.Architecture.Communication.Messages;
using ZEngine.Architecture.GameObjects;

namespace ZEngine.Architecture.Components;

/// <summary>
/// Interface describing basic component attached to game object.
/// </summary>
public interface IGameComponent : IMessageReceiver
{
    /// <summary>
    /// Enables or disables this component.
    /// </summary>
    /// <remarks>
    /// If component is not enabled, it will not be updated or rendered.
    /// </remarks>
    bool Enabled { get; set; }
    
    /// <summary>
    /// Game object to which this component is attached.
    /// </summary>
    IGameObject GameObject { get; set; }
}