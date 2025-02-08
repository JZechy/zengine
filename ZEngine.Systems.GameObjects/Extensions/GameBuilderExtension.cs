using ZEngine.Core.Game;

namespace ZEngine.Systems.GameObjects.Extensions;

public static class GameBuilderExtension
{
    /// <summary>
    ///     Registers the GameObjectSystem to the GameBuilder.
    /// </summary>
    /// <param name="gameBuilder"></param>
    public static void AddGameObjectSystem(this GameBuilder gameBuilder)
    {
        gameBuilder.AddSystem<GameObjectSystem>();
    }
}