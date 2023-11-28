using FluentAssertions;
using Moq;
using NUnit.Framework;
using ZEngine.Architecture.Communication.Messages;
using ZEngine.Architecture.Components;
using ZEngine.Architecture.GameObjects;

namespace ZEngine.Tests.Architecture.Components;

public class GameComponentModelTest
{
    [Test]
    public void Test_ComponentsOrder()
    {
        GameObject gameObject = new(Mock.Of<IServiceProvider>());
        gameObject.AddComponent<FirstComponent>();
        gameObject.AddComponent<SecondComponent>();

        gameObject.Invoking(x => x.SendMessage(SystemMethod.Awake)).Should().NotThrow();
    }

    [Test]
    public void Test_ComponentsInWrongOrder()
    {
        GameObject gameObject = new(Mock.Of<IServiceProvider>());
        gameObject.AddComponent<SecondComponent>();
        gameObject.AddComponent<FirstComponent>();

        gameObject.Invoking(x => x.SendMessage(SystemMethod.Awake)).Should().Throw<Exception>();
    }

    public class FirstComponent : GameComponent
    {
        public bool Awakened { get; private set; }

        public void Awake()
        {
            Awakened = true;
        }
    }

    public class SecondComponent : GameComponent
    {
        public void Awake()
        {
            GameObject.GetRequiredComponent<FirstComponent>().Awakened.Should().BeTrue();
        }
    }
}