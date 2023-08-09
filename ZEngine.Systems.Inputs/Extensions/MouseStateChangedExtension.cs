using System.Numerics;
using ZEngine.Systems.Inputs.Devices.Pointers.Events;
using ZEngine.Systems.Inputs.Events;
using ZEngine.Systems.Inputs.Events.Paths;

namespace ZEngine.Systems.Inputs.Extensions;

public static class MouseStateChangedExtension
{
    /// <summary>
    /// Converts native state changed event to the context for input manager.
    /// </summary>
    /// <param name="stateChanged"></param>
    /// <returns></returns>
    public static InputContext<MouseButtonContext> ToContext(this MouseButtonStateChanged stateChanged)
    {
        return new InputContext<MouseButtonContext>
        {
            InputPath = new MouseInputPath
            {
                MouseButton = stateChanged.MouseButton
            },
            Context = new MouseButtonContext
            {
                Button = stateChanged.MouseButton,
                State = stateChanged.KeyState
            }
        };
    }
    
    /// <summary>
    /// Converts native state changed event to the context for input manager.   
    /// </summary>
    /// <param name="stateChanged"></param>
    /// <returns></returns>
    public static InputContext<PointerPositionContext> ToContext(this MousePositionStateChanged stateChanged)
    {
        return new InputContext<PointerPositionContext>
        {
            InputPath = new MousePositionPath(),
            Context = new PointerPositionContext
            {
                Position = new Vector2(stateChanged.MousePosition.X, stateChanged.MousePosition.Y)
            }
        };
    }
}