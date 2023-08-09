namespace ZEngine.Systems.Inputs.Devices.Pointers.Events;

public class MousePositionStateChanged : MouseStateChanged
{
    public MousePositionStateChanged(MousePosition mousePosition)
    {
        MousePosition = mousePosition;
    }
    
    public MousePosition MousePosition { get; }
}