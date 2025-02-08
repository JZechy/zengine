using Microsoft.Extensions.DependencyInjection;
using ZEngine.Core.Game;

namespace ZEngine.Systems.Inputs.Extensions;

public static class GameBuilderExtension
{
    /// <summary>
    ///     Registers the input system and adds it to the game manager.
    /// </summary>
    /// <param name="gameBuilder"></param>
    public static void AddInputSystem(this GameBuilder gameBuilder)
    {
        gameBuilder.Services.AddSingleton<IInputSystem, InputSystem>();
        gameBuilder.AddSystem(x => x.GetRequiredService<IInputSystem>());
    }

    /// <summary>
    ///     Registers a device for the input system.
    /// </summary>
    /// <param name="gameBuilder"></param>
    /// <typeparam name="TDevice"></typeparam>
    public static void AddDevice<TDevice>(this GameBuilder gameBuilder) where TDevice : class, IDevice
    {
        gameBuilder.Services.AddSingleton<IDevice, TDevice>();
    }
}