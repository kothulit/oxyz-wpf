using OxyzWPF.Contracts.ECS;

namespace OxyzWPF.ECS
{
    internal class RealWorld : World
    {
        public RealWorld(ISystemsSwitcher systemsStateMachine) : base(systemsStateMachine) { }
    }
}
