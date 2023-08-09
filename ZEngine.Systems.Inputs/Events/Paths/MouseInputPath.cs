using ZEngine.Systems.Inputs.Devices;
using ZEngine.Systems.Inputs.Devices.Pointers;

namespace ZEngine.Systems.Inputs.Events.Paths;

/// <summary>
/// Marks a path as a mouse input path.
/// </summary>
public class MouseInputPath : InputPath
{
    public MouseInputPath()
    {
    }

    public MouseInputPath(MouseButton mouseButton)
    {
        MouseButton = mouseButton;
    }
    
    /// <inheritdoc />
    public override DeviceType Device => DeviceType.Mouse;
    
    /// <summary>
    /// The mouse button which this path is for.
    /// </summary>
    public MouseButton MouseButton { get; set; }

    /// <inheritdoc />
    protected override string GetDeviceTargetInput()
    {
        return MouseButton.ToString();
    }
}