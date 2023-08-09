using ZEngine.Systems.Inputs.Devices.Keyboards;

namespace ZEngine.Systems.Inputs.Events;

/// <summary>
/// Context of the keyboard event.
/// </summary>
public struct KeyboardContext
{
    /// <summary>
    /// What is current state of the key.
    /// </summary>
    public KeyState State { get; set; }
}