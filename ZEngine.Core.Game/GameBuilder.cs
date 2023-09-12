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
        RegisterBasics();
    }

    private GameBuilder(IServiceCollection services)
    {
        Services = services;
        RegisterBasics();
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
    /// Creates a new instance of the GameBuilder based on the provided service collection.
    /// </summary>
    /// <param name="services">Instance of existing collection services.</param>
    /// <returns></returns>
    public static GameBuilder Create(IServiceCollection services)
    {
        return new GameBuilder(services);
    }

    /// <summary>
    /// Register basic engine services.
    /// </summary>
    private void RegisterBasics()
    {
        Services.AddSingleton<IGameManager, GameManager>();
        Services.AddSingleton<IEventMediator, EventMediator>();
    }

    /// <summary>
    /// Builds the basic dependencies and creates GameManager.
    /// </summary>
    /// <returns></returns>
    public IGameManager Build()
    {
        IServiceProvider provider = Services.BuildServiceProvider();

        return provider.GetRequiredService<IGameManager>();
    }

    /// <summary>
    /// Registers a game system.
    /// </summary>
    /// <typeparam name="TGameSystem"></typeparam>
    public void AddSystem<TGameSystem>() where TGameSystem : class, IGameSystem
    {
        Services.AddSingleton<IGameSystem, TGameSystem>();
    }

    /// <summary>
    /// Register a game system with custom implementation callback.
    /// </summary>
    /// <param name="implementationCallback"></param>
    public void AddSystem(Func<IServiceProvider, IGameSystem> implementationCallback)
    {
        Services.AddSingleton(implementationCallback);
    }
}