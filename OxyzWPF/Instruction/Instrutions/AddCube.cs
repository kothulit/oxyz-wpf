using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.ECS.Components;
using OxyzWPF.UI.ViewModels;
using SharpDX;

namespace OxyzWPF.Editor.Instrutions;

internal class AddCube : IInstruction
{
    private readonly IMailer _mailer;
    private readonly IWorld _world;
    private readonly MainViewModel _mainViewModel;
    public AddCube(IWorld world, MainViewModel mainViewModel, IMailer mailer)
    {
        _mailer = mailer;
        _world = world;
        _mainViewModel = mainViewModel;
    }

    public void Execute(object args)
    {
        var cubeEntity = _world.CreateEntity($"Cube_{_world.EntityCount}");

        var transform = cubeEntity.AddComponent<TransformComponent>();
        var position = _mainViewModel.Position;
        transform.Position = new Vector3(position.X, 0.5f, position.Z); // Поднимаем куб над сеткой

        var mesh = cubeEntity.AddComponent<MeshComponent>();
        var mb = new MeshBuilder();
        mb.AddBox(new Vector3(0, 0, 0), 1, 1, 1);
        mesh.Geometry = mb.ToMeshGeometry3D();
        mesh.Material = PhongMaterials.Blue; // Синий цвет для новых кубов

        _mainViewModel.StatusText = $"Создан куб в позиции ({position.X:F1}, {position.Z:F1})";

        _mailer.Publish("TestEvent", "Test");
    }
}
