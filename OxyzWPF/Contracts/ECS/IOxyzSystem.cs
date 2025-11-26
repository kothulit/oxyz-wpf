using OxyzWPF.ECS.Systems;

namespace OxyzWPF.Contracts.ECS;

public interface IOxyzSystem
{
    public string Name { get; }
    public bool IsEnable { get; set; }
    void Update(double deltaTime);
}