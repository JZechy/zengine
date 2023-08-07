namespace ZEngine.Core;

/// <summary>
/// Represents a custom system in the engine.
/// </summary>
public interface IGameSystem
{
    /// <summary>
    /// Priority of the system. Lower number means higher priority.
    /// </summary>
    int Priority { get; }
    
    /// <summary>
    /// Initialize the system.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Updates the system.
    /// </summary>
    void Update();
}