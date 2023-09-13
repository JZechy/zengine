namespace ZEngine.Systems.GameObjects.Prefabs.Factory;

/// <summary>
/// Defines an interface for a factory that configures <see cref="IPrefab"/> instances.
/// </summary>
public interface IPrefabFactory
{
    /// <summary>
    /// Configures the prefab.
    /// </summary>
    /// <param name="prefab"></param>
    void Configure(IPrefab prefab);
}