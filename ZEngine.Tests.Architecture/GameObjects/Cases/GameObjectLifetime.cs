using ZEngine.Architecture.Components;

namespace ZEngine.Tests.Architecture.GameObjects.Cases;

public class GameObjectLifetime : GameComponent
{
    public bool Awaken { get; set; }
    public bool EnableCalled { get; set; }
    public bool DisbleCalled { get; set; }
    public bool Destroyed { get; set; }
    
    private void Awake()
    {
        Awaken = true;
    }

    private void OnEnable()
    {
        EnableCalled = true;
    }

    private void OnDisable()
    {
        EnableCalled = false;
        DisbleCalled = true;
    }

    private void OnDestroy()
    {
        Destroyed = true;
    }
}