using System.Collections;
using ZEngine.Systems.Inputs.Events.Paths;

namespace ZEngine.Systems.Inputs.Events.Collections;

/// <summary>
/// Collection of registered callbacks for a specific input path.
/// </summary>
public interface IInputPathCollection : ICollection
{
    /// <summary>
    /// The input path that this collection is for.
    /// </summary>
    InputPath InputPath { get; }
    
    /// <summary>
    /// Adds a new callback to the collection.
    /// </summary>
    /// <param name="callback"></param>
    void Add(MulticastDelegate callback);

    /// <summary>
    /// Removes a callback from the collection.
    /// </summary>
    /// <param name="callback"></param>
    void Remove(MulticastDelegate callback);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    void Invoke(object context);
}