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
    private Dictionary<int, IInstruction> _instructions = new Dictionary<int, IInstruction>();
    private readonly IInstruction _defaultInstruction;
    public IInstruction? ActiveInstruction { get; set; }
    public Dictionary<int, IInstruction> Instructions => _instructions;

    public Instructor(ProjectWorld projectWorld , SupportWorld supportWorld, IMessenger messenger)
    {
        _messenger = messenger;
        _projectWorld = projectWorld;
        _supportWorld = supportWorld;
        _defaultInstruction = new SelectionInstruction(projectWorld, messenger, this);
        ActiveInstruction = _defaultInstruction;

        _instructions.Add(1, new AddCube(_supportWorld, _messenger, this));
        _instructions.Add(2, new AddSphere(_supportWorld, _messenger, this));
        _instructions.Add(3, new CreateContour(_supportWorld, _messenger, this));

        _messenger.Subscribe<InstructionEventArgs>(EventEnum.InstructionStart.ToString(), OnInstructionStart);
        _messenger.Subscribe<InstructionCallEventArgs>(EventEnum.InstructionExecuteCall.ToString(), OnActiveInstructionCall);
        _messenger.Subscribe<EventArgs>(EventEnum.Сancellation.ToString(), OnInstructionCanseled);
    }

    private void OnInstructionStart(object? _, InstructionEventArgs e)
    {
        ActiveInstruction = _instructions[e.NumberOfInstruction];
        _messenger.Publish(EventEnum.GameStateChangeRequest.ToString(), this, new GameStateChangeRequestEventArgsy("Add"));
    }
    private void OnActiveInstructionCall(object _, InstructionCallEventArgs e)
    {
        ActiveInstruction.Execute(e);
    }
    private void OnInstructionCanseled(object? _, EventArgs e)
    {
        ActiveInstruction?.OnEnd(nameof(ActiveInstruction));
        ActiveInstruction = _defaultInstruction;
    }

}
