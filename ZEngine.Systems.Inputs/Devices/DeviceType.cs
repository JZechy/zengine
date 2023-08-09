namespace ZEngine.Systems.Inputs.Devices;

/// <summary>
/// Enum describing specific and supported device types.
/// </summary>
public enum DeviceType
{
    /// <summary>
    /// Keyboard, supporting mostly "button" events from keys.
    /// </summary>
    Keyboard,
    
    /// <summary>
    /// Pointing device supporting position and button events.
    /// </summary>
    Mouse
}