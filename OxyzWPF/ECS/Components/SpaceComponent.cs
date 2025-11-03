using SharpDX;

namespace OxyzWPF.ECS.Components;

public class SpaceComponent : OxyzWPF.Contracts.ECS.IComponent
{
    public string Name { get; } = nameof(SpaceComponent);
    public List<Vector2> Contour { get; set; }
    public float Height { get; set; }
}
