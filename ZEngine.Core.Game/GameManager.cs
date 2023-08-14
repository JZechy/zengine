using Microsoft.Extensions.Logging;

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
    /// List of all registered systems for the game.
    /// </summary>
    private List<IGameSystem> _systems;
    
    /// <summary>
    /// Indicates if the game loop is running.
    /// </summary>
    private bool _isRunning;

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
    /// Orders game to exit.
    /// </summary>
    public bool ShouldExit { get; set; }

    /// <summary>
    /// The sleep time before the next update.
    /// </summary>
    private int SleepTime
    {
        get
        {
            int targetFrameTime = 1000 / UpdateFrequency;
            int sleep = targetFrameTime - (int)GameTime.DeltaTime;

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

        await Task.Run(() =>
        {
            while (_isRunning)
            {
                GameTime.CalculateDeltaTime();

                UpdateSystems();
                CheckGameExit();

                Thread.Sleep(SleepTime);
            }
        });
        
        CleanUp();
    }

    /// <summary>
    /// Initialize the game.
    /// </summary>
    private void Initialize()
    {
        _logger.LogInformation("Initializing game...");
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
        try
        {
            foreach (IGameSystem gameSystem in _systems)
            {
                gameSystem.Update();
            }
        }
        catch (AbortGameException)
        {
            ShouldExit = true;
        }
    }

    /// <summary>
    /// Initiate cleaning after the game loop is finished.
    /// </summary>
    private void CleanUp()
    {
        foreach (IGameSystem gameSystem in _systems)
        {
            gameSystem.CleanUp();
        }
    }

    /// <summary>
    /// Checks for conditions to exit the game.
    /// </summary>
    private void CheckGameExit()
    {
        if (ShouldExit)
        {
            _isRunning = false;
        }
    }
}