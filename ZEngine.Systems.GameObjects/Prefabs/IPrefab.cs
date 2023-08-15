using ZEngine.Architecture.Components;
using ZEngine.Architecture.GameObjects;

namespace ZEngine.Systems.GameObjects.Prefabs;

/// <summary>
/// Describes a class that serves for programatic creation of a <see cref="GameObject"/>
/// </summary>
public interface IPrefab
{
    /// <summary>
    /// Name of created game object.
    /// </summary>
    string Name { get; set; }
    
    /// <summary>
    /// Gets collection of defined components.
    /// </summary>
    IReadOnlyCollection<IGameComponent> Components { get; }
    
    /// <summary>
    /// Adds a new component to the <see cref="GameObject"/> described by this <see cref="IPrefab"/>
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    TComponent AddComponent<TComponent>() where TComponent : class, IGameComponent;
    
    /// <summary>
    /// Adds a new component to the <see cref="GameObject"/> described by this <see cref="IPrefab"/> with custom initialization.
    /// </summary>
    /// <param name="init"></param>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    void AddComponent<TComponent>(Action<TComponent> init) where TComponent : class, IGameComponent;

    /// <summary>
    /// Adds a new component to the <see cref="GameObject"/> described by this <see cref="IPrefab"/>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IGameComponent AddComponent(Type type);

    /// <summary>
    /// Tries to get a component from the <see cref="GameObject"/> described by this <see cref="IPrefab"/>
    /// </summary>
    /// <param name="component"></param>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    bool TryGetComponent<TComponent>(out TComponent? component) where TComponent : class, IGameComponent;

    /// <summary>
    /// Tries to get a component from the <see cref="GameObject"/> described by this <see cref="IPrefab"/>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="component"></param>
    /// <returns></returns>
    bool TryGetComponent(Type type, out IGameComponent? component);
    
    /// <summary>
    /// Tries to remove a component from the <see cref="GameObject"/> described by this <see cref="IPrefab"/>
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    bool TryRemoveComponent<TComponent>() where TComponent : class, IGameComponent;
    
    /// <summary>
    /// Tries to remove a component from the <see cref="GameObject"/> described by this <see cref="IPrefab"/>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    bool TryRemoveComponent(Type type);
}