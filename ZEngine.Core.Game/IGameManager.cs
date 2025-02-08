namespace ZEngine.Core.Game;

/// <summary>
///     Describes the interface of main engine manager responsible for game loop and systems management.
/// </summary>
public interface IGameManager
{
    /// <summary>
    ///     Access to the game's service provider.
    /// </summary>
    IServiceProvider ServiceProvider { get; }

    /// <summary>
    ///     The frequency of game updates in Hz.
    /// </summary>
    int UpdateFrequency { get; set; }

    /// <summary>
    ///     Gets a task that represents a background thread of game loop.
    /// </summary>
    Task GameTask { get; }

    /// <summary>
    ///     Adds a new instance of <see cref="IGameSystem" /> to the game.
    /// </summary>
    /// <param name="gameSystem"></param>
    void AddSystem(IGameSystem gameSystem);

    /// <summary>
    ///     Starts the thread of a game loop.
    /// </summary>
    void Start();

    /// <summary>
    ///     Signals the game loop to stop.
    /// </summary>
    /// <remarks>
    ///     This method completes synchronously the background thread.
    /// </remarks>
    void Stop();

    /// <summary>
    ///     Signals the game loop to stop.
    /// </summary>
    /// <returns>Task that can be used to await the game loop task.</returns>
    Task StopAsync();
}