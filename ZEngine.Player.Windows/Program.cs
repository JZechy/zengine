using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ZEngine.Core.Game;
using ZEngine.Systems.GameObjects.Extensions;
using ZEngine.Systems.Inputs;
using ZEngine.Systems.Inputs.Devices.Keyboards;
using ZEngine.Systems.Inputs.Devices.Pointers;
using ZEngine.Systems.Inputs.Events.Paths;
using ZEngine.Systems.Inputs.Extensions;

namespace ZEngine.Player.Windows;

/// <summary>
/// First player implementation for the engine.
/// </summary>
/// <remarks>
/// This class represents how the engine should be in general initialized. Because we are expecting creation of custom tools for the engine,
/// the actual "game sources" or the custom Assembly, should be registered automaticaly inside the target player project. Asset pipeline
/// should be handling all the loading of assets or scenes.
///
/// General steps to initialize the player are:
/// - Defines all basic services, like logging.
/// - Initialize the platform specific input devices.
/// - Register core systems of the engine.
/// - Register any additional system available (How to solve this is subject for future development).
/// </remarks>
internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        IGameManager gameManager = InitializeEngine();
        gameManager.Start();
        InputHook();
        
        ZEnginePlayer player = new(gameManager.ServiceProvider.GetRequiredService<IInputSystem>());
        player.Text = "ZEngine Windows Player";
        
        Application.Run(player);
    }

    /// <summary>
    /// Initializes the engine with the default services.
    /// </summary>
    /// <returns></returns>
    private static IGameManager InitializeEngine()
    {
        GameBuilder builder = GameBuilder.Create();
        
        // Common services, without any specific implementation. (Right now)
        builder.Services.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));
        builder.Services.AddSingleton(typeof(ILoggerFactory), typeof(NullLoggerFactory));
        
        // Register platform specific devices.
        builder.AddDevice<KeyboardDevice>();
        builder.AddDevice<MouseDevice>();
        
        // Register core systems of the engine.
        builder.AddGameObjectSystem();
        builder.AddInputSystem();

        return builder.Build();
    }

    /// <summary>
    /// To test the input system, we hook the input events here.
    /// </summary>
    private static void InputHook()
    {
        InputManager.Instance.RegisterKeyboardInput(new KeyboardInputPath(Key.A), context =>
        {
            Console.WriteLine($"Key A: {context.Context.State}");
        });
        
        InputManager.Instance.RegisterPointerPositionInput(context =>
        {
            Console.WriteLine($"Pointer position: {context.Context.Position}");
        });
    }
}