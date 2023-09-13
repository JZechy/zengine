using System.Numerics;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using ZEngine.Architecture.Communication.Events;
using ZEngine.Architecture.Components;
using ZEngine.Architecture.GameObjects;
using ZEngine.Systems.GameObjects;
using ZEngine.Systems.GameObjects.Factory;

namespace ZEngine.Tests.Systems.GameObjects.Factory;

public class GameObjectFactoryTest
{
    /// <summary>
    /// Tests creation of a prefab from a factory.
    /// </summary>
    [Test]
    public void Test_PrefabFactory()
    {
        GameObjectSystem system = new(Mock.Of<IEventMediator>(), new NullLogger<GameObjectSystem>(), Mock.Of<IServiceProvider>());
        system.Initialize();

        Vector3 position = new(10, 15, 20);
        IGameObject gameObject = GameObjectManager.FromFactory(new TestingGameObjectFactory(position));
        MyComponent requiredComponent = gameObject.GetRequiredComponent<MyComponent>();

        requiredComponent.Position.Should().Be(position);
    }

    public class MyComponent : GameComponent
    {
        public Vector3 Position { get; set; }
    }

    /// <summary>
    /// Implementation of <see cref="IGameObjectFactory"/> to test it's capabilities.
    /// </summary>
    private class TestingGameObjectFactory : IGameObjectFactory
    {
        /// <summary>
        /// Predefines position for the factory.
        /// </summary>
        private readonly Vector3 _position;

        public TestingGameObjectFactory(Vector3 position)
        {
            _position = position;
        }

        /// <inheritdoc />
        public void Configure(IGameObject gameObject)
        {
            gameObject.AddComponent<MyComponent>(x => x.Position = _position);
        }
    }
}