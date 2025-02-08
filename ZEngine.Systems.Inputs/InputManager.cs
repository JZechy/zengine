using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using ZEngine.Systems.Inputs.Events;
using ZEngine.Systems.Inputs.Events.Collections;
using ZEngine.Systems.Inputs.Events.Paths;

namespace ZEngine.Systems.Inputs;

/// <summary>
///     Input Manager is a exposed public API that can be used to bind input events to actions.
/// </summary>
public class InputManager
{
    /// <summary>
    ///     Actual instance of the input manager.
    /// </summary>
    private static InputManager? _instance;

    /// <summary>
    ///     Collection of callback managers.
    /// </summary>
    private readonly ConcurrentBag<IDeviceCallbackManager> _callbackManagers;

    private readonly ILogger<InputManager> _logger;

    private InputManager(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<InputManager>();

        _callbackManagers = new ConcurrentBag<IDeviceCallbackManager>
        {
            new KeyboardInputCallbackManager(loggerFactory),
            new MouseButtonCallbackManager(loggerFactory),
            new PointerPositionCallbackManager(loggerFactory)
        };
    }

    /// <summary>
    ///     Gets current instance of input manager.
    /// </summary>
    /// <exception cref="InvalidOperationException">Input manager was not initialized.</exception>
    public static InputManager Instance
    {
        get
        {
            if (_instance is null) throw new InvalidOperationException("Input Manager is not initialized.");

            return _instance;
        }
    }

    /// <summary>
    ///     Factory method to create a new instance of the input manager.
    /// </summary>
    internal static InputManager CreateInstance(ILoggerFactory loggerFactory)
    {
        _instance = new InputManager(loggerFactory);
        return _instance;
    }

    /// <summary>
    ///     Pass the input context to all registered callbacks.
    /// </summary>
    /// <param name="inputContext"></param>
    internal void ProcessInput(InputContext inputContext)
    {
        IDeviceCallbackManager callbackManager = GetCallbackManager(inputContext);
        callbackManager.Invoke(inputContext);
    }

    /// <summary>
    ///     Registers new input callback for given device path.
    /// </summary>
    /// <param name="devicePath"></param>
    /// <param name="callback"></param>
    /// <typeparam name="TPath"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public void Register<TPath, TContext>(TPath devicePath, Action<InputContext<TContext>> callback) where TPath : InputPath
    {
        IDeviceCallbackManager callbackManager = GetCallbackManager(typeof(InputContext<TContext>));
        callbackManager.Register(devicePath.Path, callback);
    }

    /// <summary>
    ///     Removes input callback for given device path.
    /// </summary>
    /// <param name="devicePath"></param>
    /// <param name="callback"></param>
    /// <typeparam name="TPath"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public void Unregister<TPath, TContext>(TPath devicePath, Action<InputContext<TContext>> callback) where TPath : InputPath
    {
        IDeviceCallbackManager callbackManager = GetCallbackManager(typeof(InputContext<TContext>));
        callbackManager.Unregister(devicePath.Path, callback);
    }

    /// <summary>
    ///     Finds a suitable manager by given input contex type.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private IDeviceCallbackManager GetCallbackManager(InputContext context)
    {
        Type contextType = context.GetType();

        return GetCallbackManager(contextType);
    }

    /// <summary>
    ///     Finds a suitable manager by given input contex type.
    /// </summary>
    /// <param name="contextType"></param>
    /// <returns></returns>
    private IDeviceCallbackManager GetCallbackManager(Type contextType)
    {
        try
        {
            return _callbackManagers.First(manager => manager.ContextType == contextType);
        }
        catch (InvalidOperationException)
        {
            _logger.LogError("Could not find a suitable callback manager for given context type: {ContextType}", contextType);
            throw;
        }
    }
}