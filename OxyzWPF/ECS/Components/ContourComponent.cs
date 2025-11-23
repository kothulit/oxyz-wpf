using OxyzWPF.Contracts.ECS;
using SharpDX;

namespace OxyzWPF.ECS.Components;

public class ContourComponent : OxyzWPF.Contracts.ECS.IOxyzComponent
{
    public string Name { get; } = nameof(ContourComponent);

    public List<Vector2> ContourPoints = new List<Vector2>();
}
