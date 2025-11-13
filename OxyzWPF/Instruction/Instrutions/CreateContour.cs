using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.ECS;
using OxyzWPF.ECS.Components;
using SharpDX;

namespace OxyzWPF.Instruction.Instrutions;

public class CreateContour : BaseInstruction, IInstruction
{
    public string Name { get; } = nameof(CreateContour);
    private bool _isPreviosPointEnable = false;
    private Vector3 _previosPoint;

    public CreateContour(World world, IMessenger messenger, IInstructor instructor) : base(world, messenger, instructor) { }

    public void OnStart(object args)
    {
        _messenger.Publish(EventEnum.GameStateChangeRequest.ToString(), this, new GameStateChangeRequestEventArgsy("Edit"));
        _messenger.Publish(EventEnum.InstructionStart.ToString(), this, new InstructionEventArgs(this));
        _instructor.ActiveInstruction = this;

        _messenger.Publish(EventEnum.StatusChangedEvent.ToString(), this, new StatusEventArgs("Режим создания контура."));
    }

    public void Execute(object args)
    {
        Vector3 currentPoint = (Vector3)args;

        Factory.CreatePoint(_world, currentPoint);
        if (_isPreviosPointEnable) Factory.CreateLine(_world, currentPoint, _previosPoint);

        _previosPoint = currentPoint;
        _isPreviosPointEnable = true;

        _messenger.Publish(EventEnum.StatusChangedEvent.ToString(), this, new StatusEventArgs($"Добавлена точка: ({((Vector3)args).X:F2}, {((Vector3)args).Y:F2})"));
    }

    public void OnEnd(object args)
    {
        _messenger.Publish(EventEnum.GameStateChangeRequest.ToString(), this, new GameStateChangeRequestEventArgsy("Browse"));
        _instructor.ActiveInstruction = null;
        _isPreviosPointEnable = false;
    }
}
