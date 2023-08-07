﻿namespace ZEngine.Core.Game;

/// <summary>
/// Main manager responsible for game loop and systems management.
/// </summary>
public class GameManager
{
    /// <summary>
    /// List of all registered systems for the game.
    /// </summary>
    private List<IGameSystem> _systems = new();
    
    /// <summary>
    /// Indicates if the game loop is running.
    /// </summary>
    private bool _isRunning;

    /// <summary>
    /// Adds new instance of <see cref="IGameSystem"/> to the game.
    /// </summary>
    /// <param name="gameSystem"></param>
    public void AddSystem(IGameSystem gameSystem)
    {
        _systems.Add(gameSystem);
    }

    /// <summary>
    /// Starts the game loop.
    /// </summary>
    public void Start()
    {
        Initialize();

        while (_isRunning)
        {
            UpdateSystems();
            CheckGameExit();
        }
    }

    /// <summary>
    /// Initialize the game.
    /// </summary>
    private void Initialize()
    {
        _isRunning = true;
        _systems = _systems.OrderBy(x => x.Priority).ToList();

        foreach (IGameSystem gameSystem in _systems)
        {
            gameSystem.Initialize();
        }
    }

    /// <summary>
    /// Updates all systems.
    /// </summary>
    private void UpdateSystems()
    {
        foreach (IGameSystem gameSystem in _systems)
        {
            gameSystem.Update();
        }
    }

    /// <summary>
    /// Checks for conditions to exit the game.
    /// </summary>
    private void CheckGameExit()
    {
        
    }
}