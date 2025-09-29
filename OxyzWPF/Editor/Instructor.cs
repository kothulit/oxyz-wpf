using Microsoft.Extensions.DependencyInjection;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Editor;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Editor.Instrutions;
using OxyzWPF.Mailing;
using OxyzWPF.UI.ViewModels;

namespace OxyzWPF.Editor;

public class Instructor : IInstructor
{
    private readonly IMailer _mailer;
    private readonly IWorld _world;
    private readonly MainViewModel _mainViewModel;
    private Dictionary<string, IInstruction> _instructions = new Dictionary<string, IInstruction>();
    public Dictionary<string, IInstruction> Instructions => _instructions;

    public Instructor(IWorld world, MainViewModel mainViewModel, IMailer mailer)
    {
        _mailer = mailer;
        _world = world;
        _mainViewModel = mainViewModel;
        _instructions.Add("AddCube", new AddCube(_world, _mainViewModel, _mailer));
        _instructions.Add("AddSphere", new AddSphere(_world, _mainViewModel));
    }
}
