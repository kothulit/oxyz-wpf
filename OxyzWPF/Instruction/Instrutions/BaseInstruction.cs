using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF.Instruction.Instrutions;

public class BaseInstruction
{
    private protected readonly IMailer _mailer;
    private protected readonly IWorld _world;
    private protected readonly IInstructor _instructor;
    public BaseInstruction(IWorld world, IMailer mailer, IInstructor instructor)
    {
        _mailer = mailer;
        _world = world;
        _instructor = instructor;
    }
}
