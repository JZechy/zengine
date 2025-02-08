using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.WinForms;
using ZEngine.Systems.Inputs;
using Timer = System.Windows.Forms.Timer;

namespace ZEngine.Player.Windows;

/// <summary>
///     Form class responsible for rendering the engine's graphics and processing input.
/// </summary>
public partial class ZEnginePlayer : Form
{
    /// <summary>
    ///     Timer used to contiously check device state.
    /// </summary>
    private readonly Timer _engineTimer = new();

    /// <summary>
    ///     Control used to render OpenGL graphics.
    /// </summary>
    private readonly GLControl _glControl;

    /// <summary>
    ///     Instance of engine's input system to process device scaning.
    /// </summary>
    /// <remarks>
    ///     Device scanning must be done on the UI thread. Callbacks from the scanning are handled on the engine's thread.
    /// </remarks>
    private readonly IInputSystem _inputSystem;

    public ZEnginePlayer(IInputSystem inputSystem)
    {
        _inputSystem = inputSystem;
        _glControl = InitializeOpenGl();
        _engineTimer.Interval = 1000 / 60;
        _engineTimer.Tick += EngineTimerOnTick;
        _engineTimer.Start();

        InitializeComponent();
    }

    /// <summary>
    ///     Initializes the OpenGL control.
    /// </summary>
    /// <returns></returns>
    private GLControl InitializeOpenGl()
    {
        GLControl control = new();
        control.Dock = DockStyle.Fill;
        control.Load += ControlOnLoad;
        control.Paint += ControlOnPaint;
        control.Resize += ControlOnResize;

        Controls.Add(control);

        return control;
    }

    /// <summary>
    ///     Process callbacks for the engine, that must be done on the form thread.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EngineTimerOnTick(object? sender, EventArgs e)
    {
        _inputSystem.ScanDevices();
    }

    /// <summary>
    ///     Operation to perform when the control is loaded.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ControlOnLoad(object? sender, EventArgs e)
    {
        GL.ClearColor(Color4.CornflowerBlue);
    }

    /// <summary>
    ///     Process rendering of the control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ControlOnPaint(object? sender, PaintEventArgs e)
    {
        _glControl.MakeCurrent();
        GL.Clear(ClearBufferMask.ColorBufferBit);

        _glControl.SwapBuffers();
    }

    /// <summary>
    ///     Proces resizing of the control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ControlOnResize(object? sender, EventArgs e)
    {
        GL.Viewport(0, 0, _glControl.Width, _glControl.Height);
    }
}