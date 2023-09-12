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
    /// Time between previous and current frame.
    /// </summary>
    private static TimeSpan _frameSpan = TimeSpan.Zero;

    /// <summary>
    /// Gets the delta time between the last update and the current update in miliseconds.
    /// </summary>
    public static double DeltaTimeMs => _frameSpan.TotalMilliseconds;

    /// <summary>
    /// Gets the delta time between the last update and the current update in seconds.
    /// </summary>
    public static double DeltaTime => _frameSpan.TotalSeconds;

    /// <summary>
    /// Calculates the delta time.
    /// </summary>
    public static void CalculateDeltaTime()
    {
        DateTime now = DateTime.Now;

        _frameSpan = _lastUpdateTime is null
            ? TimeSpan.Zero
            : now.Subtract(_lastUpdateTime.Value);

        _lastUpdateTime = now;
    }
}