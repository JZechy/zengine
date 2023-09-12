using Xunit.Abstractions;
using ZEngine.Architecture.Components;
using ZEngine.Architecture.GameObjects;
using ZEngine.Systems.GameObjects;

namespace ZEngine.Testing;

/// <summary>
/// Abstract class used for testing game components, providing prepared instance of the component.
/// </summary>
/// <typeparam name="TFixtureFactory"></typeparam>
/// <typeparam name="TGameComponent"></typeparam>
public abstract class GameComponentFixture<TFixtureFactory, TGameComponent> : ZEngineFixture<TFixtureFactory>
    where TFixtureFactory : class, ITestFactory
    where TGameComponent : IGameComponent
{
    protected GameComponentFixture(TFixtureFactory factory, ITestOutputHelper testOutputHelper) : base(factory, testOutputHelper)
    {
    }

    /// <summary>
    /// Initialized instance of the component.
    /// </summary>
    public TGameComponent Component { get; set; } = default!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        IGameObject go = GameObjectManager.Create();
        Component = go.AddComponent<TGameComponent>();
    }
}