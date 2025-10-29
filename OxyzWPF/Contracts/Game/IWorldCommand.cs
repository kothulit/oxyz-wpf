using OxyzWPF.Contracts.ECS;

namespace OxyzWPF.Contracts.Game;

internal interface IWorldCommand
{
    string Name { get; }
    void Do(IWorld world);
    void Undo(IWorld world);
}
