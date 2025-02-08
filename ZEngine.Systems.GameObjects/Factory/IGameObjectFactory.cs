using ZEngine.Architecture.GameObjects;

namespace ZEngine.Systems.GameObjects.Factory;

/// <summary>
///     Defines an interface for a factory that configures <see cref="IGameObject" /> instances.
/// </summary>
public interface IGameObjectFactory
{
    /// <summary>
    ///     Configures the prefab.
    /// </summary>
    /// <param name="gameObject"></param>
    void Configure(IGameObject gameObject);
}