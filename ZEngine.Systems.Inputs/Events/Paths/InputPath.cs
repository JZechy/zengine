using ZEngine.Systems.Inputs.Devices;

namespace ZEngine.Systems.Inputs.Events.Paths;

/// <summary>
///     Input Path is a string representation of a device input target.
/// </summary>
public abstract class InputPath
{
    /// <summary>
    ///     Basic format for the path.
    /// </summary>
    /// <example>Keyboard/Alpha0</example>
    private const string BasePathFormat = "{0}/{1}";

    /// <summary>
    ///     Type of device this path is for.
    /// </summary>
    public abstract DeviceType Device { get; }

    /// <summary>
    ///     String representation of the path.
    /// </summary>
    public string Path => string.Format(BasePathFormat, Device, GetDeviceTargetInput());

    /// <summary>
    ///     Gets the name of device input target.
    /// </summary>
    /// <remarks>
    ///     For example, this is the name of the keyboard key or mouse button.
    /// </remarks>
    /// <returns></returns>
    protected abstract string GetDeviceTargetInput();
}