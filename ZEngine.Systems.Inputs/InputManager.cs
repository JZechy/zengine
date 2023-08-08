namespace ZEngine.Systems.Inputs;

/// <summary>
/// Input Manager is a exposed public API that can be used to bind input events to actions.
/// </summary>
public class InputManager
{
    /// <summary>
    /// Actual instance of the input manager.
    /// </summary>
    private static InputManager? _instance;
    
    /// <summary>
    /// Reference to the input system.
    /// </summary>
    private readonly InputSystem _inputSystem;

    private InputManager(InputSystem inputSystem)
    {
        _inputSystem = inputSystem;
    }

    /// <summary>
    /// Gets current instance of input manager.
    /// </summary>
    /// <exception cref="InvalidOperationException">Input manager was not initialized.</exception>
    public static InputManager Instance
    {
        get
        {
            if (_instance is null)
            {
                throw new InvalidOperationException("Input Manager is not initialized.");
            }

            return _instance;
        }
    }

    /// <summary>
    /// Factory method to create a new instance of the input manager.
    /// </summary>
    /// <param name="inputSystem"></param>
    internal static void CreateInstance(InputSystem inputSystem)
    {
        _instance = new InputManager(inputSystem);
    }
}