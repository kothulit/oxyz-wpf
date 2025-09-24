using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Editor;
using OxyzWPF.Editor.Instrutions;
using OxyzWPF.UI.ViewModels;

namespace OxyzWPF.Editor;

public class Instructor : IInstructor
{
    private readonly IWorld _world;
    private readonly MainViewModel _mainViewModel;
    private Dictionary<string, IInstruction> _instructions = new Dictionary<string, IInstruction>();
    public Dictionary<string, IInstruction> Instructions => _instructions;

    public Instructor(IWorld world, MainViewModel mainViewModel)
    {
        _world = world;
        _mainViewModel = mainViewModel;
        _instructions.Add("AddCube", new AddCube(_world, _mainViewModel));
        _instructions.Add("AddSphere", new AddSphere(_world, _mainViewModel));
    }
}
