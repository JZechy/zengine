using ZEngine.Architecture.Communication.Messages;

namespace ZEngine.Tests.Architecture.Communication.Messages.Cases;

/// <summary>
/// Clas for testing Message Handler functionality.
/// </summary>
internal class TestReceiver : IMessageReceiver
{
    /// <summary>
    /// Handler for receiving messages.
    /// </summary>
    private readonly MessageHandler _messageHandler;
        
    public TestReceiver()
    {
        _messageHandler = new MessageHandler(this);
    }
        
    /// <summary>
    /// Property that we can watch for changing the value.
    /// </summary>
    public int TestValue { get; protected set; }

    /// <summary>
    /// For test of system method.
    /// </summary>
    public void Awake()
    {
        TestValue = 2;
    }

    private void OnEnable()
    {
        TestValue = 3;
    }

    /// <summary>
    /// Test of method with attribute.
    /// </summary>
    [MessageTarget]
    public void TestMethod()
    {
        TestValue = 1;
    }

    /// <summary>
    /// Interface implementation.
    /// </summary>
    /// <param name="target"></param>
    public void SendMessage(string target)
    {
        _messageHandler.Handle(target);
    }

    public void SendMessage(SystemMethod systemTarget)
    {
        _messageHandler.Handle(systemTarget);
    }
}