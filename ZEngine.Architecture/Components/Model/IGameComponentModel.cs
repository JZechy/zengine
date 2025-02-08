namespace ZEngine.Architecture.Components.Model;

/// <summary>
///     This interface describes all possible ways, how to work with game component model on a object.
/// </summary>
public interface IGameComponentModel
{
    /// <summary>
    ///     Event fired when new component is added to the game object.
    /// </summary>
    event EventHandler<IGameComponent>? ComponentAdded;

    /// <summary>
    ///     Event fired when component is removed from the game object.
    /// </summary>
    event EventHandler<IGameComponent>? ComponentRemoved;

    #region Add components

    /// <summary>
    ///     Adds new component of type <see cref="componentType" /> to the game object.
    /// </summary>
    /// <param name="componentType"></param>
    /// <returns></returns>
    IGameComponent AddComponent(Type componentType);

    /// <summary>
    ///     Adds new component of type <see cref="TComponent" /> to the game object.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    TComponent AddComponent<TComponent>() where TComponent : IGameComponent;

    /// <summary>
    ///     Adds new component of type <see cref="TComponent" /> to the game object and configures it.
    /// </summary>
    /// <param name="configure"></param>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    TComponent AddComponent<TComponent>(Action<TComponent> configure) where TComponent : IGameComponent;

    #endregion

    #region Get components

    /// <summary>
    ///     Gets component of type <see cref="componentType" /> attached to this game object.
    /// </summary>
    /// <param name="componentType"></param>
    /// <returns></returns>
    IGameComponent? GetComponent(Type componentType);

    /// <summary>
    ///     Gets component of type <see cref="componentType" /> attached to this game object. Otherwise throw exception.
    /// </summary>
    /// <param name="componentType"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns></returns>
    IGameComponent GetRequiredComponent(Type componentType);

    /// <summary>
    ///     Gets component of type <see cref="TComponent" /> attached to this game object.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    TComponent? GetComponent<TComponent>() where TComponent : IGameComponent;

    /// <summary>
    ///     Gets component of type <see cref="TComponent" /> attached to this game object. Otherwise throw exception.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <exception cref="ArgumentException"></exception>
    /// <returns></returns>
    TComponent GetRequiredComponent<TComponent>() where TComponent : IGameComponent;

    /// <summary>
    ///     Checks if this game object has component of type <see cref="componentType" /> attached.
    /// </summary>
    /// <param name="componentType"></param>
    /// <returns></returns>
    bool HasComponent(Type componentType);

    /// <summary>
    ///     Checks if this game object has component of type <see cref="TComponent" /> attached.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    bool HasComponent<TComponent>();

    #endregion

    #region Component removal

    /// <summary>
    ///     Removes component of type <paramref name="type" /> from this game object.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    bool RemoveComponent(Type type);

    /// <summary>
    ///     Removes component of type <typeparamref name="TComponent" /> from this game object.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    bool RemoveComponent<TComponent>() where TComponent : IGameComponent;

    #endregion
}