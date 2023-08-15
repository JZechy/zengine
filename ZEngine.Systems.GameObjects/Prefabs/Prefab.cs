using ZEngine.Architecture.Components;
using ZEngine.Architecture.Components.Model;
using ZEngine.Architecture.GameObjects;

namespace ZEngine.Systems.GameObjects.Prefabs;

/// <summary>
/// Prefab is a class that serves for programatic creation of a <see cref="GameObject"/>
/// </summary>
public class Prefab : GameComponentModel, IPrefab
{
    /// <inheritdoc />
    public string Name { get; set; } = "Prefab Clone";

    /// <inheritdoc />
    public IReadOnlyCollection<IGameComponent> PrefabComponents => Components.ToHashSet();

    public void AddComponent<TComponent>(Action<TComponent> init) where TComponent : IGameComponent
    {
        TComponent component = AddComponent<TComponent>();
        init.Invoke(component);
    }
}