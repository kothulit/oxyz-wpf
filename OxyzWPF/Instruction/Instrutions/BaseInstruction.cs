using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF.Instruction.Instrutions;

public class BaseInstruction
{
    private protected readonly IMessenger _messenger;
    private protected readonly IWorld _world;
    private protected readonly IInstructor _instructor;
    public BaseInstruction(IWorld world, IMessenger messenger, IInstructor instructor)
    {
        _messenger = messenger;
        _world = world;
        _instructor = instructor;
    }
}
