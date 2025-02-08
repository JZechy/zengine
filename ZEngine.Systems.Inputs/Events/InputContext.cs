using ZEngine.Systems.Inputs.Events.Paths;

namespace ZEngine.Systems.Inputs.Events;

/// <summary>
///     Basic context describing the input event.
/// </summary>
public class InputContext
{
    public InputContext(InputPath inputPath)
    {
        InputPath = inputPath;
    }

    /// <summary>
    ///     Base input path that was triggered.
    /// </summary>
    public InputPath InputPath { get; }
}

/// <summary>
///     Generic extension providing access to the context of the event.
/// </summary>
/// <typeparam name="TContext"></typeparam>
public class InputContext<TContext> : InputContext
{
    public InputContext(InputPath inputPath, TContext context) : base(inputPath)
    {
        Context = context;
    }

    /// <summary>
    ///     Context class that hold specific data for the callback.
    /// </summary>
    public TContext Context { get; }
}