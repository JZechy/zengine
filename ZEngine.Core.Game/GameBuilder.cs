using Microsoft.Extensions.DependencyInjection;
using ZEngine.Architecture.Communication.Events;

namespace ZEngine.Core.Game;

/// <summary>
/// Core class used to build the game.
/// </summary>
public class GameBuilder
{
    private GameBuilder()
    {
        Services.AddSingleton<GameManager>();
        Services.AddSingleton<IEventMediator, EventMediator>();
    }
    
    /// <summary>
    /// Access to the service collection.
    /// </summary>
    public IServiceCollection Services { get; } = new ServiceCollection();
    
    /// <summary>
    /// Creates a new instance of the <see cref="GameBuilder"/> class.
    /// </summary>
    /// <returns></returns>
    public static GameBuilder Create()
    {
        return new GameBuilder();
    }

    /// <summary>
    /// Builds the basic dependencies and creates GameManager.
    /// </summary>
    /// <returns></returns>
    public GameManager Build()
    {
        IServiceProvider provider = Services.BuildServiceProvider();

        return provider.GetRequiredService<GameManager>();
    }

    /// <summary>
    /// Registers a game system.
    /// </summary>
    /// <typeparam name="TGameSystem"></typeparam>
    public void AddSystem<TGameSystem>() where TGameSystem : class, IGameSystem
    {
        Services.AddSingleton<IGameSystem, TGameSystem>();
    }
}