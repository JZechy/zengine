using ZEngine.Architecture.Communication.Messages;

namespace ZEngine.Tests.Architecture.Communication.Messages.Cases;

/// <summary>
///     Implementation of <see cref="TestReceiver" /> for testing extending functionality.
/// </summary>
internal class ExtendingReceiver : TestReceiver
{
    [MessageTarget]
    public void TestMethod2()
    {
        TestValue = 4;
    }
}