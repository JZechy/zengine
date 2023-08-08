using ZEngine.Systems.Inputs.Devices.Events;

namespace ZEngine.Systems.Inputs.Devices.Pointers.Events;

public class MousePositionEventArgs : DeviceEventArgs
{
    public MousePositionEventArgs(MousePosition mousePosition)
    {
        MousePosition = mousePosition;
    }
    
    public MousePosition MousePosition { get; }
}