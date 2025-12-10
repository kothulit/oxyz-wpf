using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Game;
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
    private List<Vector3> _points = new List<Vector3>();
    private Vector3 _previosPoint;
    private Entity _entity;

    public CreateContour(World world, IMessenger messenger, IInstructor instructor) : base(world, messenger, instructor) { }

    public void OnStart(object args)
    {
        _messenger.Publish(EventEnum.GameStateChangeRequest.ToString(), this, new GameStateChangeRequestEventArgsy(GameStateEnum.EntityAdding.ToString()));
        _instructor.ActiveInstruction = this;

        _messenger.Publish(EventEnum.StatusChangedEvent.ToString(), this, new StatusEventArgs("Режим создания контура."));
    }

    public void Execute(object args)
    {
        if (_entity == null)
        {
            _entity = _world.CreateEntity("Contour");
            _entity.AddComponent(new MeshComponent());
            _entity.AddComponent(new TransformComponent());
        }
        Vector3 currentPoint = (args as InstructionCallEventArgs).ScenePoint;
        _points.Add(currentPoint);

        Factory.CreatePoint(_world, currentPoint);
        if (_isPreviosPointEnable) Factory.CreateLine(_world, currentPoint, _previosPoint);

        if (_points.Count > 2)
        {
            _entity.GetComponent<MeshComponent>().Geometry = Factory.CreatePolygon(_points);
        }

        _previosPoint = currentPoint;
        _isPreviosPointEnable = true;

        _messenger.Publish(EventEnum.StatusChangedEvent.ToString(), this, new StatusEventArgs($"Добавлена точка: ({currentPoint.X:F2}, {currentPoint.Y:F2})"));
    }

    public void OnEnd(object args)
    {
        _messenger.Publish(EventEnum.GameStateChangeRequest.ToString(), this, new GameStateChangeRequestEventArgsy(GameStateEnum.Browsing.ToString()));
        _isPreviosPointEnable = false;
    }
}
