using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.ECS.Components;
using SharpDX;

namespace OxyzWPF.Editor.Instrutions;

internal class AddCube : IInstruction
{
    private readonly IMailer _mailer;
    private readonly IWorld _world;
    public AddCube(IWorld world , IMailer mailer)
    {
        _mailer = mailer;
        _world = world;
    }

    public void OnStart(object args)
    {
        _mailer.Publish(EventEnum.GameStateChangeRequest, "Add");
        _mailer.Publish(EventEnum.InstructionStart, this);
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

    public void OnEnd()
    {
        _mailer.Publish(EventEnum.GameStateChangeRequest, "Navigation");
    }
}
