using Microsoft.Extensions.DependencyInjection;
using ZEngine.Core.Game;
using ZEngine.Systems.GameObjects.Extensions;
using ZEngine.Systems.ThreadSynchronization.Extensions;

namespace ZEngine.Player.Server;

/// <summary>
/// Server player allows easy integration to the dependency container on custom server-side project.
/// </summary>
/// <remarks>
/// During server initilization, it is not required to call the Build method, because the container should
/// be built by the server itself and the engine should be registered as a part of it.
/// </remarks>
public static class ZEngineSetup
{
    /// <summary>
    /// Registers the engine as a part of existing dependency container.
    /// </summary>
    /// <param name="services"></param>
    public static GameBuilder AddZEngine(this IServiceCollection services)
    {
        GameBuilder builder = GameBuilder.Create(services);
        builder.AddGameObjectSystem();
        builder.AddThreadSynchronizationSystem();

        return builder;
    }
}