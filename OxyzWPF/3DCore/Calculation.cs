using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using System.Windows.Input;

namespace OxyzWPF._3DCore;

public static class Calculation
{
    public static bool GetScreenPointOnZeroPlane(Viewport3DX viewport, Vector2 screenPoint, out Vector3 planePoint)
    {
        bool isIntersects = false;
        planePoint = Vector3.Zero;
        var plane = new Plane(new Vector3(0, 1, 0), 0);
        var ray = viewport.UnProject(screenPoint);
        if (ray.Intersects(ref plane, out float distance))
        {
            planePoint = ray.Position + ray.Direction * distance;
            isIntersects = true;
        }
        return isIntersects;
    }

    public static bool GetScreenPointOnZeroPlane(object? sender, MouseEventArgs e, out Vector3 planePoint)
    {
        bool isIntersects = false;
        planePoint = Vector3.Zero;
        if (sender is Viewport3DX viewport)
        {
            var position = e.GetPosition(viewport);
            var point2D = new Vector2((float)position.X, (float)position.Y);
            isIntersects = GetScreenPointOnZeroPlane(viewport, point2D, out planePoint);
        }
        return isIntersects;
    }
}