using HelixToolkit.Wpf.SharpDX;
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
}
