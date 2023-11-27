using Xunit.Abstractions;
using ZEngine.Architecture.Communication.Messages;
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="testOutputHelper"></param>
    protected GameComponentFixture(TFixtureFactory factory, ITestOutputHelper testOutputHelper) : base(factory, testOutputHelper)
    {
    }

    /// <summary>
    /// Instance of the game object to which the component is attached.
    /// </summary>
    protected IGameObject GameObject { get; private set; } = default!;

    /// <summary>
    /// Initialized instance of the component.
    /// </summary>
    protected TGameComponent Component { get; private set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        GameObject = GameObjectManager.Create(false); // Create the component in-active so it can be set up before it's awakened.
        Component = GameObject.AddComponent<TGameComponent>();
        SetUpComponent(Component);
        
        GameObject.SendMessage(SystemMethod.Awake); // Awake is not called when the component is activated later. So we need to call it.
        GameObject.Active = true; // Activate the game object after set-up.
    }

    /// <summary>
    /// Initial component set-up before it's awaken.
    /// </summary>
    /// <param name="component"></param>
    protected virtual void SetUpComponent(TGameComponent component)
    {
    }
}