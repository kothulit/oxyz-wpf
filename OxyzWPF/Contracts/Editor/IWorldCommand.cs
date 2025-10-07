using OxyzWPF.Contracts.ECS;

namespace OxyzWPF.Contracts.Editor;

internal interface IWorldCommand
{
    string Name { get; }
    void Do(IWorld world);
    void Undo(IWorld world);
}
