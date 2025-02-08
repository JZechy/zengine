using ZEngine.Systems.Inputs.Devices;
using ZEngine.Systems.Inputs.Devices.Keyboards;

namespace ZEngine.Systems.Inputs.Events.Paths;

/// <summary>
///     Marks a path as a keyboard input path.
/// </summary>
public class KeyboardInputPath : InputPath
{
    public KeyboardInputPath()
    {
    }

    public KeyboardInputPath(Key key)
    {
        Key = key;
    }

    /// <inheritdoc />
    public override DeviceType Device => DeviceType.Keyboard;

    /// <summary>
    ///     Key which this path is for.
    /// </summary>
    public Key Key { get; set; }

    /// <inheritdoc />
    protected override string GetDeviceTargetInput()
    {
        return Key.ToString();
    }
}