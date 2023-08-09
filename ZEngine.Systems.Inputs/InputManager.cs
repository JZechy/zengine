using System.Collections.Concurrent;
using ZEngine.Systems.Inputs.Events;
using ZEngine.Systems.Inputs.Events.Collections;
using ZEngine.Systems.Inputs.Events.Delegates;
using ZEngine.Systems.Inputs.Events.Paths;

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
    
    /// <summary>
    /// Collection of registered callbacks for a specific input path.
    /// </summary>
    private readonly ConcurrentDictionary<string, IInputPathCollection> _inputPathsCallbacks = new();

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

    /// <summary>
    /// Registers new keyboard input.
    /// </summary>
    /// <param name="inputPath"></param>
    /// <param name="callback"></param>
    public void RegisterKeyboardInput(KeyboardInputPath inputPath, KeyboardInputCallback callback)
    {
        string path = inputPath.Path;
        if (!_inputPathsCallbacks.ContainsKey(path))
        {
            _inputPathsCallbacks.TryAdd(path, new GenericInputPathCollection<KeyboardInputCallback, KeyboardContext>(inputPath));
        }
        
        _inputPathsCallbacks[path].Add(callback);
    }

    /// <summary>
    /// Registers new mouse input.
    /// </summary>
    /// <param name="inputPath"></param>
    /// <param name="callback"></param>
    public void RegisterMouseInput(MouseInputPath inputPath, MouseInputCallback callback)
    {
        string path = inputPath.Path;
        if (!_inputPathsCallbacks.ContainsKey(path))
        {
            _inputPathsCallbacks.TryAdd(path, new GenericInputPathCollection<MouseInputCallback, MouseButtonContext>(inputPath));
        }
        
        _inputPathsCallbacks[path].Add(callback);
    }
}