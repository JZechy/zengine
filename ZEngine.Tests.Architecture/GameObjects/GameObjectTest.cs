using FluentAssertions;
using NUnit.Framework;
using ZEngine.Architecture.Components;
using ZEngine.Architecture.GameObjects;
using ZEngine.Tests.Architecture.GameObjects.Cases;

namespace ZEngine.Tests.Architecture.GameObjects;

/// <summary>
/// Tests game object implementation.
/// </summary>
public class GameObjectTest
{
    /// <summary>
    /// Tests basic component management.
    /// </summary>
    [Test]
    public void Test_GameObjectComponent()
    {
        GameObject gameObject = new();
        
        Transform transform = gameObject.AddComponent<Transform>();
        gameObject.Components.Should().Contain((IGameComponent) transform);
        transform.GameObject.Should().Be(gameObject);

        gameObject.HasComponent<Transform>().Should().BeTrue();
        gameObject.GetComponent<Transform>(); // Should not throw.
        gameObject.Invoking(x => x.AddComponent<Transform>()).Should().Throw<ArgumentException>();
        gameObject.RemoveComponent<Transform>().Should().BeTrue();
        gameObject.HasComponent<Transform>().Should().BeFalse();
    }

    [Test]
    public void Test_GameObjectComponent_Lifetime()
    {
        GameObject gameObject = new();

        // Initialize the component.
        GameObjectLifetime lifetime = gameObject.AddComponent<GameObjectLifetime>();
        lifetime.Awaken.Should().BeTrue();
        lifetime.EnableCalled.Should().BeTrue();
        lifetime.DisbleCalled.Should().BeFalse();
        lifetime.Destroyed.Should().BeFalse();
        
        // Disable game object
        gameObject.Active = false;
        lifetime.DisbleCalled.Should().BeTrue();
        lifetime.EnableCalled.Should().BeFalse();
        
        // Activate game object
        gameObject.Active = true;
        lifetime.EnableCalled.Should().BeTrue();
        
        // Destroy component
        gameObject.RemoveComponent<GameObjectLifetime>();
        lifetime.Destroyed.Should().BeTrue();
    }
}