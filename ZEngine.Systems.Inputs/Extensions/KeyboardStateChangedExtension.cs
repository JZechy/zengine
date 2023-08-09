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
        return new InputContext<KeyboardContext>
        {
            InputPath = new KeyboardInputPath
            {
                Key = stateChanged.Key
            },
            Context = new KeyboardContext
            {
                Key = stateChanged.Key,
                State = stateChanged.KeyState
            }
        };
    }
}