using System.Windows.Media.Media3D;
using System.Windows.Input;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using OxyzWPF.UI.Commands;
using System.Collections.ObjectModel;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Transponder;
using OxyzWPF.Contracts.Game;

namespace OxyzWPF.UI.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IMailer _mailer;
    private readonly IGameStateMachine _gameStateMachine;
    private readonly IInstructor _instructor;
    private readonly IInputTransponder _inputTransponder;

    private string _stateName;
    public string StateName
    {
        get { return _stateName; }
        set
        {
            if (_stateName != value)
            {
                _stateName = value;
                OnPropertyChanged();
            }
        }
    }

    private Vector3 _position = Vector3.Zero;
    public Vector3 Position => _position;

    private FPSCubeVM _fPSCubeVM = new FPSCubeVM();
    private Transform3D _cubeTransform = Transform3D.Identity;

    private double _fps;
    public double FPS
    {
        get => _fps;
        set
        {
            _fps = value;
            OnPropertyChanged();
        }
    }
    public ObservableCollection<ToolbarButtonViewModel> ToolbarButtons
    {
        get;
        private set;
    } = new ObservableCollection<ToolbarButtonViewModel>();
    public ICommand OnMouseDowmCommand { get; }

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

    private string _statusText;
    public string StatusText
    {
        get => _statusText;
        set
        {
            _statusText = value;
            OnPropertyChanged();
        }
    }

    public MainViewModel(IMailer mailer, IGameStateMachine gameStateMachine, IInstructor instructor, IInputTransponder inputTransponder)
    {
        _mailer = mailer;
        _gameStateMachine = gameStateMachine;
        _instructor = instructor;
        _inputTransponder = inputTransponder;

        StatusText = _gameStateMachine.CurrentState.StateName;

        _mailer.Subscribe<object>(EventEnum.GameStateChanged, OnStateChanged);
        _mailer.Subscribe<object>(EventEnum.InstructionStart, OnInstrutionStart);
        _inputTransponder = inputTransponder;
    }

    public void Update(double deltaTime)
    {
        _fPSCubeVM.Update(deltaTime);
        CubeTransform = _fPSCubeVM.CubeTransform;
    }

    public void OnStateChanged(object args)
    {
        StateName = _gameStateMachine.CurrentState.StateName;
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

        switch (_stateName)
        {
            case "Add":
                _instructor.ActiveInstruction.Execute(_position);
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

    public void OnInstrutionStart(object args)
    {
        StateName = _gameStateMachine.CurrentState.StateName;
    }

    public void OnKeyDown(object args)
    {
        _inputTransponder.OnKeyDown(args);
    }

    
}
