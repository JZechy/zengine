using Microsoft.Extensions.DependencyInjection;
using ZEngine.Core.Game;

namespace ZEngine.Systems.Inputs.Extensions;

public static class GameBuilderExtension
{
    public static void AddDevice<TDevice>(this GameBuilder gameBuilder) where TDevice : class, IDevice
    {
        gameBuilder.Services.AddSingleton<IDevice, TDevice>();
    }
}