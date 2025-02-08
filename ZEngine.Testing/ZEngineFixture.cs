using Xunit;
using Xunit.Abstractions;
using ZEngine.Core.Game;

namespace ZEngine.Testing;

/// <summary>
///     Defines an abstract class that is used to construct the unit test class for the game engine components & objects.
/// </summary>
/// <remarks>
///     Class is used to initialize complete engine environment for testing.
/// </remarks>
/// <typeparam name="TFixtureFactory"></typeparam>
public abstract class ZEngineFixture<TFixtureFactory> : IClassFixture<TFixtureFactory>, IAsyncLifetime
    where TFixtureFactory : class, ITestFactory
{
    /// <summary>
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="testOutputHelper"></param>
    protected ZEngineFixture(TFixtureFactory factory, ITestOutputHelper testOutputHelper)
    {
        Factory = factory;
        TestOutputHelper = testOutputHelper;
        GameManager = factory.Build();
    }

    /// <summary>
    ///     Instance of factory used to create the test.
    /// </summary>
    public TFixtureFactory Factory { get; }

    /// <summary>
    ///     Output helper allowing more debugging options in tests.
    /// </summary>
    public ITestOutputHelper TestOutputHelper { get; }

    /// <summary>
    ///     Instance of build game engine manager.
    /// </summary>
    public IGameManager GameManager { get; }

    /// <summary>
    ///     Access the game environment service provider.
    /// </summary>
    public IServiceProvider ServiceProvider => GameManager.ServiceProvider;

    /// <inheritdoc />
    public virtual Task InitializeAsync()
    {
        GameManager.Start();
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public virtual Task DisposeAsync()
    {
        return GameManager.StopAsync();
    }
}