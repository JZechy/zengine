using Microsoft.Extensions.Logging;
using ZEngine.Systems.Inputs.Devices.Events;
using ZEngine.Systems.Inputs.Devices.Keyboards.Events;
using ZEngine.Systems.Inputs.Devices.Pointers.Events;
using ZEngine.Systems.Inputs.Extensions;

namespace ZEngine.Systems.Inputs;

/// <summary>
/// Input System is responsible for handling user input.
/// </summary>
public class InputSystem : IInputSystem
{
    private readonly ILoggerFactory _loggerFactory;

    /// <summary>
    /// Logger for the input system.
    /// </summary>
    private readonly ILogger<InputSystem> _logger;

    /// <summary>
    /// Collection of all registered devices.
    /// </summary>
    private readonly List<IDevice> _devices = new();

    /// <summary>
    /// Instance of input manager for passing events to the game.
    /// </summary>
    private InputManager _inputManager = null!;

    public InputSystem(ILoggerFactory loggerFactory, IEnumerable<IDevice> devices)
    {
        _loggerFactory = loggerFactory;
        _logger = loggerFactory.CreateLogger<InputSystem>();
        _devices.AddRange(devices);
    }

    /// <summary>
    /// Input System precedes game object system. Because at first we need to know what keys are pressed before we can
    /// do anything with the game objects.
    /// </summary>
    public int Priority => 0;

    /// <summary>
    /// Registers additionaly device to the input system.
    /// </summary>
    /// <param name="device"></param>
    public void AddDevice(IDevice device)
    {
        _devices.Add(device);
    }

    /// <inheritdoc />
    public void Initialize()
    {
        _inputManager = InputManager.CreateInstance(_loggerFactory);
        
        foreach (IDevice device in _devices)
        {
            device.Initialize();
            device.StateChanged += DeviceOnStateChanged;
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
            device.StateChanged -= DeviceOnStateChanged;
            device.Dispose();
        }
    }

    /// <summary>
    /// Handles device events.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void DeviceOnStateChanged(object? sender, DeviceStateChanged e)
    {
        switch (e)
        {
            case KeyboardStateChanged keyboardStateChanged:
                ProcessKeyboardEvent(keyboardStateChanged);
                break;
            case MouseStateChanged mouseStateChanged:
                ProcessMouseEvent(mouseStateChanged);
                break;
        }
    }

    /// <summary>
    /// Process the keyboard and pass the event to the input manager.
    /// </summary>
    /// <param name="keyboardStateChanged"></param>
    private void ProcessKeyboardEvent(KeyboardStateChanged keyboardStateChanged)
    {
        _inputManager.ProcessInput(keyboardStateChanged.ToContext());
    }

    /// <summary>
    /// Process the mouse and pass the event to the input manager.
    /// </summary>
    /// <param name="mouseStateChanged"></param>
    private void ProcessMouseEvent(MouseStateChanged mouseStateChanged)
    {
        switch (mouseStateChanged)
        {
            case MousePositionStateChanged mouseMoved:
                _inputManager.ProcessInput(mouseMoved.ToContext());
                break;
            case MouseButtonStateChanged mouseButtonStateChanged:
                _inputManager.ProcessInput(mouseButtonStateChanged.ToContext());
                break;
        }
    }
}