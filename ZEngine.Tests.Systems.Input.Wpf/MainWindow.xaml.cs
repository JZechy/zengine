using System;
using System.Windows;
using System.Windows.Threading;
using ZEngine.Systems.Inputs.Devices.Events;
using ZEngine.Systems.Inputs.Devices.Keyboards;
using ZEngine.Systems.Inputs.Devices.Keyboards.Events;
using Key = ZEngine.Systems.Inputs.Devices.Keyboards.Key;
using KeyboardDevice = ZEngine.Systems.Inputs.Devices.Keyboards.KeyboardDevice;

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
    
    /// <summary>
    /// Timer used to update the input system.
    /// </summary>
    private readonly DispatcherTimer _updateTimer = new();

    public MainWindow()
    {
        InitializeComponent();

        _keyboardDevice.DeviceEvent += OnKeyboardEvent;

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
    }

    private void OnKeyboardEvent(object? sender, DeviceEventArgs e)
    {
        if (e is KeyboardEventArgs keyboardEvent)
        {
            LogKeyboardEvent(keyboardEvent.Key, keyboardEvent.KeyState);
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
}