using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ZEngine.Core.Game;
using ZEngine.Systems.GameObjects.Extensions;
using ZEngine.Systems.ThreadSynchronization.Extensions;
using ZEngine.Testing.System.Extensions;

namespace ZEngine.Testing;

/// <summary>
/// Default implementation of <see cref="ITestFactory" />.
/// </summary>
/// <remarks>
/// This factory creates basic environment with game object system available and default implementation of null logging.
/// </remarks>
public abstract class ZEngineTestFactory : ITestFactory
{
    /// <inheritdoc />
    public IGameManager Build()
    {
        GameBuilder builder = GameBuilder.Create();
        
        // Basic logging for the game.
        builder.Services.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));
        builder.Services.AddSingleton(typeof(ILoggerFactory), typeof(NullLoggerFactory));
        
        // Registers the basic required systems.
        builder.AddGameObjectSystem();
        builder.AddThreadSynchronizationSystem();
        builder.AddTestingSystem();
        
        // Allows for additional configuration.
        OnServicesConfigure(builder.Services);
        OnEngineConfigure(builder);
        
        return builder.Build();
    }

    /// <summary>
    /// Place where you can configure services for the test.
    /// </summary>
    /// <param name="services"></param>
    protected virtual void OnServicesConfigure(IServiceCollection services)
    {
    }

    /// <summary>
    /// Place where you can do additonal configuration of the engine.
    /// </summary>
    /// <param name="builder"></param>
    protected virtual void OnEngineConfigure(GameBuilder builder)
    {
    }
}