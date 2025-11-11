using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.ECS;

namespace OxyzWPF.Instruction.Instrutions;

public class BaseInstruction
{
    private protected readonly IMessenger _messenger;
    private protected readonly World _world;
    private protected readonly IInstructor _instructor;
    public BaseInstruction(World world, IMessenger messenger, IInstructor instructor)
    {
        _messenger = messenger;
        _world = world;
        _instructor = instructor;
    }
}
