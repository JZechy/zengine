using ZEngine.Systems.Inputs.Devices;

namespace ZEngine.Systems.Inputs.Events.Paths;

public class MousePositionPath : InputPath
{
    public override DeviceType Device => DeviceType.Mouse;
    protected override string GetDeviceTargetInput()
    {
        return "Position";
    }
}