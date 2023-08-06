namespace ZEngine.Architecture.Communication.Messages;

/// <summary>
/// Enumeration of all system methods that can receive messages.
/// </summary>
public enum SystemMethod
{
    Awake,
    OnEnable,
    OnDisable,
    Update,
    OnDestroy
}