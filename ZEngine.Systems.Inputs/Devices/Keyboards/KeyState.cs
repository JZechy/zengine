namespace ZEngine.Systems.Inputs.Devices.Keyboards;

/// <summary>
///     Marks the current state of a key.
/// </summary>
public enum KeyState
{
    /// <summary>
    ///     The key is released.
    /// </summary>
    Released,

    /// <summary>
    ///     They key is down.
    /// </summary>
    Down,

    /// <summary>
    ///     The key is being hold.
    /// </summary>
    Pressed
}