using ZEngine.Systems.Inputs.Devices.Events;

namespace ZEngine.Systems.Inputs.Devices.Keyboards.Events;

public class KeyboardEventArgs : DeviceEventArgs
{
    public Key Key { get; }
    public KeyState KeyState { get; }

    public KeyboardEventArgs(Key key, KeyState keyState)
    {
        Key = key;
        KeyState = keyState;
    }
}