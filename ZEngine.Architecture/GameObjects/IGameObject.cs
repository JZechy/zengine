using ZEngine.Architecture.Communication.Messages;
using ZEngine.Architecture.Components;

namespace ZEngine.Architecture.GameObjects;

/// <summary>
/// Interface providing basic game object functionality.
/// </summary>
public interface IGameObject : IMessageReceiver
{
    /// <summary>
    /// Name of this game object.
    /// </summary>
    string Name { get; set; }
    
    /// <summary>
    /// Enables or disables the active state of this game object.
    /// </summary>
    /// <remarks>
    /// If game object is not active, it will not be updated or rendered.
    /// </remarks>
    bool Active { get; set; }
    
    /// <summary>
    /// Gets readonly collection of components attached to this game object.
    /// </summary>
    IReadOnlyCollection<IGameComponent> Components { get; }

    /// <summary>
    /// Creates a new instance of component of type <typeparamref name="TComponent"/> and attaches it to this game object.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    TComponent AddComponent<TComponent>() where TComponent : IGameComponent;
    
    /// <summary>
    /// Attaches existing component to this game object.
    /// </summary>
    /// <param name="component"></param>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    TComponent AddComponent<TComponent>(TComponent component) where TComponent : IGameComponent;
    
    /// <summary>
    /// Gets component of type <typeparamref name="TComponent"/> attached to this game object.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    TComponent GetComponent<TComponent>() where TComponent : IGameComponent;
    
    /// <summary>
    /// Gets component of type <paramref name="componentType"/> attached to this game object.
    /// </summary>
    /// <param name="componentType"></param>
    /// <returns></returns>
    object GetComponent(Type componentType);

    /// <summary>
    /// Checks if this game object has component of type <typeparamref name="TComponent"/> attached.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    bool HasComponent<TComponent>() where TComponent : IGameComponent;
    
    /// <summary>
    /// Checks if this game object has component of type <paramref name="componentType"/> attached.
    /// </summary>
    /// <param name="componentType"></param>
    /// <returns></returns>
    bool HasComponent(Type componentType);
    
    /// <summary>
    /// Removes component of type <typeparamref name="TComponent"/> from this game object.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    bool RemoveComponent<TComponent>() where TComponent : IGameComponent;

    /// <summary>
    /// Removes component of type <paramref name="type"/> from this game object.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    bool RemoveComponent(Type type);
}