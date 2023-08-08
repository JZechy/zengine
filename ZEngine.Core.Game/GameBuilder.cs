using Microsoft.Extensions.DependencyInjection;

namespace ZEngine.Core.Game;

/// <summary>
/// Core class used to build the game.
/// </summary>
public class GameBuilder
{
    private GameBuilder()
    {
        Services.AddSingleton<GameManager>();
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
}