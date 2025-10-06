using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.ECS.Components;
using SharpDX;

namespace OxyzWPF.Editor.Instrutions;

internal class AddSphere : IInstruction
{
    private readonly IMailer _mailer;
    private readonly IWorld _world;
    private readonly IInstructor _instructor;
    public AddSphere(IWorld world, IMailer mailer, IInstructor instructor)
    {
        _mailer = mailer;
        _world = world;
        _instructor = instructor;
    }

    public void OnStart(object args)
    {
        _mailer.Publish(EventEnum.GameStateChangeRequest, "Add");
        _mailer.Publish(EventEnum.InstructionStart, this);
        _instructor.ActiveInstruction = this;
    }

    public void Execute(object args)
    {
        var sphereEntity = _world.CreateEntity($"Sphere_{_world.EntityCount}");

        var transform = sphereEntity.AddComponent<TransformComponent>();
        var position = (Vector3)args;
        transform.Position = new Vector3(position.X, 0.5f, position.Z); // Поднимаем сферу над сеткой

        var mesh = sphereEntity.AddComponent<MeshComponent>();
        var mb = new MeshBuilder();
        mb.AddSphere(new Vector3(0, 0, 0), 1.0f, 32, 16);
        mesh.Geometry = mb.ToMeshGeometry3D();
        mesh.Material = PhongMaterials.Blue; // Синий цвет для новых элементов

        _mailer.Publish(EventEnum.TestEvent, $"Создана сфера в позиции ({position.X:F1}, {position.Z:F1})");
    }

    public void OnEnd()
    {
        _mailer.Publish(EventEnum.GameStateChangeRequest, "Navigation");
        _instructor.ActiveInstruction = null;
    }
}
