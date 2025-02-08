using ZEngine.Systems.Inputs.Devices.Events;

namespace ZEngine.Systems.Inputs;

/// <summary>
///     Define common interface for all input devices.
/// </summary>
public interface IDevice : IDisposable
{
    /// <summary>
    ///     Event raised when device state changes.
    /// </summary>
    event EventHandler<DeviceStateChanged> StateChanged;

    /// <summary>
    ///     Initializes the device.
    /// </summary>
    void Initialize();

    /// <summary>
    ///     Scan the device for changes.
    /// </summary>
    void Scan();
}