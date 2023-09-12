using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using ZEngine.Architecture.Components;
using ZEngine.Architecture.GameObjects;
using ZEngine.Core;
using ZEngine.Systems.GameObjects;
using ZEngine.Testing.Extensions;
using ZEngine.Tests.Testing.SetUp;

namespace ZEngine.Tests.Testing;

/// <summary>
/// Tests the basic abilities of the test framework.
/// </summary>
public class BasicsTest : FrameworkTest
{
    public BasicsTest(FrameworkTestFactory factory, ITestOutputHelper testOutputHelper) : base(factory, testOutputHelper)
    {
    }

    /// <summary>
    /// Tests the ability to interract with the engine environment inside the test.
    /// </summary>
    [Fact]
    public void Test_BasicTest()
    {
        IGameObject go = GameObjectManager.Create();
        TestingComponent component = go.AddComponent<TestingComponent>();
        component.Should().NotBeNull();
    }

    /// <summary>
    /// Tests awaiting when until thje component's member doesn't reach a specific value.
    /// </summary>
    [Fact]
    public async Task Test_AwaitTheValue()
    {
        IGameObject go = GameObjectManager.Create();
        TestingComponent component = go.AddComponent<TestingComponent>();

        await component.TestPredicate(x => x.ElapsedTime > 0.5);
    }

    /// <summary>
    /// The predicate should be cancelled after the default timeout (1000 ms).
    /// </summary>
    [Fact]
    public async Task Test_PredicateTimeout()
    {
        IGameObject go = GameObjectManager.Create();
        TestingComponent component = go.AddComponent<TestingComponent>();

        await component.Awaiting(x => x.TestPredicate(y => y.ElapsedTime > 5))
            .Should()
            .ThrowAsync<TaskCanceledException>();
    }

    /// <summary>
    /// Component used for testing.
    /// </summary>
    public class TestingComponent : GameComponent
    {
        /// <summary>
        /// Elapsed time in seconds.
        /// </summary>
        public double ElapsedTime { get; private set; }
        
        private void Update()
        {
            ElapsedTime += GameTime.DeltaTime;
        }
    }
}