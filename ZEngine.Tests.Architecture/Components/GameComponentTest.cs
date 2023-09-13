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
}