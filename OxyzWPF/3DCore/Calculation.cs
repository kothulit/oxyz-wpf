using HelixToolkit.Wpf.SharpDX;
using SharpDX;

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
}