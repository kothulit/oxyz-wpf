using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.ECS;
using OxyzWPF.Editor.Instrutions;
using OxyzWPF.Instruction.Instrutions;

namespace OxyzWPF.Editor;

public class Instructor : IInstructor
{
    private readonly IMailer _mailer;
    private readonly IWorld _world;
    private readonly ProvisionalWorld _provisionalWorld;
    private Dictionary<string, IInstruction> _instructions = new Dictionary<string, IInstruction>();
    public IInstruction? ActiveInstruction { get; set; }
    public Dictionary<string, IInstruction> Instructions => _instructions;

    public Instructor(IWorld realWorld , ProvisionalWorld provisionalWorld, IMailer mailer)
    {
        _mailer = mailer;
        _world = realWorld;
        _provisionalWorld = provisionalWorld;

        _instructions.Add("AddCube", new AddCube(_world, _mailer, this));
        _instructions.Add("AddSphere", new AddSphere(_provisionalWorld, _mailer, this));
        _instructions.Add("CreateContour", new CreateContour(_provisionalWorld, _mailer, this));

        _mailer.Subscribe<object>(EventEnum.InstructionStart, OnInstructionStart);
        _mailer.Subscribe<object>(EventEnum.InstructionCanseled, OnInstructionCanseled);
    }

    private void OnInstructionStart(object instruction)
    {
        ActiveInstruction = (IInstruction)instruction;
    }

    private void OnInstructionCanseled(object args)
    {
        ActiveInstruction?.OnEnd(nameof(ActiveInstruction));
        ActiveInstruction = null;
    }
}
