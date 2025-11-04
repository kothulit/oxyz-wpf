using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
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

    public CreateContour(IWorld world, IMailer mailer, IInstructor instructor) : base(world, mailer, instructor) { }

    public void OnStart(object args)
    {
        _mailer.Publish(EventEnum.GameStateChangeRequest, "Edit");
        _mailer.Publish(EventEnum.InstructionStart, this);
        _instructor.ActiveInstruction = this;

        _isCreating = true;

        //_entity = _world.CreateEntity();
        //_contour = _entity.AddComponent<ContourComponent>();

        _mailer.Publish(EventEnum.TestEvent, "Режим создания контура.");
    }

    public void Execute(object args)
    {
        //if (!_isCreating) return;
        //var position = (Vector3)args;
        //var point2D = new Vector2(position.X, position.Z);
        //_contour.ContourPoints.Add(point2D);

        Vector3 currentPoint = (Vector3)args;

        Factory.CreatePoint(_world, currentPoint);
        if (_isPreviosPointEnable) Factory.CreateLine(_world, currentPoint, _previosPoint);

        _previosPoint = currentPoint;
        _isPreviosPointEnable = true;

        //_mailer.Publish(EventEnum.TestEvent, $"Добавлена точка {_contour.ContourPoints.Count}: ({point2D.X:F2}, {point2D.Y:F2})");
        _mailer.Publish(EventEnum.TestEvent, $"Добавлена точка: ({((Vector3)args).X:F2}, {((Vector3)args).Y:F2})");
    }

    public void OnEnd(object args)
    {
        _mailer.Publish(EventEnum.GameStateChangeRequest, "Browse");
        _instructor.ActiveInstruction = null;
        _isPreviosPointEnable = false;
    }
}
