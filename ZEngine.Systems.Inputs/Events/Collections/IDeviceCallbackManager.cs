namespace ZEngine.Systems.Inputs.Events.Collections;

public interface IDeviceCallbackManager
{
    /// <summary>
    ///     Type of context this callback manager handles.
    /// </summary>
    Type ContextType { get; }

    /// <summary>
    ///     Registers new callback for given path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="callback"></param>
    void Register(string path, Delegate callback);

    /// <summary>
    ///     Removes callback for given path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="callback"></param>
    void Unregister(string path, Delegate callback);

    /// <summary>
    ///     Invokes all callbacks for given path.
    /// </summary>
    /// <param name="inputContext"></param>
    void Invoke(InputContext inputContext);
}