using Microsoft.Extensions.Logging;

namespace ZEngine.Core.Game;

/// <summary>
/// Main manager responsible for game loop and systems management.
/// </summary>
public class GameManager : IGameManager
{
    /// <summary>
    /// Main game logger.
    /// </summary>
    private readonly ILogger<GameManager> _logger;

    /// <summary>
    /// Cancellation token source used to stop the game loop.
    /// </summary>
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    /// <summary>
    /// Task completion source used to wait for the game loop to finish.
    /// </summary>
    private readonly TaskCompletionSource _gameLoopCompletion = new();

    /// <summary>
    /// List of all registered systems for the game.
    /// </summary>
    private List<IGameSystem> _systems;

    public GameManager(IServiceProvider serviceProvider, IEnumerable<IGameSystem> gameSystems, ILogger<GameManager> logger)
    {
        _logger = logger;
        ServiceProvider = serviceProvider;
        _systems = gameSystems.ToList();
    }

    /// <inheritdoc />
    public IServiceProvider ServiceProvider { get; }

    /// <inheritdoc />
    public int UpdateFrequency { get; set; } = 60;

    /// <inheritdoc />
    public Task GameTask => _gameLoopCompletion.Task;

    /// <summary>
    /// Orders game to exit.
    /// </summary>
    private bool ShouldExit { get; set; }

    /// <summary>
    /// The sleep time in ms before the next update.
    /// </summary>
    private int SleepTime
    {
        get
        {
            int targetFrameTime = 1000 / UpdateFrequency;
            int sleep = targetFrameTime - (int) GameTime.DeltaTimeMs;

            return Math.Max(sleep, 0);
        }
    }

    /// <inheritdoc />
    public void AddSystem(IGameSystem gameSystem)
    {
        _systems.Add(gameSystem);
    }

    /// <inheritdoc />
    public void Start()
    {
        Initialize();
        Task.Run(GameLoop);
    }

    /// <inheritdoc />
    public void Stop()
    {
        StopAsync()
            .GetAwaiter()
            .GetResult();
    }

    /// <inheritdoc />
    public Task StopAsync()
    {
        ShouldExit = true;
        return GameTask;
    }

    /// <summary>
    /// Method processing the actual game loop.
    /// </summary>
    private async void GameLoop()
    {
        try
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                GameTime.CalculateDeltaTime();

                UpdateSystems();
                CheckGameExit();

                await Task.Delay(SleepTime);
            }
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An unhandled exception occurred while running the game loop.");
            _gameLoopCompletion.SetException(e);
            return;
        }

        CleanUp();
        _gameLoopCompletion.SetResult();
    }

    /// <summary>
    /// Initialize the game.
    /// </summary>
    private void Initialize()
    {
        _logger.LogInformation("Initializing game...");
        _systems = _systems.OrderBy(x => x.Priority).ToList();
        List<IGameSystem> failedSystems = new();

        foreach (IGameSystem gameSystem in _systems)
        {
            try
            {
                gameSystem.Initialize();
            }
            catch (Exception e)
            {
                failedSystems.Add(gameSystem);
                _logger.LogCritical(e, "An unhandled exception occurred while initializing game system {SystemName}. Failing systems are removed.", gameSystem.GetType().Name);
            }
        }

        foreach (IGameSystem gameSystem in failedSystems)
        {
            _systems.Remove(gameSystem);
        }
    }

    /// <summary>
    /// Updates all systems.
    /// </summary>
    private void UpdateSystems()
    {
        foreach (IGameSystem gameSystem in _systems)
        {
            try
            {
                gameSystem.Update();
            }
            catch (AbortGameException)
            {
                ShouldExit = true;
                break;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "An unhandled exception occurred while updating game system {SystemName}.", gameSystem.GetType().Name);
            }
        }
    }

    /// <summary>
    /// Initiate cleaning after the game loop is finished.
    /// </summary>
    private void CleanUp()
    {
        foreach (IGameSystem gameSystem in _systems)
        {
            try
            {
                gameSystem.CleanUp();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "An unhandled exception occurred while cleaning up game system {SystemName}.", gameSystem.GetType().Name);
            }
        }
    }

    /// <summary>
    /// Checks for conditions to exit the game.
    /// </summary>
    private void CheckGameExit()
    {
        if (ShouldExit)
        {
            _cancellationTokenSource.Cancel();
        }
    }
}