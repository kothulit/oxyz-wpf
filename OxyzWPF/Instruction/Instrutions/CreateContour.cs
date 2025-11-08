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
    private bool _isCreating = false;
    private Entity _entity;
    private ContourComponent _contour;
    private bool _isPreviosPointEnable = false;
    private Vector3 _previosPoint;

    public CreateContour(IWorld world, IMessenger messenger, IInstructor instructor) : base(world, messenger, instructor) { }

    public void OnStart(object args)
    {
        _messenger.Publish(EventEnum.GameStateChangeRequest.ToString(), this, new GameStateChangeRequestEventArgsy("Edit"));
        _messenger.Publish(EventEnum.InstructionStart.ToString(), this, new InstructionEventArgs(this));
        _instructor.ActiveInstruction = this;

        _isCreating = true;

        _messenger.Publish(EventEnum.TestEvent.ToString(), this, new TestEventArgs("Режим создания контура."));
    }

    public void Execute(object args)
    {
        Vector3 currentPoint = (Vector3)args;

        Factory.CreatePoint(_world, currentPoint);
        if (_isPreviosPointEnable) Factory.CreateLine(_world, currentPoint, _previosPoint);

        _previosPoint = currentPoint;
        _isPreviosPointEnable = true;

        _messenger.Publish(EventEnum.TestEvent.ToString(), this, new TestEventArgs($"Добавлена точка: ({((Vector3)args).X:F2}, {((Vector3)args).Y:F2})"));
    }

    public void OnEnd(object args)
    {
        _messenger.Publish(EventEnum.GameStateChangeRequest.ToString(), this, new GameStateChangeRequestEventArgsy("Browse"));
        _instructor.ActiveInstruction = null;
        _isPreviosPointEnable = false;
    }
}
