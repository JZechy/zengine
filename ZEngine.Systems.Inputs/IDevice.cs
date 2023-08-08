using ZEngine.Systems.Inputs.Devices.Events;

namespace ZEngine.Systems.Inputs;

/// <summary>
/// Define common interface for all input devices.
/// </summary>
public interface IDevice : IDisposable
{
    /// <summary>
    /// Event raised when device state changes.
    /// </summary>
    event EventHandler<DeviceEventArgs> DeviceEvent; 
    
    /// <summary>
    /// Initializes the device.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Updates the devices current state.
    /// </summary>
    void Update();
}