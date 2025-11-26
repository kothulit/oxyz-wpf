using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.ECS.Components;
using SharpDX;

namespace OxyzWPF.ECS.Systems;

public class ExtrudeContourSystem : IOxyzSystem
{
    public string Name => typeof(ExtrudeContourSystem).Name;
    public bool IsEnable { get; set; } = true;

    private readonly World _world;
    private readonly MeshBuilder _meshBuilder = new MeshBuilder();

    public ExtrudeContourSystem(World world)
    {
        _world = world;
    }

    public void Update(double deltaTime)
    {
        var entitiesToExtrude = _world.GetEntitiesWithComponents<SpaceComponent>();

        foreach (var entity in entitiesToExtrude)
        {
            var spaceComponent = entity.GetComponent<SpaceComponent>();
            var meshComponent = entity.GetComponent<MeshComponent>();

            // Пропускаем, если меш уже создан или нет контура
            if (meshComponent?.Geometry != null || spaceComponent.Contour.Count < 3)
                continue;

            // Создаем компоненты, если их нет
            if (meshComponent == null)
                meshComponent = entity.AddComponent<MeshComponent>();

            var transformComponent = entity.GetComponent<TransformComponent>();
            if (transformComponent == null)
                transformComponent = entity.AddComponent<TransformComponent>();

            // Создаем меш выдавливания
            meshComponent.Geometry = CreateExtrudedMesh(spaceComponent.Contour, spaceComponent.Height);
            meshComponent.Material = PhongMaterials.Green;

            // Устанавливаем позицию в центр контура
            var center = CalculateContourCenter(spaceComponent.Contour);
            transformComponent.Position = new Vector3(center.X, spaceComponent.Height / 2.0f, center.Y);
        }
    }

    private Vector2 CalculateContourCenter(List<Vector2> points)
    {
        float sumX = 0, sumY = 0;
        foreach (var point in points)
        {
            sumX += point.X;
            sumY += point.Y;
        }
        return new Vector2(sumX / points.Count, sumY / points.Count);
    }

    private MeshGeometry3D CreateExtrudedMesh(List<Vector2> contour, float height)
    {
        //_meshBuilder.Clear();

        var bottomPoints = contour.Select(p => new Vector3(p.X, 0, p.Y)).ToList();
        var topPoints = contour.Select(p => new Vector3(p.X, height, p.Y)).ToList();

        // Нижняя грань
        if (bottomPoints.Count > 2)
        {
            _meshBuilder.AddPolygon(bottomPoints);
        }

        // Верхняя грань (перевернутая для правильной нормали)
        if (topPoints.Count > 2)
        {
            topPoints.Reverse();
            _meshBuilder.AddPolygon(topPoints);
        }

        // Боковые грани
        for (int i = 0; i < contour.Count; i++)
        {
            int nextIndex = (i + 1) % contour.Count;

            var bottom1 = bottomPoints[i];
            var bottom2 = bottomPoints[nextIndex];
            var top1 = topPoints[i];
            var top2 = topPoints[nextIndex];

            _meshBuilder.AddQuad(bottom1, bottom2, top2, top1);
        }

        return _meshBuilder.ToMeshGeometry3D();
    }
}