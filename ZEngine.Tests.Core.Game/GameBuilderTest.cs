using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using ZEngine.Core;
using ZEngine.Core.Game;
using ZEngine.Tests.Core.Game.TestSystems;

namespace ZEngine.Tests.Core.Game;

/// <summary>
/// Tests behaviour of game builder.
/// </summary>
public class GameBuilderTest
{
    [Test]
    public async Task TestGameBuilder()
    {
        GameBuilder builder = GameBuilder.Create();
        builder.Services.AddSingleton<ILogger<GameManager>>(_ => new NullLogger<GameManager>());
        builder.Services.AddSingleton<IGameSystem, BasicSystem>();
        
        GameManager gameManager = builder.Build();
        BasicSystem basicSystem = gameManager.ServiceProvider.GetServices<IGameSystem>()
            .OfType<BasicSystem>()
            .First();
        
        gameManager.Start();
        await gameManager.Task;

        basicSystem.Initialized.Should().BeTrue();
        basicSystem.Updated.Should().BeTrue();
        basicSystem.CleanedUp.Should().BeTrue();
    }
}