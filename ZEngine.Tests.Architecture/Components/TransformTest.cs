using System.Numerics;
using System.Threading.Tasks.Dataflow;
using FluentAssertions;
using NUnit.Framework;
using ZEngine.Architecture.Components;

namespace ZEngine.Tests.Architecture.Components;

/// <summary>
/// Tests of the Transform component.
/// </summary>
public class TransformTest
{
    /// <summary>
    /// Tests assigning parent to a transform.
    /// </summary>
    [Test]
    public void Test_SetParent()
    {
        Transform parent = new();
        Transform child = new();
        
        // Sets parent
        child.SetParent(parent);
        child.Parent.Should().NotBeNull();
        parent.Should().HaveCount(1);
        
        // Unsets parent
        child.SetParent(null);
        child.Parent.Should().BeNull();
        parent.Should().HaveCount(0);
    }

    /// <summary>
    /// Tests position of transform relative to its parent.
    /// </summary>
    [Test]
    public void Test_LocalPosition()
    {
        Transform parent = new()
        {
            Position = new Vector3(5, 5, 10)
        };
        Transform child = new()
        {
            Position = new Vector3(10, 10, 0)
        };

        child.LocalPosition.Should().Be(child.Position);
        
        child.SetParent(parent);
        child.LocalPosition.Should().Be(child.Position - parent.Position);
        
        child.SetParent(null);
        child.LocalPosition.Should().Be(child.Position);
    }
}