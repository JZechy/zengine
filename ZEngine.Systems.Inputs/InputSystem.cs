using Microsoft.Extensions.Logging;
using ZEngine.Systems.Inputs.Devices.Events;
using ZEngine.Systems.Inputs.Devices.Keyboards;

namespace ZEngine.Systems.Inputs;

/// <summary>
/// Input System is responsible for handling user input.
/// </summary>
public class InputSystem : IInputSystem
{
    /// <summary>
    /// Logger for the input system.
    /// </summary>
    private readonly ILogger<InputSystem> _logger;

    /// <summary>
    /// Collection of all registered devices.
    /// </summary>
    private readonly List<IDevice> _devices = new();

    public InputSystem(ILogger<InputSystem> logger)
    {
        _logger = logger;
        
        _devices.Add(new KeyboardDevice());
    }

    /// <summary>
    /// Input System precedes game object system. Because at first we need to know what keys are pressed before we can
    /// do anything with the game objects.
    /// </summary>
    public int Priority => 0;

    /// <inheritdoc />
    public void Initialize()
    {
        foreach (IDevice device in _devices)
        {
            device.Initialize();
            device.DeviceEvent += DeviceOnDeviceEvent;
        }
    }

    /// <inheritdoc />
    public void Update()
    {
        foreach (IDevice device in _devices)
        {
            device.Update();
        }
    }

    /// <inheritdoc />
    public void CleanUp()
    {
        foreach (IDevice device in _devices)
        {
            device.DeviceEvent -= DeviceOnDeviceEvent;
            device.Dispose();
        }
    }

    /// <summary>
    /// Handles device events.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void DeviceOnDeviceEvent(object? sender, DeviceEventArgs e)
    {
        // TODO: Handles the device event, and dispatches to the public API.
    }
}