using ZEngine.Core.Game;

namespace ZEngine.Testing;

/// <summary>
///     Describes the factory that is used to create a new test set-up.
/// </summary>
public interface ITestFactory
{
    /// <summary>
    ///     Builds the game environment from factory.
    /// </summary>
    /// <returns></returns>
    IGameManager Build();
}