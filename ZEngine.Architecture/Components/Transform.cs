using System.Collections;
using System.Numerics;

namespace ZEngine.Architecture.Components;

/// <summary>
/// Basic component providing transform functionality.
/// </summary>
public class Transform : GameComponent, IEnumerable<Transform>
{
    /// <summary>
    /// Collection of all available children of this transform.
    /// </summary>
    private readonly HashSet<Transform> _children = new();
    
    /// <summary>
    /// Position of the game object.
    /// </summary>
    public Vector3 Position { get; set; }
    
    /// <summary>
    /// Parent of the game object. If null, game object is a root object.
    /// </summary>
    public Transform? Parent { get; private set; }

    /// <summary>
    /// Sets parent of this transform.
    /// </summary>
    /// <param name="parent"></param>
    public void SetParent(Transform? parent)
    {
        if (Parent is null)
        {
            Parent?._children.Remove(this);
        }

        Parent = parent;
        Parent?._children.Add(this);
    }
    
    /// <inheritdoc />
    public IEnumerator<Transform> GetEnumerator()
    {
        return _children.GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}