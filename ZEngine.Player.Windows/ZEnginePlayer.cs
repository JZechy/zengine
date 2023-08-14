using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.WinForms;

namespace ZEngine.Player.Windows;

public partial class ZEnginePlayer : Form
{
    private readonly GLControl _glControl;

    public ZEnginePlayer()
    {
        InitializeComponent();
        _glControl = InitializeOpenGl();
    }

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

    private void ControlOnLoad(object? sender, EventArgs e)
    {
        GL.ClearColor(Color4.CornflowerBlue);
    }

    private void ControlOnPaint(object? sender, PaintEventArgs e)
    {
        _glControl.MakeCurrent();
        GL.Clear(ClearBufferMask.ColorBufferBit);

        _glControl.SwapBuffers();
    }

    private void ControlOnResize(object? sender, EventArgs e)
    {
        GL.Viewport(0, 0, _glControl.Width, _glControl.Height);
    }
}