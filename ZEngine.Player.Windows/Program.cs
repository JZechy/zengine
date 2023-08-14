using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ZEngine.Core.Game;
using ZEngine.Systems.Inputs;
using ZEngine.Systems.Inputs.Devices.Keyboards;
using ZEngine.Systems.Inputs.Events.Paths;
using ZEngine.Systems.Inputs.Extensions;

namespace ZEngine.Player.Windows;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        GameManager gameManager = InitializeEngine();
        gameManager.Start();
        InputHook();
        
        ZEnginePlayer player = new();
        player.Text = "ZEngine Windows Player";
        
        Application.Run(player);
    }

    private static GameManager InitializeEngine()
    {
        GameBuilder builder = GameBuilder.Create();
        builder.Services.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));
        builder.Services.AddSingleton(typeof(ILoggerFactory), typeof(NullLoggerFactory));
        
        builder.AddDevice<KeyboardDevice>();
        builder.AddSystem<InputSystem>();

        return builder.Build();
    }

    private static void InputHook()
    {
        InputManager.Instance.RegisterKeyboardInput(new KeyboardInputPath(Key.A), context =>
        {
            Console.WriteLine(context.InputPath);
        });
    }
}