using ZEngine.Core;
using ZEngine.Core.Game;

namespace ZEngine.Tests.Core.Game.TestSystems;

public class BasicSystem : IGameSystem
{
    /// <summary>
    ///     Counter to interrupt the game loop.
    /// </summary>
    private int _counter;

    public bool Initialized { get; set; }
    public bool Updated { get; set; }
    public bool CleanedUp { get; set; }

    public int Priority => 1;

    public void Initialize()
    {
        Initialized = true;
    }

    public void Update()
    {
        Updated = true;
        if (_counter++ > 5) throw new AbortGameException();
    }

    public void CleanUp()
    {
        CleanedUp = true;
    }
}