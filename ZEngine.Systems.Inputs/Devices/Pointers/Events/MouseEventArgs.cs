using ZEngine.Systems.Inputs.Devices.Events;
using ZEngine.Systems.Inputs.Devices.Keyboards;

namespace ZEngine.Systems.Inputs.Devices.Pointers.Events;

/// <summary>
/// Events raised by the mouse device.
/// </summary>
public class MouseEventArgs : DeviceEventArgs
{
    public MouseEventArgs(MouseButton mouseButton, KeyState keyState)
    {
        MouseButton = mouseButton;
        KeyState = keyState;
    }
    
    /// <summary>
    /// Which mouse button changed its state.
    /// </summary>
    public MouseButton MouseButton { get; }
    
    /// <summary>
    /// Current state of the mouse button.
    /// </summary>
    public KeyState KeyState { get; }
}