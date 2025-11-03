using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.ECS.Components;
using SharpDX;

namespace OxyzWPF.ECS;

public static class Factory
{
    public static Entity CreatePoint(IWorld world, Vector3 point)
    {
        Entity pointEntity = world.CreateEntity("Point");

        var transform = pointEntity.AddComponent<TransformComponent>();
        var position = (Vector3)point;
        transform.Position = new Vector3(position.X, 0.5f, position.Z);

        var mesh = pointEntity.AddComponent<MeshComponent>();
        var mb = new MeshBuilder();
        mb.AddSphere(new Vector3(0, 0, 0), 0.5f, 32, 16);
        mesh.Geometry = mb.ToMeshGeometry3D();
        mesh.Material = PhongMaterials.Blue; // Синий цвет для новых элементов


        //MeshComponent meshComponent = new();
        //MeshBuilder meshBuilder = new MeshBuilder();
        //meshBuilder.AddSphere(new Vector3(0, 0, 0), 0.5f, 32, 16);
        //meshComponent.Geometry = meshBuilder.ToMeshGeometry3D();
        //meshComponent.Material = PhongMaterials.Blue;
        //pointEntity.AddComponent<MeshComponent>();
        return pointEntity;
    }
}
