using OxyzWPF.Contracts.ECS;
using SharpDX;

namespace OxyzWPF.ECS.Components;

public class ContourComponent : OxyzWPF.Contracts.ECS.IComponent
{
    public string Name { get; } = nameof(ContourComponent);

    public List<Vector2> ContourPoints = new List<Vector2>();
}
