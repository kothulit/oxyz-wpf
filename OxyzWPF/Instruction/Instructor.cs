using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.ECS;
using OxyzWPF.Editor.Instrutions;
using OxyzWPF.Instruction.Instrutions;

namespace OxyzWPF.Editor;

public class Instructor : IInstructor
{
    private readonly IMessenger _messenger;
    private readonly ProjectWorld _projectWorld;
    private readonly SupportWorld _supportWorld;
    private Dictionary<string, IInstruction> _instructions = new Dictionary<string, IInstruction>();
    public IInstruction? ActiveInstruction { get; set; }
    public Dictionary<string, IInstruction> Instructions => _instructions;

    public Instructor(ProjectWorld projectWorld , SupportWorld supportWorld, IMessenger messenger)
    {
        _messenger = messenger;
        _projectWorld = projectWorld;
        _supportWorld = supportWorld;

        _instructions.Add("AddCube", new AddCube(_supportWorld, _messenger, this));
        _instructions.Add("AddSphere", new AddSphere(_supportWorld, _messenger, this));
        _instructions.Add("CreateContour", new CreateContour(_supportWorld, _messenger, this));

        _messenger.Subscribe<InstructionEventArgs>(EventEnum.InstructionStart.ToString(), OnInstructionStart);
        _messenger.Subscribe<InstructionEventArgs>(EventEnum.InstructionCanseled.ToString(), OnInstructionCanseled);
    }

    private void OnInstructionStart(object? _, InstructionEventArgs e)
    {
        ActiveInstruction = e.Instruction;
    }

    private void OnInstructionCanseled(object? _, InstructionEventArgs e)
    {
        ActiveInstruction?.OnEnd(nameof(ActiveInstruction));
        ActiveInstruction = null;
    }
}
