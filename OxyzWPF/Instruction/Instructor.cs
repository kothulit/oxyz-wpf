using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Editor.Instrutions;

namespace OxyzWPF.Editor;

public class Instructor : IInstructor
{
    private readonly IMailer _mailer;
    private readonly IWorld _world;
    private Dictionary<string, IInstruction> _instructions = new Dictionary<string, IInstruction>();
    public IInstruction ActiveInstruction { get; private set; }
    public Dictionary<string, IInstruction> Instructions => _instructions;

    public Instructor(IWorld world , IMailer mailer)
    {
        _mailer = mailer;
        _world = world;
        _instructions.Add("AddCube", new AddCube(_world, _mailer));
        _instructions.Add("AddSphere", new AddSphere(_world, _mailer));
        _mailer.Subscribe<object>(EventEnum.InstructionStart, OnInstructionStart);
    }

    private void OnInstructionStart(object instruction)
    {
        ActiveInstruction = (IInstruction)instruction;
    }
}
