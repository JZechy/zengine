using System.Runtime.InteropServices;
using ZEngine.Systems.Inputs.Devices.Events;
using ZEngine.Systems.Inputs.Devices.Keyboards.Events;

namespace ZEngine.Systems.Inputs.Devices.Keyboards;

/// <summary>
/// Supported keyboard devices.
/// </summary>
public class KeyboardDevice : IDevice
{
    /// <summary>
    /// Event raised when a keyboard state changes.
    /// </summary>
    public event EventHandler<DeviceEventArgs>? DeviceEvent;

    /// <summary>
    /// What was previous state of the keyboard.
    /// </summary>
    private readonly byte[] _previousState = new byte[256];

    /// <summary>
    /// Current state of the keyboard.
    /// </summary>
    private readonly byte[] _currentState = new byte[256];

    /// <summary>
    /// Win32 API call to get keyboard state.
    /// </summary>
    /// <param name="lpKeyState"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    private static extern bool GetKeyboardState(byte[] lpKeyState);

    /// <inheritdoc />
    public void Initialize()
    {
    }

    /// <inheritdoc />
    public void Update()
    {
        GetKeyboardState(_currentState);

        for (int i = 0; i < _currentState.Length; i++)
        {
            bool wasDown = (_previousState[i] & 0x80) != 0;
            bool isDown = (_currentState[i] & 0x80) != 0;

            switch (wasDown)
            {
                case true when !isDown:
                    DeviceEvent?.Invoke(this, new KeyboardEventArgs((Key) i, KeyState.Released));
                    break;
                case false when isDown:
                    DeviceEvent?.Invoke(this, new KeyboardEventArgs((Key) i, KeyState.Down));
                    DeviceEvent?.Invoke(this, new KeyboardEventArgs((Key) i, KeyState.Pressed)); // Pressed requires more logic.
                    break;
            }
        }

        Array.Copy(_currentState, _previousState, _currentState.Length);
    }

    /// <inheritdoc />
    public void Dispose()
    {
    }
}