using Xunit.Abstractions;
using ZEngine.Architecture.Components;
using ZEngine.Testing;

namespace ZEngine.Tests.Testing.SetUp;

public abstract class GameComponentTest<TGameComponent> : GameComponentFixture<FrameworkTestFactory, TGameComponent>
    where TGameComponent : IGameComponent
{
    protected GameComponentTest(FrameworkTestFactory factory, ITestOutputHelper testOutputHelper) : base(factory, testOutputHelper)
    {
    }
}