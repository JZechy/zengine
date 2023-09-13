using ZEngine.Architecture.Components;
using ZEngine.Architecture.Components.Model;

namespace ZEngine.Systems.GameObjects.Prefabs;

/// <summary>
/// Prefab is a class that serves for programatic creation of a <see cref="GameObject"/>
/// </summary>
public class Prefab : GameComponentModel, IPrefab
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceProvider"></param>
    public Prefab(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

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