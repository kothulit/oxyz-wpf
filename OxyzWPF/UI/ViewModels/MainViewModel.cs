using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Transponder;
using OxyzWPF.Game.States;
using OxyzWPF.UI.Commands;
using SharpDX;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Media3D;

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

    public void Update(double deltaTime)
    {
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
            // Если кликнули по объекту, обрабатываем выделение
            var hitObject = hitResult[0].ModelHit as MeshGeometryModel3D;
            if (hitObject != null)
            {
                if (_gameStateMachine.CurrentState.StateName.Equals("Browse"))
                {
                    _mailer.Publish(EventEnum.HitToGeometryModel, hitObject);
                }
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

    public void OnInstrutionStart(object args)
    {
        
    }

    public void OnKeyDown(object args)
    {
        _inputTransponder.OnKeyDown(args);
    }
}
