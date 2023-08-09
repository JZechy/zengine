using ZEngine.Systems.Inputs.Devices.Keyboards;

namespace ZEngine.Systems.Inputs.Events;

/// <summary>
/// Context of the keyboard event.
/// </summary>
public struct KeyboardContext
{
    /// <summary>
    /// For which key the event was triggered.
    /// </summary>
    public Key Key { get; set; }
    
    /// <summary>
    /// What is current state of the key.
    /// </summary>
    public KeyState State { get; set; }
}