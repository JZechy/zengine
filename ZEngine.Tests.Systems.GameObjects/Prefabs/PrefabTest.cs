using System.Numerics;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using ZEngine.Architecture.Communication.Events;
using ZEngine.Architecture.Components;
using ZEngine.Architecture.GameObjects;
using ZEngine.Systems.GameObjects;
using ZEngine.Systems.GameObjects.Prefabs;

namespace ZEngine.Tests.Systems.GameObjects.Prefabs;

public class PrefabTest
{
    [Test]
    public void Test_BasicPrefab()
    {
        GameObjectSystem system = new(Mock.Of<IEventMediator>(), new NullLogger<GameObjectSystem>());
        system.Initialize();
        
        Prefab prefab = new()
        {
            Name = "Testing Prefab"
        };
        prefab.AddComponent<Transform>(x => x.Position = new Vector3(10, 10, 10));

        IGameObject gameObject = ObjectManager.FromPrefab(prefab);
        Transform transform = gameObject.GetRequiredComponent<Transform>();

        Transform prefabTransform = prefab.GetRequiredComponent<Transform>();
        transform.Position.Should().Be(prefabTransform.Position);
    }
}