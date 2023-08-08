using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using ZEngine.Architecture.Communication.Events;

namespace ZEngine.Tests.Architecture.Communication.Mediator;

public class EventMediatorTest
{
    /// <summary>
    /// Test basic event mediator flow.
    /// </summary>
    [Test]
    public void Test_Subscribing()
    {
        bool wasCalled = false;
        EventMediator mediator = new(new NullLoggerFactory());
        mediator.Subscribe<TestMessage>(Receiver);
        mediator.Notify(new TestMessage());
        wasCalled.Should().BeTrue();
        
        wasCalled = false;
        mediator.Unsubscribe<TestMessage>(Receiver);
        mediator.Notify(new TestMessage());
        wasCalled.Should().BeFalse();
        return;

        void Receiver(TestMessage message)
        {
            wasCalled = true;
        }
    }
    
    private class TestMessage : IEventMessage
    {
        
    }
}