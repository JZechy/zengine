using System.Runtime.InteropServices;
using ZEngine.Systems.Inputs.Devices.Events;
using ZEngine.Systems.Inputs.Devices.Keyboards;
using ZEngine.Systems.Inputs.Devices.Pointers.Events;

namespace ZEngine.Systems.Inputs.Devices.Pointers;

/// <summary>
/// Reads mouse state.
/// </summary>
public class MouseDevice : IDevice
{
    /// <summary>
    /// Event that is invoked when a mouse event occurs.
    /// </summary>
    public event EventHandler<DeviceEventArgs>? DeviceEvent;

    /// <summary>
    /// Previous keyboard state - This is also containing mouse buttons.
    /// </summary>
    private readonly byte[] _previousState = new byte[256];

    /// <summary>
    /// Current keyboard state - This is also containig mouse button.
    /// </summary>
    private readonly byte[] _currentState = new byte[256];

    /// <summary>
    /// Supported mouse buttons.
    /// </summary>
    private readonly byte[] _supportedKeys = Enum.GetValues<MouseButton>()
        .Select(x => (byte) x)
        .ToArray();

    /// <summary>
    /// The previous position of the mouse.
    /// </summary>
    private MousePosition _previousPositon;

    /// <summary>
    /// Win32 API call to get keyboard state.
    /// </summary>
    /// <remarks>
    /// This method is returning a byte array of 256 bytes, where each byte represents current state of a key. For the scan codes,
    /// refers to the <see cref="KeyScanCode"/>. For the key codes, refers to the <see cref="Key"/>.
    ///
    /// Mouse buttons are part of the keyboard state, and they are represented by the <see cref="MouseButton"/> enum.
    /// </remarks>
    /// <param name="lpKeyState"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    private static extern bool GetKeyboardState(byte[] lpKeyState);

    /// <summary>
    /// This method is used to read the current mouse position.
    /// </summary>
    /// <param name="lpPoint"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out MousePosition lpPoint);

    /// <inheritdoc />
    public void Initialize()
    {
    }

    /// <inheritdoc />
    public void Update()
    {
        ReadMouseButtons();
        ReadMousePosition();
    }

    /// <inheritdoc />
    public void Dispose()
    {
    }

    /// <summary>
    /// Reads current mouse position, if the position changes, it will invoke the <see cref="DeviceEvent"/> with the new position.
    /// </summary>
    private void ReadMousePosition()
    {
        if (!GetCursorPos(out MousePosition position))
        {
            return;
        }

        if (_previousPositon.Equals(position))
        {
            return;
        }

        DeviceEvent?.Invoke(this, new MousePositionEventArgs(position));
        _previousPositon = position;
    }

    /// <summary>
    /// Reads the pressed mouse buttons, and invokes the <see cref="DeviceEvent"/> with the appropriate event.
    /// </summary>
    private void ReadMouseButtons()
    {
        if (!GetKeyboardState(_currentState))
        {
            return;
        }

        foreach (byte key in _supportedKeys)
        {
            bool wasDown = ((KeyScanCode) _previousState[key]).HasFlag(KeyScanCode.Pressed);
            bool isDown = ((KeyScanCode) _currentState[key]).HasFlag(KeyScanCode.Pressed);

            switch (wasDown)
            {
                case true when !isDown:
                    DeviceEvent?.Invoke(this, new MouseEventArgs((MouseButton) key, KeyState.Released));
                    break;
                case false when isDown:
                    DeviceEvent?.Invoke(this, new MouseEventArgs((MouseButton) key, KeyState.Down));
                    DeviceEvent?.Invoke(this, new MouseEventArgs((MouseButton) key, KeyState.Pressed)); // TODO: Pressed requires more logic. And magic :P
                    break;
            }
        }

        Array.Copy(_currentState, _previousState, _currentState.Length);
    }
}