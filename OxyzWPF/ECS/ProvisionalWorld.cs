using OxyzWPF.Contracts.ECS;

namespace OxyzWPF.ECS;

public class ProvisionalWorld : World
{
    public ProvisionalWorld(ISystemsSwitcher systemsStateMachine) : base(systemsStateMachine) { }
}
