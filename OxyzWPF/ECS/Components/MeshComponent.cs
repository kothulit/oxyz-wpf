using HelixToolkit.Wpf.SharpDX;

namespace OxyzWPF.ECS.Components;

/// <summary>
/// Компонент для 3D геометрии объекта
/// </summary>
public class MeshComponent : IComponent
{
    public MeshGeometry3D? Geometry { get; set; }
    public Material? Material { get; set; }
    public string MeshName { get; set; } = "";

    public MeshComponent()
    {
    }

    public MeshComponent(MeshGeometry3D geometry, Material? material = null, string meshName = "")
    {
        Geometry = geometry;
        Material = material;
        MeshName = meshName;
    }

    public override string ToString()
    {
        return $"Mesh({MeshName})";
    }
}