using ZEngine.Architecture.Components;

namespace ZEngine.Tests.Architecture.Components.Cases;

/// <summary>
/// Implmenet for testing component lifetime.
/// </summary>
public class LifetimeComponent : GameComponent
{
    public bool OnEnableCalled { get; set; }
    public bool OnDisableCalled { get; set; }
    
    public void OnEnable()
    {
        OnEnableCalled = true;
    }

    public void OnDisable()
    {
        OnDisableCalled = true;
    }
}