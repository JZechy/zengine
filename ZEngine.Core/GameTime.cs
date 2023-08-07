namespace ZEngine.Core;

/// <summary>
/// Static class for managing time in the game.
/// </summary>
public static class GameTime
{
    /// <summary>
    /// The last time when the game was updated.
    /// </summary>
    private static DateTime? _lastUpdateTime;
    
    /// <summary>
    /// Gets the delta time between the last update and the current update.
    /// </summary>
    public static double DeltaTime { get; private set; }

    /// <summary>
    /// Calculates the delta time.
    /// </summary>
    public static void CalculateDeltaTime()
    {
        DeltaTime = _lastUpdateTime is null 
            ? 0d 
            : DateTime.Now.Subtract(_lastUpdateTime.Value).TotalMilliseconds;

        _lastUpdateTime = DateTime.Now;
    }
}