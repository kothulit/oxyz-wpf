using HelixToolkit.Wpf.SharpDX;
using OxyzWPF._3DCore;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.ECS.Components;
using SharpDX;
using System.Windows.Documents;

namespace OxyzWPF.ECS;

public static class Factory
{
    public static Entity CreatePoint(IWorld world, Vector3 point)
    {
        Entity pointEntity = world.CreateEntity("Point");

        var transform = pointEntity.AddComponent<TransformComponent>();
        var position = (Vector3)point;
        transform.Position = new Vector3(position.X, 0.0f, position.Z);

        var mesh = pointEntity.AddComponent<MeshComponent>();
        var mb = new MeshBuilder();
        mb.AddSphere(new Vector3(0, 0, 0), 0.1f, 32, 16);
        mesh.Geometry = mb.ToMeshGeometry3D();
        mesh.Material = PhongMaterials.Indigo;

        return pointEntity;
    }

    public static Entity CreateLine(IWorld world, Vector3 startPoint, Vector3 endPoint)
    {
        Entity lineEntity = world.CreateEntity("Line");

        var transform = lineEntity.AddComponent<TransformComponent>();
        var mesh = lineEntity.AddComponent<MeshComponent>();
        var mb = new MeshBuilder();

        mb.AddArrow(endPoint, startPoint, 0.1d);
        mesh.Geometry = mb.ToMeshGeometry3D();
        mesh.Material = PhongMaterials.Indigo;

        return lineEntity;
    }

    public static Entity CreatSurface(IWorld world, List<Vector2> contour, Plane plane)
    {
        if (contour == null || contour.Count < 3)
            throw new ArgumentException("Контур должен содержать минимум 3 точки", nameof(contour));

        Entity surfaceEntity = world.CreateEntity("Surface");

        // Получаем нормаль и точку на плоскости
        Vector3 normal = plane.Normal;
        Vector3 pointOnPlane = normal * plane.D;

        // Создаем базисные векторы для плоскости
        Vector3 u, v;
        if (Math.Abs(normal.Y) < 0.9f)
        {
            // Если нормаль не параллельна оси Y, используем (0,1,0) как опорный вектор
            u = Vector3.Cross(normal, Vector3.UnitY);
        }
        else
        {
            // Если нормаль близка к Y, используем (1,0,0)
            u = Vector3.Cross(normal, Vector3.UnitX);
        }
        u.Normalize();
        v = Vector3.Cross(normal, u);
        v.Normalize();

        // Преобразуем 2D точки контура в 3D точки на плоскости
        var points3D = new List<Vector3>();
        foreach (var point2D in contour)
        {
            // Преобразуем 2D координаты в 3D, используя базисные векторы плоскости
            Vector3 point3D = pointOnPlane + u * point2D.X + v * point2D.Y;
            points3D.Add(point3D);
        }

        // Устанавливаем трансформацию (центр контура)
        var transform = surfaceEntity.AddComponent<TransformComponent>();
        Vector3 center = Calculation.CalculateContourCenter3D(points3D);
        transform.Position = center;

        // Создаем меш
        var mesh = surfaceEntity.AddComponent<MeshComponent>();
        var mb = new MeshBuilder(generateNormals: false, generateTexCoords: false);

        if (points3D.Count > 4)
        {
            // Для более чем 4 точек используем AddTriangleFan
            mb.AddTriangleFan(points3D);
        }
        else if (points3D.Count > 2)
        {
            // Для 3-4 точек используем AddPolygon
            mb.AddPolygon(points3D);
        }

        mesh.Geometry = mb.ToMeshGeometry3D();
        mesh.Material = PhongMaterials.Blue;

        return surfaceEntity;
    }
}
