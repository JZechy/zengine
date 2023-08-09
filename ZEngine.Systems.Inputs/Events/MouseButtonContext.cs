using ZEngine.Systems.Inputs.Devices.Keyboards;
using ZEngine.Systems.Inputs.Devices.Pointers;

namespace ZEngine.Systems.Inputs.Events;

public struct MouseButtonContext
{
    public MouseButton Button { get; set; }
    public KeyState State { get; set; }
}