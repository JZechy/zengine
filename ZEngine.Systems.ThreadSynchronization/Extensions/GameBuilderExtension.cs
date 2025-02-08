using ZEngine.Core.Game;

namespace ZEngine.Systems.ThreadSynchronization.Extensions;

public static class GameBuilderExtension
{
    /// <summary>
    ///     Registers the thread synchronization system.
    /// </summary>
    /// <param name="builder"></param>
    public static void AddThreadSynchronizationSystem(this GameBuilder builder)
    {
        builder.AddSystem<ThreadSynchronizationSystem>();
    }
}