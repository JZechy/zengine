using ZEngine.Architecture.Components;

namespace ZEngine.Tests.Systems.GameObjects.TestingComponents;

public class SystemTestComponent : GameComponent
{
    public bool AwakeCalled { get; private set; }
    public bool OnEnableCalled { get; private set; }
    public bool OnDestroyCalled { get; private set; }
    public bool UpdateCalled { get; private set; }
    
    private void Awake()
    {
        AwakeCalled = true;
    }

    private void OnEnable()
    {
        OnEnableCalled = true;
    }
    
    private void Update()
    {
        UpdateCalled = true;
    }

    private void OnDestroy()
    {
        OnDestroyCalled = true;
    }
}