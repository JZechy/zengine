using ZEngine.Systems.Inputs.Events;
using ZEngine.Systems.Inputs.Events.Paths;

namespace ZEngine.Systems.Inputs.Extensions;

public static class InputManagerExtensions
{
    /// <summary>
    ///     Register keyboard input for given path.
    /// </summary>
    /// <param name="inputManager"></param>
    /// <param name="path"></param>
    /// <param name="context"></param>
    public static void RegisterKeyboardInput(this InputManager inputManager, KeyboardInputPath path, Action<InputContext<KeyboardContext>> context)
    {
        inputManager.Register(path, context);
    }

    /// <summary>
    ///     Removes keyboard input for given path.
    /// </summary>
    /// <param name="inputManager"></param>
    /// <param name="path"></param>
    /// <param name="context"></param>
    public static void UnregisterKeyboardInput(this InputManager inputManager, KeyboardInputPath path, Action<InputContext<KeyboardContext>> context)
    {
        inputManager.Unregister(path, context);
    }

    /// <summary>
    ///     Registers mouse button input for given path.
    /// </summary>
    /// <param name="inputManager"></param>
    /// <param name="path"></param>
    /// <param name="context"></param>
    public static void RegisterMouseButtonInput(this InputManager inputManager, MouseInputPath path, Action<InputContext<MouseButtonContext>> context)
    {
        inputManager.Register(path, context);
    }

    /// <summary>
    ///     Removes mouse button input for given path.
    /// </summary>
    /// <param name="inputManager"></param>
    /// <param name="path"></param>
    /// <param name="context"></param>
    public static void UnregisterMouseButtonInput(this InputManager inputManager, MouseInputPath path, Action<InputContext<MouseButtonContext>> context)
    {
        inputManager.Unregister(path, context);
    }

    /// <summary>
    ///     Register mouse position input.
    /// </summary>
    /// <param name="inputManager"></param>
    /// <param name="context"></param>
    public static void RegisterPointerPositionInput(this InputManager inputManager, Action<InputContext<PointerPositionContext>> context)
    {
        inputManager.Register(new MousePositionPath(), context);
    }

    /// <summary>
    ///     Removes mouse position input.
    /// </summary>
    /// <param name="inputManager"></param>
    /// <param name="context"></param>
    public static void UnregisterPointerPositionInput(this InputManager inputManager, Action<InputContext<PointerPositionContext>> context)
    {
        inputManager.Unregister(new MousePositionPath(), context);
    }
}