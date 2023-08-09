using ZEngine.Systems.Inputs.Devices.Keyboards.Events;
using ZEngine.Systems.Inputs.Events;
using ZEngine.Systems.Inputs.Events.Paths;

namespace ZEngine.Systems.Inputs.Extensions;

public static class KeyboardStateChangedExtension
{
    /// <summary>
    /// Converts native state changed event to the context for input manager.
    /// </summary>
    /// <param name="stateChanged"></param>
    /// <returns></returns>
    public static InputContext<KeyboardContext> ToContext(this KeyboardStateChanged stateChanged)
    {
        KeyboardInputPath inputPath = new()
        {
            Key = stateChanged.Key
        };
        KeyboardContext context = new()
        {
            Key = stateChanged.Key,
            State = stateChanged.KeyState
        };
        
        return new InputContext<KeyboardContext>(inputPath, context);
    }
}