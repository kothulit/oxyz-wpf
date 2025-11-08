using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.ECS.Components;
using SharpDX;

namespace OxyzWPF.ECS.Systems;

public class CreateContourModel : ISystem
{
    public bool IsOn { get; set; } = false;

    private readonly IWorld _world;
    private readonly IMessenger _messenger;

    private MeshBuilder meshBuilder = new MeshBuilder();

    public CreateContourModel(IWorld world, IMessenger messenger)
    {
        _world = world;
        _messenger = messenger;
    }

    public void Update(double deltaTime)
    {
        var entitiesToMesh = _world.GetEntitiesWithComponents<ContourComponent>();
        foreach (var entity in entitiesToMesh)
        {
            var contourComponent = entity.GetComponent<ContourComponent>();
            var meshComponent = entity.GetComponent<MeshComponent>();
            var TransformComponent = entity.GetComponent<TransformComponent>();

            if (meshComponent is null) meshComponent = entity.AddComponent<MeshComponent>();
            if (TransformComponent is null) TransformComponent = entity.AddComponent<TransformComponent>();

            meshComponent.Material = PhongMaterials.Red;
            var points3D = contourComponent?.ContourPoints.Select(p => new Vector3(p.X, 0, p.Y)).ToList();
            if (points3D?.Count > 2) meshBuilder.AddPolygon(points3D);
            meshComponent.Geometry = meshBuilder.ToMeshGeometry3D();
            meshBuilder = new MeshBuilder();
        }
    }
}
