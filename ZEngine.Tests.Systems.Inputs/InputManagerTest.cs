using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using ZEngine.Systems.Inputs;
using ZEngine.Systems.Inputs.Devices.Keyboards;
using ZEngine.Systems.Inputs.Events;
using ZEngine.Systems.Inputs.Events.Paths;
using ZEngine.Systems.Inputs.Extensions;

namespace ZEngine.Tests.Systems.Inputs;

public class InputManagerTest
{
    /// <summary>
    /// Basic tests of calling callback.
    /// </summary>
    [Test]
    public void Test_KeyboardInput()
    {
        bool called = false;
        InputManager inputManager = InputManager.CreateInstance(new NullLoggerFactory());
        inputManager.RegisterKeyboardInput(new KeyboardInputPath(Key.W), KeyboardCallback);
        
        inputManager.ProcessInput(new InputContext<KeyboardContext>(new KeyboardInputPath(Key.A), new KeyboardContext
        {
            State = KeyState.Pressed
        }));
        called.Should().BeFalse();
        
        inputManager.ProcessInput(new InputContext<KeyboardContext>(new KeyboardInputPath(Key.W), new KeyboardContext
        {
            State = KeyState.Pressed
        }));
        called.Should().BeTrue();

        called = false;
        inputManager.UnregisterKeyboardInput(new KeyboardInputPath(Key.W), KeyboardCallback);
        inputManager.ProcessInput(new InputContext<KeyboardContext>(new KeyboardInputPath(Key.W), new KeyboardContext
        {
            State = KeyState.Pressed
        }));
        called.Should().BeFalse();
        
        return;

        void KeyboardCallback(InputContext<KeyboardContext> context)
        {
            called = true;
        }
    }
}