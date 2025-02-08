using ZEngine.Core.Game;

namespace ZEngine.Testing.System.Extensions;

public static class GameBuilderExtension
{
    /// <summary>
    ///     Registers the testing system to the game builder.
    /// </summary>
    /// <param name="builder"></param>
    public static void AddTestingSystem(this GameBuilder builder)
    {
        builder.AddSystem<TestingSystem>();
    }
}