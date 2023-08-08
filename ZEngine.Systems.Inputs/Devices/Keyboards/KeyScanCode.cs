namespace ZEngine.Systems.Inputs.Devices.Keyboards;

/// <summary>
/// For every key we receive a byte that contains information about the key. This enum is describing these scan codes in a bit more readable way.
/// </summary>
[Flags]
public enum KeyScanCode : byte
{
    /// <summary>
    /// No scan code. This is the default value, meaning that the key wasn't pressed or used in any way.
    /// </summary>
    None = 0b_0000_0000,
    
    /// <summary>
    /// The key is toggled.
    /// </summary>
    /// <remarks>
    /// This is used for keys like Caps Lock, Num Lock, etc.
    /// </remarks>
    ToggleOn = 0b_0000_0001,
    
    /// <summary>
    /// Reserved, unused.
    /// </summary>
    Reserved1 = 0b_0000_0010,
    
    /// <summary>
    /// Reserved, unused.
    /// </summary>
    Reserved2 = 0b_0000_0100,
    
    /// <summary>
    /// Reserved, unused.
    /// </summary>
    Reserved3 = 0b_0000_1000,
    
    /// <summary>
    /// Reserved, unused.
    /// </summary>
    Reserved4 = 0b_0001_0000,
    
    /// <summary>
    /// Reserved, unused.
    /// </summary>
    Reserved5 = 0b_0010_0000,
    
    /// <summary>
    /// Reserved, unused.
    /// </summary>
    Reserved6 = 0b_0100_0000,
    
    /// <summary>
    /// Top level bit is indicating that the key was pressed.
    /// </summary>
    Pressed = 0b_1000_0000
}