using System.Windows.Media.Media3D;
using System.Windows.Input;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using OxyzWPF.UI.Commands;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Game.States;
using System.Collections.ObjectModel;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Game.States;

namespace OxyzWPF.UI.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IMailer _mailer;
    private readonly IWorld? _world;
    private readonly IGameState _gameState;
    private int _testNumber = 0;
    private Vector3 _position = Vector3.Zero;
    private string _testText = "Test";
    private FPSCubeVM _fPSCubeVM = new FPSCubeVM();
    private Transform3D _cubeTransform = Transform3D.Identity;
    private bool _isAddMode = false;
    private string _statusText;
    private IInstruction _instruction;

    public Vector3 Position => _position;
    public ObservableCollection<ToolbarButtonViewModel> ToolbarButtons
    {
        get;
        private set;
    } = new ObservableCollection<ToolbarButtonViewModel>();
    public ICommand OnMouseDowmCommand { get; }
    //Текст для теста mailer
    public string TestText
    {
        get => _testText;
        set
        {
            _testText = value;
            OnPropertyChanged();
        }
    }
    public void OnTestEventInvoked(object arg)
    {
        _testNumber++;
        TestText = "Test " + _testNumber;
    }
    //Transform для вращающегося куба
    public Transform3D CubeTransform
    {
        get => _cubeTransform;
        set
        {
            _cubeTransform = value;
            OnPropertyChanged();
        }
    }
    public bool IsPanEnabled => !_isAddMode;
    public bool IsRotationEnabled => !_isAddMode;
    public string StatusText
    {
        get => _statusText;
        set
        {
            _statusText = value;
            OnPropertyChanged();
        }
    }
    public ICommand AddCubeCommand { get; }

    public MainViewModel(IWorld world, IMailer mailer)
    {
        _mailer = mailer;
        _world = world;
        _gameState = new StateNavigation();
        StatusText = _gameState.StateName;
        _mailer.Subscribe<object>(EventEnum.TestEvent, OnTestEventInvoked);
        _mailer.Subscribe<object>(EventEnum.GameStateChanged, OnStateChanged);
        _mailer.Subscribe<object>(EventEnum.InstructionStart, OnInstrutionStart);
        //OnMouseDowmCommand = new RelayCommand(OnMouseDowm);
    }

    public void Update(double deltaTime)
    {
        _fPSCubeVM.Update(deltaTime);
        CubeTransform = _fPSCubeVM.CubeTransform;
    }
    public void OnStateChanged(object args)
    {
        var gameState = (IGameState)args;
        StatusText = gameState.StateName;
    }
    public void OnMouseClick(Vector2 screenPoint, Viewport3DX viewport)
    {
        // Преобразуем экранные координаты в 3D координаты на плоскости Y=0
        IList<HitTestResult> hitResult = viewport.FindHits(screenPoint);
        if (hitResult != null && hitResult.Count > 0)
        {
            // Ищем пересечение с плоскостью Y=0
            var ray = viewport.UnProject(screenPoint);
            Plane plane = new Plane(new Vector3(0, 1, 0), 0); // Плоскость Y=0

            if (ray.Intersects(ref plane, out float distance))
            {
                var worldPoint = ray.Position + ray.Direction * distance;
            }
        }
        else
        {
            // Если не попали в объект, используем проекцию на плоскость
            var ray = viewport.UnProject(screenPoint);
            var plane = new Plane(new Vector3(0, 1, 0), 0);

            if (ray.Intersects(ref plane, out float distance))
            {
                _position = ray.Position + ray.Direction * distance;
            }
        }

        switch (_statusText)
        {
            case "Add":
                _instruction?.Execute(_position);
                break;
        }
    }

    public void InitialiseToolbarButtons(Dictionary<string, IInstruction> instructions)
    {
        foreach (var instruction in instructions)
        {
            ToolbarButtons.Add(new ToolbarButtonViewModel()
            {
                Content = instruction.Key.ToString(),
                Command = new RelayCommand(instruction.Value.OnStart)
            });
        }
    }
    public void OnButtonClick(object sender, EventArgs e)
    {

    }

    private void OnInstrutionStart(object instruction)
    {
        _instruction = (IInstruction)instruction;
    }
}
