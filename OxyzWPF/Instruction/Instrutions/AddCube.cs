using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.ECS.Components;
using OxyzWPF.Instruction.Instrutions;
using SharpDX;

namespace OxyzWPF.Editor.Instrutions;

public class AddCube : BaseInstruction, IInstruction
{
    public string Name { get; } = nameof(AddCube);
    public AddCube(IWorld world, IMailer mailer, IInstructor instructor) : base(world, mailer, instructor) { }

    public void OnStart(object args)
    {
        _mailer.Publish(EventEnum.GameStateChangeRequest, "Add");
        _mailer.Publish(EventEnum.InstructionStart, this);
        _instructor.ActiveInstruction = this;
    }

    public void Execute(object args)
    {
        var cubeEntity = _world.CreateEntity($"Cube_{_world.EntityCount}");

        var transform = cubeEntity.AddComponent<TransformComponent>();
        var position = (Vector3)args;
        transform.Position = new Vector3(position.X, 0.5f, position.Z); // Поднимаем куб над сеткой

        var mesh = cubeEntity.AddComponent<MeshComponent>();
        var mb = new MeshBuilder();
        mb.AddBox(new Vector3(0, 0, 0), 1, 1, 1);
        mesh.Geometry = mb.ToMeshGeometry3D();
        mesh.Material = PhongMaterials.Blue; // Синий цвет для новых кубов

        _mailer.Publish(EventEnum.TestEvent, $"Создан куб в позиции ({position.X:F1}, {position.Z:F1})");
    }

    public void OnEnd(object args)
    {
        _mailer.Publish(EventEnum.GameStateChangeRequest, "Browse");
        _instructor.ActiveInstruction = null;
    }
}
