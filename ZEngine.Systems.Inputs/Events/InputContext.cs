using ZEngine.Systems.Inputs.Events.Paths;

namespace ZEngine.Systems.Inputs.Events;

/// <summary>
/// Basic context describing the input event.
/// </summary>
/// <typeparam name="TContext"></typeparam>
public struct InputContext<TContext>
{
    /// <summary>
    /// Base input path that was triggered.
    /// </summary>
    public InputPath InputPath { get; set; }
    
    /// <summary>
    /// Context class that hold specific data for the callback.
    /// </summary>
    public TContext Context { get; set; }
}