using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.ECS;
using OxyzWPF.ECS.Components;
using SharpDX;

namespace OxyzWPF.Instruction.Instrutions;

public class SelectionInstruction : BaseInstruction, IInstruction
{
    public string Name => "Выбор";
    
    public SelectionInstruction(World world, IMessenger messenger, IInstructor instructor) : base(world, messenger, instructor) { }

    public void OnStart(object _)
    {
        _instructor.ActiveInstruction = this;
    }
    public void Execute(object e)
    {
        var selectedIds = (e as InstructionCallEventArgs).SelectedElementsIds;
        
        _messenger.Publish(EventEnum.SelectionChange.ToString(), this, new SelectionChangeEventArgs(selectedIds));
    }

    public void OnEnd(object _)
    {
        _messenger.Publish(EventEnum.GameStateChangeRequest.ToString(), this, new GameStateChangeRequestEventArgsy("Browse"));
    }

}
