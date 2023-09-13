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
using ZEngine.Systems.GameObjects.Prefabs.Factory;

namespace ZEngine.Tests.Systems.GameObjects.Prefabs;

public class PrefabTest
{
    /// <summary>
    /// Tests creation of basic prefab.
    /// </summary>
    [Test]
    public void Test_BasicPrefab()
    {
        GameObjectSystem system = new(Mock.Of<IEventMediator>(), new NullLogger<GameObjectSystem>(), Mock.Of<IServiceProvider>());
        system.Initialize();

        Prefab prefab = new(Mock.Of<IServiceProvider>())
        {
            Name = "Testing Prefab"
        };
        prefab.AddComponent<Transform>(x => x.Position = new Vector3(10, 10, 10));

        IGameObject gameObject = GameObjectManager.FromPrefab(prefab);
        Transform transform = gameObject.GetRequiredComponent<Transform>();

        Transform prefabTransform = prefab.GetRequiredComponent<Transform>();
        transform.Position.Should().Be(prefabTransform.Position);
    }

    /// <summary>
    /// Tests creation of a prefab from a factory.
    /// </summary>
    [Test]
    public void Test_PrefabFactory()
    {
        GameObjectSystem system = new(Mock.Of<IEventMediator>(), new NullLogger<GameObjectSystem>(), Mock.Of<IServiceProvider>());
        system.Initialize();

        Vector3 position = new(10, 15, 20);
        IGameObject gameObject = GameObjectManager.FromFactory(new TestingPrefabFactory(position));
        Transform transform = gameObject.GetRequiredComponent<Transform>();
        
        transform.Position.Should().Be(position);
    }
    
    /// <summary>
    /// Implementation of <see cref="IPrefabFactory"/> to test it's capabilities.
    /// </summary>
    private class TestingPrefabFactory : IPrefabFactory
    {
        /// <summary>
        /// Predefines position for the factory.
        /// </summary>
        private readonly Vector3 _position;

        public TestingPrefabFactory(Vector3 position)
        {
            _position = position;
        }

        /// <inheritdoc />
        public void Configure(IPrefab prefab)
        {
            prefab.AddComponent<Transform>(x => x.Position = _position);
        }
    }
}