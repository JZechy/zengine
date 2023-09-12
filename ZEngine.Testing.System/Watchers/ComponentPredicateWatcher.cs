using ZEngine.Architecture.Components;

namespace ZEngine.Testing.System.Watchers;

/// <summary>
/// Instance of watcher that is used to wait until the predicate is met.
/// </summary>
/// <typeparam name="TGameComponent"></typeparam>
public class ComponentPredicateWatcher<TGameComponent> : Watcher
    where TGameComponent : IGameComponent
{
    /// <summary>
    /// Instance of the component that is used to check the predicate.
    /// </summary>
    private readonly TGameComponent _component;
    
    /// <summary>
    /// Predicate callback that is used to check the component.
    /// </summary>
    private readonly Func<TGameComponent, bool> _predicate;

    public ComponentPredicateWatcher(TGameComponent component, Func<TGameComponent, bool> predicate, int timeoutMs) : base(timeoutMs)
    {
        _component = component;
        _predicate = predicate;
    }

    /// <summary>
    /// Check the predicate return value.
    /// </summary>
    public override void Check()
    {
        if (!_predicate.Invoke(_component))
        {
            return;
        }

        Source.TrySetResult();
    }
}