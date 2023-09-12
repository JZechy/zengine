namespace ZEngine.Systems.ThreadSynchronization.Extensions;

/// <summary>
/// Offers extensions methods on game components to be able to synchronize the calls to the main thread.
/// </summary>
public static class GameComponentExtension
{
    /// <summary>
    /// Synchronizes the call to the main thread.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="action"></param>
    /// <typeparam name="TGameComponent"></typeparam>
    /// <returns></returns>
    public static Task Synchronize<TGameComponent>(this TGameComponent component, Action<TGameComponent> action)
    {
        return ThreadSynchronization.RunAsync(() => action.Invoke(component));
    }

    /// <summary>
    /// Synchronizes the call to the main thread.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="func"></param>
    /// <typeparam name="TGameComponent"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static Task<TResult> Synchronize<TGameComponent, TResult>(this TGameComponent component, Func<TGameComponent, TResult> func)
    {
        return ThreadSynchronization.RunAsync(() => func.Invoke(component));
    }
}