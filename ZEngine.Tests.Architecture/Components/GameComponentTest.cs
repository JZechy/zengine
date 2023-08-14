using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using ZEngine.Architecture.Components;
using ZEngine.Tests.Architecture.Components.Cases;

namespace ZEngine.Tests.Architecture.Components;

/// <summary>
/// Tests abstract implementation of <see cref="IGameComponent"/>.
/// </summary>
public class GameComponentTest
{
    /// <summary>
    /// Tests calling of OnEnable and OnDisable methods when the component is activated or deactivated.
    /// </summary>
    [Test]
    public void Test_ComponentLifetime()
    {
        LifetimeComponent component = new();
        component.OnEnableCalled.Should().BeFalse();
        component.OnDisableCalled.Should().BeFalse();

        component.Enabled = true;
        component.OnEnableCalled.Should().BeTrue();
        component.OnDisableCalled.Should().BeFalse();

        component.Enabled = false;
        component.OnDisableCalled.Should().BeTrue();
    }

    [Test]
    public void Test_Clone()
    {
        Transform transform = new()
        {
            Position = new Vector3(10, 10, 10)
        };

        Transform clone = (Transform) transform.Clone();
        clone.Position.Should().Be(transform.Position);

        clone.Position = new Vector3(15, 15, 15);
        clone.Position.Should().NotBe(transform.Position);
    }

    [Test]
    public void Test_TransformWithChildren()
    {
        Transform parent = new()
        {
            Position = new Vector3(10, 10, 10)
        };

        Transform child1 = new();
        child1.SetParent(parent);
        Transform child2 = new()
        {
            Position = new Vector3(25, 50, 25)
        };
        child2.SetParent(parent);

        Transform clone = (Transform) parent.Clone();
        clone.Should().HaveCount(2);
        clone.Last().Position.Should().Be(child2.Position);
    }
}