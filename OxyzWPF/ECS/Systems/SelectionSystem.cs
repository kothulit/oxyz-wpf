namespace OxyzWPF.ECS.Systems;

public class SelectionSystem
{
    public string Name => typeof(SelectionSystem).Name;
    public bool IsEnable { get; set; } = false;
}
