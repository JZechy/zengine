using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using ZEngine.Core;
using ZEngine.Core.Game;
using ZEngine.Tests.Core.Game.TestSystems;

namespace ZEngine.Tests.Core.Game;

/// <summary>
/// Tests basic functionality of the game manager.
/// </summary>
public class GameManagerTest
{
    /// <summary>
    /// Test basic game loop behaviour.
    /// </summary>
    [Test]
    public async Task Test_BasicGameLoop()
    {
        GameManager gameManager = new(Mock.Of<IServiceProvider>(), Enumerable.Empty<IGameSystem>(), new NullLogger<GameManager>());
        BasicSystem basicSystem = new(); // Basic system will interrupt the game loop after some iterations.
        gameManager.AddSystem(basicSystem);
        
        gameManager.Start();
        
        await gameManager.Task;
        
        GameTime.DeltaTime.Should().BeGreaterThan(0);
        
        basicSystem.Initialized.Should().BeTrue();
        basicSystem.Updated.Should().BeTrue();
        basicSystem.CleanedUp.Should().BeTrue();
    }
}