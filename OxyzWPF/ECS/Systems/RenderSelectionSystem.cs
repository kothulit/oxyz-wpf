using OxyzWPF.Contracts.ECS;

namespace OxyzWPF.ECS.Systems;

internal class RenderSelectionSystem : ISystem
{
    public string Name => typeof(RenderSelectionSystem).Name;
    public bool IsEnable { get; set; } = false;

    private readonly World _provisionalWorld;

    public RenderSelectionSystem(World provisionalWorld)
    {
        _provisionalWorld = provisionalWorld;
    }

    public void Update(double deltaTime)
    {
        _provisionalWorld.Clear();
    }
}
