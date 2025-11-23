using OxyzWPF.Contracts.ECS;

namespace OxyzWPF.ECS.Components;

public class SelectionComponent : IOxyzComponent
{
    public string Name { get; } = nameof(SelectionComponent);
    public bool IsSelected { get; set; } = false;
}
