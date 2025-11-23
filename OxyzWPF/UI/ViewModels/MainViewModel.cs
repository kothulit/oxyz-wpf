using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.Contracts.Transponder;
using OxyzWPF.Transponder;
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
    private readonly IKeyInputTransponder _keyInputTransponder;
    private readonly IMouseInputTransponder _mouseInputTransponder;
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

    private Vector2 _screenPoint = Vector2.Zero;
    public Vector2 ScreenPoint
    {
        get { return _screenPoint; }
        set { _screenPoint = value; }
    }

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

    public MainViewModel(IMessenger messenger,
        IGameStateMachine gameStateMachine,
        IInstructor instructor,
        IKeyInputTransponder keyInputTransponder,
        IMouseInputTransponder mouseInputTransponder)
    {
        _messenger = messenger;
        _gameStateMachine = gameStateMachine;
        _instructor = instructor;
        _keyInputTransponder = keyInputTransponder;
        _mouseInputTransponder = mouseInputTransponder;

        StateName = _gameStateMachine.CurrentState.StateName;

        _messenger.Subscribe<GameStateEventArgs>(EventEnum.GameStateChanged.ToString(), OnStateChanged);
        _messenger.Subscribe<StatusEventArgs>(EventEnum.StatusChangedEvent.ToString(), StatusEventHandler);
        _messenger.Subscribe<GameStateEventArgs>(EventEnum.GameStateChanged.ToString(), OnStateChanged);
    }

    public void InitialiseToolbarButtons(Dictionary<int, IInstruction> instructions)
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

    private void OnStateChanged(object? _, GameStateEventArgs e)
    {
        StateName = e.CurrentState.StateName;
    }

    public void OnMouseClick(Vector2 screenPoint, Viewport3DX viewport)
    {
        //Пока определяем точку только на нулевой плоскости
        _position = GetNullPlaneIntersection(screenPoint, viewport);

        //Ищем пересечение с объектами
        IList<HitTestResult> hitResult = viewport.FindHits(screenPoint);
        if (hitResult != null && hitResult.Count > 0)
        {
            var hitElementIds = new List<int>();
            // Если кликнули по объекту, обрабатываем выделение
            foreach (var hit in hitResult)
            {
                var modelHit = hit.ModelHit as MeshGeometryModel3D;
                if (modelHit.Tag != null)
                {
                    hitElementIds.Add((int)modelHit.Tag);
                }
            }
            _messenger.Publish(EventEnum.MouseDown.ToString(), this, new OxyzMouseEventArgs(screenPoint, _position, hitElementIds));
        }
        else
        {
            _messenger.Publish(EventEnum.MouseDown.ToString(), this, new OxyzMouseEventArgs(screenPoint, _position));
        }
    }

    public void OnKeyDown(object sender, KeyEventArgs e)
    {
        _messenger.Publish(EventEnum.KeyPress.ToString(), sender, e);
    }

    public void StatusEventHandler(object sender, StatusEventArgs e)
    {
        StatusText = e.Message;
    }

    public void OnMouseMove(Viewport3DX viewPort, System.Windows.Input.MouseEventArgs e)
    {
        _messenger.Publish(EventEnum.MouseMove.ToString(), viewPort, e);
    }

    private Vector3 GetNullPlaneIntersection(Vector2 screenPoint, Viewport3DX viewport)
    {
        var position = new Vector3();
        var ray = viewport.UnProject(screenPoint);
        var plane = new Plane(new Vector3(0, 1, 0), 0);

        if (ray.Intersects(ref plane, out float distance))
        {
            position = ray.Position + ray.Direction * distance;
        }

        return position;
    }
}
