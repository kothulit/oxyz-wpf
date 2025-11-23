using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.ECS;
using OxyzWPF.ECS.Components;
using OxyzWPF.Instruction.Instrutions;
using SharpDX;

namespace OxyzWPF.Editor.Instrutions;

public class AddSphere : BaseInstruction, IInstruction
{
    public string Name { get; } = nameof(AddSphere);
    public AddSphere(World world, IMessenger messenger, IInstructor instructor) : base(world, messenger, instructor) { }

    public void OnStart(object args)
    {
        _messenger.Publish(EventEnum.GameStateChangeRequest.ToString(), this, new GameStateChangeRequestEventArgsy(GameStateEnum.EntityAdding.ToString()));
        _instructor.ActiveInstruction = this;
    }

    public void Execute(object args)
    {
        var sphereEntity = _world.CreateEntity($"Sphere_{_world.EntityCount}");
        var selection = sphereEntity.AddComponent<SelectionComponent>();
        selection.IsSelected = false;

        var transform = sphereEntity.AddComponent<TransformComponent>();
        var position = (args as InstructionCallEventArgs).ScenePoint;
        transform.Position = new Vector3(position.X, 0.5f, position.Z); // Поднимаем сферу над сеткой

        var mesh = sphereEntity.AddComponent<MeshComponent>();
        var mb = new MeshBuilder();
        mb.AddSphere(new Vector3(0, 0, 0), 0.5f, 32, 16);
        mesh.Geometry = mb.ToMeshGeometry3D();
        mesh.Material = PhongMaterials.Blue; // Синий цвет для новых элементов

        _messenger.Publish(EventEnum.StatusChangedEvent.ToString(), this, new StatusEventArgs($"Создана сфера в позиции ({position.X:F1}, {position.Z:F1})"));
    }

    public void OnEnd(object args)
    {
        _messenger.Publish(EventEnum.GameStateChangeRequest.ToString(), this, new GameStateChangeRequestEventArgsy(GameStateEnum.Browsing.ToString()));
    }
}
