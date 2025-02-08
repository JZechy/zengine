using ZEngine.Architecture.Components;
using ZEngine.Testing.System;

namespace ZEngine.Testing.Extensions;

/// <summary>
///     Extends the <see cref="IGameComponent" /> interface with testing methods.
/// </summary>
public static class GameComponentExtension
{
    /// <summary>
    ///     Creates a component predicate watcher and returns it's awaitable task.
    /// </summary>
    /// <param name="component">The type of component on which the predicate is called.</param>
    /// <param name="predicate">Predicate function which result must be met.</param>
    /// <param name="timeoutMs">After which time the task will be cancelled to prevent never-ending run.</param>
    /// <typeparam name="TGameComponent"></typeparam>
    /// <returns></returns>
    public static Task TestPredicate<TGameComponent>(this TGameComponent component, Func<TGameComponent, bool> predicate, int timeoutMs = 1000)
        where TGameComponent : IGameComponent
    {
        return TestManager.CreateComponentPredicateWatcher(component, predicate, timeoutMs).Task;
    }
}