using OxyzWPF.Contracts.ECS;

namespace OxyzWPF.ECS.Systems;

public class SelectionSystem : ISystem
{
    public string Name => typeof(SelectionSystem).Name;
    public bool IsEnable { get; set; } = true;

    public void Update(double deltaTime)
    {
        throw new NotImplementedException();
    }
}
