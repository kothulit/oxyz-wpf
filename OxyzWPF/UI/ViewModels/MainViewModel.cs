using System.Windows.Media.Media3D;
using System.Windows.Input;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using OxyzWPF.ECS;
using OxyzWPF.ECS.Components;
using OxyzWPF.UI.Commands;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Game.States;

namespace OxyzWPF.UI.ViewModels;

public class MainViewModel : ViewModelBase
{
    private IWorld? _world;
    private IEditorState _editorState;

    private Vector3 _position = Vector3.Zero;
    public Vector3 Position => _position;

    private FPSCubeVM _fPSCubeVM = new FPSCubeVM();
    private Transform3D _cubeTransform = Transform3D.Identity;
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


    private bool _isAddMode = false;
    private string _statusText = "Режим навигации";
    

    public bool IsAddMode
    {
        get => _isAddMode;
        set
        {
            _isAddMode = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsPanEnabled));
            OnPropertyChanged(nameof(IsRotationEnabled));
            StatusText = _isAddMode ? "Режим добавления кубов - кликните на сетку" : "Режим навигации";
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

    public MainViewModel(IWorld world, IEditorState editorState)
    {
        _world = world;
        AddCubeCommand = new RelayCommand(() => IsAddMode = !IsAddMode);
    }

    public void Update(double deltaTime)
    {
        _fPSCubeVM.Update(deltaTime);
        CubeTransform = _fPSCubeVM.CubeTransform;
    }

    public void OnMouseClick(Vector2 screenPoint, Viewport3DX viewport)
    {
        if (!_isAddMode || _world == null) return;

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
    }
}
