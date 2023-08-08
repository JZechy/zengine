using ZEngine.Systems.Inputs.Devices.Events;

namespace ZEngine.Systems.Inputs.Devices.Keyboards.Events;

/// <summary>
/// Event args when a change in a key state occurs.
/// </summary>
public class KeyboardEventArgs : DeviceEventArgs
{
    /// <summary>
    /// They key for which the state has changed.
    /// </summary>
    public Key Key { get; }
    
    /// <summary>
    /// The new state of the key.
    /// </summary>
    public KeyState KeyState { get; }

    public KeyboardEventArgs(Key key, KeyState keyState)
    {
        Key = key;
        KeyState = keyState;
    }
}