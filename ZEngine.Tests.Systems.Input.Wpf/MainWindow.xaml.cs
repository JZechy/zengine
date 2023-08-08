using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ZEngine.Systems.Inputs.Devices.Events;
using ZEngine.Systems.Inputs.Devices.Keyboards;
using ZEngine.Systems.Inputs.Devices.Pointers;
using ZEngine.Systems.Inputs.Devices.Pointers.Events;
using Key = ZEngine.Systems.Inputs.Devices.Keyboards.Key;
using KeyboardDevice = ZEngine.Systems.Inputs.Devices.Keyboards.KeyboardDevice;
using KeyboardEventArgs = ZEngine.Systems.Inputs.Devices.Keyboards.Events.KeyboardEventArgs;
using MouseButton = ZEngine.Systems.Inputs.Devices.Pointers.MouseButton;
using MouseDevice = ZEngine.Systems.Inputs.Devices.Pointers.MouseDevice;
using MouseEventArgs = ZEngine.Systems.Inputs.Devices.Pointers.Events.MouseEventArgs;

namespace ZEngine.Tests.Systems.Input.Wpf;

/// <summary>
/// This window is used to test different windows devices in the input system.
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Instance of device implementation for the keyboard.
    /// </summary>
    private readonly KeyboardDevice _keyboardDevice = new();

    private readonly MouseDevice _mouseDevice = new();

    /// <summary>
    /// Timer used to update the input system.
    /// </summary>
    private readonly DispatcherTimer _updateTimer = new();

    public MainWindow()
    {
        InitializeComponent();

        _keyboardDevice.DeviceEvent += OnKeyboardEvent;
        _mouseDevice.DeviceEvent += MouseDeviceOnDeviceEvent;

        _updateTimer.Interval = TimeSpan.FromMilliseconds(1000 / 60f);
        _updateTimer.Tick += UpdateTick;
        _updateTimer.Start();
    }


    /// <summary>
    /// Every timer tick, update the devices.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateTick(object? sender, EventArgs e)
    {
        _keyboardDevice.Update();
        _mouseDevice.Update();
    }

    private void OnKeyboardEvent(object? sender, DeviceEventArgs e)
    {
        if (e is KeyboardEventArgs keyboardEvent)
        {
            LogKeyboardEvent(keyboardEvent.Key, keyboardEvent.KeyState);
        }
    }

    private void MouseDeviceOnDeviceEvent(object? sender, DeviceEventArgs e)
    {
        if (e is MouseEventArgs mouseEvent)
        {
            LogMouseEvent(mouseEvent.MouseButton, mouseEvent.KeyState);
        }

        if (e is MousePositionEventArgs positionEvent)
        {
            LogMousePosition(positionEvent.MousePosition);
        }
    }

    /// <summary>
    /// Logs a keyboard event to the ListBox.
    /// </summary>
    /// <param name="key">The key that was acted upon.</param>
    /// <param name="state">The state of the key.</param>
    private void LogKeyboardEvent(Key key, KeyState state)
    {
        // Add the event to the ListBox.
        KeyboardEventsListBox.Items.Add($"Key {key} is {state}.");
        KeyboardEventsListBox.ScrollIntoView(KeyboardEventsListBox.Items[^1]);
    }

    private void LogMouseEvent(MouseButton key, KeyState state)
    {
        // Add the event to the ListBox.
        MouseEventsListBox.Items.Add($"Key {key} is {state}.");
        MouseEventsListBox.ScrollIntoView(MouseEventsListBox.Items[^1]);
    }

    private void LogMousePosition(MousePosition mousePosition)
    {
        MouseEventsListBox.Items.Add($"Mouse position is {mousePosition.X}, {mousePosition.Y}.");
        MouseEventsListBox.ScrollIntoView(MouseEventsListBox.Items[^1]);
    }
}