using FluentAssertions;
using NUnit.Framework;
using ZEngine.Architecture.Communication.Messages;
using ZEngine.Tests.Architecture.Communication.Messages.Cases;

namespace ZEngine.Tests.Architecture.Communication.Messages;

public class MessageHandlerTest
{
    /// <summary>
    ///     Tests receiving messages by system method.
    /// </summary>
    [Test]
    public void Test_SystemMethod()
    {
        TestReceiver receiver = new();
        receiver.SendMessage(nameof(TestReceiver.Awake));
        receiver.TestValue.Should().Be(2);
    }

    /// <summary>
    ///     Tests receiving messages by method with <see cref="MessageTargetAttribute" />.
    /// </summary>
    [Test]
    public void Test_AttributeMethod()
    {
        TestReceiver receiver = new();
        receiver.SendMessage(nameof(TestReceiver.TestMethod));
        receiver.TestValue.Should().Be(1);
    }

    /// <summary>
    ///     Tests receiving messages by system target.
    /// </summary>
    /// <remarks>
    ///     This also tests private method access.
    /// </remarks>
    [Test]
    public void Test_SystemTarget()
    {
        TestReceiver receiver = new();
        receiver.SendMessage(SystemMethod.OnEnable);
        receiver.TestValue.Should().Be(3);
    }

    /// <summary>
    ///     Tests extension access.
    /// </summary>
    [Test]
    public void Test_Extending()
    {
        ExtendingReceiver receiver = new();
        receiver.SendMessage(nameof(ExtendingReceiver.TestMethod));
        receiver.TestValue.Should().Be(1);

        receiver.SendMessage(nameof(ExtendingReceiver.TestMethod2));
        receiver.TestValue.Should().Be(4);
    }

    /// <summary>
    ///     Test using message handler with class, that is no implementing the interface.
    /// </summary>
    [Test]
    public void Test_WithoutInterface()
    {
        ReceiverWithoutInterface receiver = new();

        this.Invoking(x => { _ = new MessageHandler(receiver); })
            .Should()
            .Throw<ArgumentException>();
    }

    /// <summary>
    ///     Invoking the message handler with a non-existing method should not throw an exception.
    /// </summary>
    [Test]
    public void Test_NonExistingMethod()
    {
        TestReceiver receiver = new();
        receiver.SendMessage(SystemMethod.OnDestroy);
    }
}