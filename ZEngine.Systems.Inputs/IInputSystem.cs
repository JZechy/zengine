using ZEngine.Core;

namespace ZEngine.Systems.Inputs;

public interface IInputSystem : IGameSystem
{
    /// <summary>
    ///     Scan devices for changes.
    /// </summary>
    void ScanDevices();
}