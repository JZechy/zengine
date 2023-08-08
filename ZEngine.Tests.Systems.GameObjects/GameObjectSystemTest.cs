using FluentAssertions;
using NUnit.Framework;
using ZEngine.Architecture.GameObjects;
using ZEngine.Systems.GameObjects;
using ZEngine.Tests.Systems.GameObjects.TestingComponents;

namespace ZEngine.Tests.Systems.GameObjects;

public class GameObjectSystemTest
{
    /// <summary>
    /// Tests initialization of the game object system.
    /// </summary>
    [Test]
    public void Test_Initialization()
    {
        this.Invoking(x => { _ = ObjectManager.Instance; }).Should().Throw<InvalidOperationException>();

        GameObjectSystem system = new();
        system.Initialize();

        ObjectManager.Instance.Should().NotBeNull();
    }

    /// <summary>
    /// Tests the lifetime of the game object system.
    /// </summary>
    [Test]
    public void Test_SystemLifetime()
    {
        GameObjectSystem system = new();
        system.Initialize();

        IGameObject gameObject = ObjectManager.Create();
        SystemTestComponent component = gameObject.AddComponent<SystemTestComponent>();
        component.AwakeCalled.Should().BeTrue();

        system.Update();
        component.UpdateCalled.Should().BeTrue();
        component.OnEnableCalled.Should().BeTrue();
        ObjectManager.Destroy(gameObject);
        component.OnDestroyCalled.Should().BeFalse(); // Game Object is destroyed in next frame.

        system.Update();
        component.OnDestroyCalled.Should().BeTrue();
    }
}