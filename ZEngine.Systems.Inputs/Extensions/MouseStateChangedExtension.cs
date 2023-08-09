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
        MouseInputPath inputPath = new()
        {
            MouseButton = stateChanged.MouseButton
        };
        MouseButtonContext context = new()
        {
            Button = stateChanged.MouseButton,
            State = stateChanged.KeyState
        };

        return new InputContext<MouseButtonContext>(inputPath, context);
    }

    /// <summary>
    /// Converts native state changed event to the context for input manager.   
    /// </summary>
    /// <param name="stateChanged"></param>
    /// <returns></returns>
    public static InputContext<PointerPositionContext> ToContext(this MousePositionStateChanged stateChanged)
    {
        MousePositionPath inputPath = new();
        PointerPositionContext context = new()
        {
            Position = new Vector2(stateChanged.MousePosition.X, stateChanged.MousePosition.Y)
        };

        return new InputContext<PointerPositionContext>(inputPath, context);
    }
}