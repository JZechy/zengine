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
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly KeyboardDevice _keyboardDevice = new();
    private readonly DispatcherTimer _updateTimer = new();

    public MainWindow()
    {
        InitializeComponent();

        _keyboardDevice.DeviceEvent += OnKeyboardEvent;

        // Nastavení intervalu pro pravidelný update.
        _updateTimer.Interval = TimeSpan.FromMilliseconds(1000 / 60f); // Přibližně 60 FPS
        _updateTimer.Tick += UpdateTick;

        // Startování timeru.
        _updateTimer.Start();
    }

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
    public void LogKeyboardEvent(Key key, KeyState state)
    {
        // Add the event to the ListBox.
        KeyboardEventsListBox.Items.Add($"Key {key} is {state}.");

        // Scroll to the last added item to always display the newest event.
        KeyboardEventsListBox.ScrollIntoView(KeyboardEventsListBox.Items[^1]);
    }
}