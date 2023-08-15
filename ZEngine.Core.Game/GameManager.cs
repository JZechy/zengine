﻿using Microsoft.Extensions.Logging;

namespace ZEngine.Core.Game;

/// <summary>
/// Main manager responsible for game loop and systems management.
/// </summary>
public class GameManager
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

    /// <summary>
    /// Access to the game's service provider.
    /// </summary>
    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// The frequency of game updates in Hz.
    /// </summary>
    public int UpdateFrequency { get; set; } = 60;

    /// <summary>
    /// Gets a task that completes when the game is finished.
    /// </summary>
    public Task Task => _gameLoopCompletion.Task;

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
            int sleep = targetFrameTime - (int) GameTime.DeltaTime;

            return Math.Max(sleep, 0);
        }
    }

    /// <summary>
    /// Adds additional instance of <see cref="IGameSystem"/> to the game.
    /// </summary>
    /// <param name="gameSystem"></param>
    public void AddSystem(IGameSystem gameSystem)
    {
        _systems.Add(gameSystem);
    }

    /// <summary>
    /// Starts the game loop.
    /// </summary>
    public async void Start()
    {
        Initialize();
        await Task.Run(GameLoop);
        CleanUp();

        _gameLoopCompletion.SetResult();
    }

    /// <summary>
    /// Method processing the actual game loop.
    /// </summary>
    private async void GameLoop()
    {
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            GameTime.CalculateDeltaTime();

            UpdateSystems();
            CheckGameExit();

            await Task.Delay(SleepTime);
        }
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