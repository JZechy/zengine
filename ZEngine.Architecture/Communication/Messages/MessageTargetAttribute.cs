namespace ZEngine.Architecture.Communication.Messages;

/// <summary>
///     Marks methods, that can receive messages.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class MessageTargetAttribute : Attribute
{
}