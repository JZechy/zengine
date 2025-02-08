using ZEngine.Systems.Inputs.Devices.Keyboards;

namespace ZEngine.Systems.Inputs.Devices.Pointers.Events;

/// <summary>
///     Event raised when the state of a mouse button changes.
/// </summary>
public class MouseButtonStateChanged : MouseStateChanged
{
    public MouseButtonStateChanged(MouseButton mouseButton, KeyState keyState)
    {
        MouseButton = mouseButton;
        KeyState = keyState;
    }

    /// <summary>
    ///     Which mouse button changed its state.
    /// </summary>
    public MouseButton MouseButton { get; }

    /// <summary>
    ///     Current state of the mouse button.
    /// </summary>
    public KeyState KeyState { get; }
}