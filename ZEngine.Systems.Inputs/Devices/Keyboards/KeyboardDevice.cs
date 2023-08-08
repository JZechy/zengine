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
    /// Array of supported keys.
    /// </summary>
    private readonly byte[] _supportedKeys = Enum.GetValues<Key>()
        .Select(x => (byte) x)
        .ToArray();

    /// <summary>
    /// Win32 API call to get keyboard state.
    /// </summary>
    /// <remarks>
    /// This method is returning a byte array of 256 bytes, where each byte represents current state of a key. For the scan codes,
    /// refers to the <see cref="KeyScanCode"/>. For the key codes, refers to the <see cref="Key"/>.
    /// </remarks>
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
                    DeviceEvent?.Invoke(this, new KeyboardEventArgs((Key) key, KeyState.Released));
                    break;
                case false when isDown:
                    DeviceEvent?.Invoke(this, new KeyboardEventArgs((Key) key, KeyState.Down));
                    DeviceEvent?.Invoke(this, new KeyboardEventArgs((Key) key, KeyState.Pressed)); // TODO: Pressed requires more logic. And magic :P
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