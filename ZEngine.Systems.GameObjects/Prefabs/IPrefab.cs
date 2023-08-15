using ZEngine.Architecture.Components;
using ZEngine.Architecture.Components.Model;
using ZEngine.Architecture.GameObjects;

namespace ZEngine.Systems.GameObjects.Prefabs;

/// <summary>
/// Describes a class that serves for programatic creation of a <see cref="GameObject"/>
/// </summary>
public interface IPrefab : IGameComponentModel
{
    /// <summary>
    /// Name of created game object.
    /// </summary>
    string Name { get; set; }
    
    /// <summary>
    /// Gets collection of defined components.
    /// </summary>
    IReadOnlyCollection<IGameComponent> PrefabComponents { get; }

    /// <summary>
    /// Adds a new componentt with custom initialization callback.
    /// </summary>
    /// <param name="init"></param>
    /// <typeparam name="TComponent"></typeparam>
    void AddComponent<TComponent>(Action<TComponent> init) where TComponent : IGameComponent;
}