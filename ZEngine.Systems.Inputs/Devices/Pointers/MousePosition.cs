using System.Runtime.InteropServices;

namespace ZEngine.Systems.Inputs.Devices.Pointers;

/// <summary>
///     This struct is used to save mouse position from the platform API.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MousePosition : IEquatable<MousePosition>
{
    public int X;
    public int Y;

    public bool Equals(MousePosition other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is MousePosition other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}