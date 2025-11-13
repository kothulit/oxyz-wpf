using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.Contracts.Transponder;
using OxyzWPF.UI.Commands;
using SharpDX;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace OxyzWPF.UI.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
    private readonly IGameStateMachine _gameStateMachine;
    private readonly IInstructor _instructor;
    private readonly IInputTransponder _inputTransponder;
    public ObservableCollection<string> Messages { get; private set; } = new ObservableCollection<string>();

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

    private string _statusText = "Статус";
    public string StatusText
    {
        get => _statusText;
        set
        {
            _statusText = value;
            OnPropertyChanged();
            if (Messages.Count > 10)
            {
                Messages.RemoveAt(0);
                Messages.Add(_statusText);
            }
            else Messages.Add(_statusText);
        }
    }

    public MainViewModel(IMessenger messenger, IGameStateMachine gameStateMachine, IInstructor instructor, IInputTransponder inputTransponder)
    {
        _messenger = messenger;
        _gameStateMachine = gameStateMachine;
        _instructor = instructor;
        _inputTransponder = inputTransponder;

        StateName = _gameStateMachine.CurrentState.StateName;

        _messenger.Subscribe<GameStateEventArgs>(EventEnum.GameStateChanged.ToString(), OnStateChanged);
        _messenger.Subscribe<StatusEventArgs>(EventEnum.StatusChangedEvent.ToString(), TestEventHandler);
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

    public void OnStateChanged(object? _, GameStateEventArgs e)
    {
        StateName = e.CurrentState.StateName;
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
                    _messenger.Publish(EventEnum.HitToGeometryModel.ToString(), this, new GeometryEventArgs(hitObject));
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
            case "Edit":
                _instructor.ActiveInstruction.Execute(_position);
                break;
        }
    }

    public void OnKeyDown(object sender, KeyEventArgs e)
    {
        _messenger.Publish(EventEnum.KeyPress.ToString(), sender, e);
    }

    public void TestEventHandler(object sender, StatusEventArgs e)
    {
        StatusText = e.Message;
    }

    public void OnMouseMove(Viewport3DX viewPort, MouseEventArgs e)
    {
        _messenger.Publish(EventEnum.MouseMove.ToString(), viewPort, e);
    }
}
