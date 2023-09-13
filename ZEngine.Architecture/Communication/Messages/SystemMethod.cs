namespace ZEngine.Architecture.Communication.Messages;

/// <summary>
/// Enumeration of all system methods that can receive messages.
/// </summary>
public enum SystemMethod
{
    /// <summary>
    /// Marks a ZEngine lifetime method that is called when the object is created.
    /// </summary>
    Awake,
    
    /// <summary>
    /// Marks a ZEngine lifetime method that is called when the object is enabled.
    /// </summary>
    OnEnable,
    
    /// <summary>
    /// Marks a ZEngine lifetime method that is called when the object is disabled.
    /// </summary>
    OnDisable,
    
    /// <summary>
    /// Marks a ZEngine lifetime method that is called when the object is update.
    /// </summary>
    Update,
    
    /// <summary>
    /// Marks a ZEngine lifetime method that is called when the object is destroyed.
    /// </summary>
    OnDestroy
}