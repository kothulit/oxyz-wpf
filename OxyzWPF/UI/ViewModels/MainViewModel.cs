using System.Windows.Media.Media3D;
using System.Windows.Input;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using OxyzWPF.ECS;
using OxyzWPF.ECS.Components;

namespace OxyzWPF.UI.ViewModels;

public class MainViewModel : ViewModelBase
{
    private FPSCubeVM _fPSCubeVM = new FPSCubeVM();

    private Transform3D _cubeTransform = Transform3D.Identity;
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
    private World? _world;

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

    public MainViewModel()
    {
        AddCubeCommand = new RelayCommand(() => IsAddMode = !IsAddMode);
    }

    public void SetWorld(World world)
    {
        _world = world;
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
                CreateCubeAtPosition(worldPoint);
            }
        }
        else
        {
            // Если не попали в объект, используем проекцию на плоскость
            var ray = viewport.UnProject(screenPoint);
            var plane = new Plane(new Vector3(0, 1, 0), 0);

            if (ray.Intersects(ref plane, out float distance))
            {
                var worldPoint = ray.Position + ray.Direction * distance;
                CreateCubeAtPosition(worldPoint);
            }
        }
    }

    private void CreateCubeAtPosition(Vector3 position)
    {
        if (_world == null) return;

        // Создаем новую сущность куба
        var cubeEntity = _world.CreateEntity($"Cube_{_world.EntityCount}");

        // Добавляем компонент трансформации
        var transform = cubeEntity.AddComponent<TransformComponent>();
        transform.Position = new Vector3(position.X, 0.5f, position.Z); // Поднимаем куб над сеткой

        // Добавляем компонент меша
        var mesh = cubeEntity.AddComponent<MeshComponent>();
        var mb = new MeshBuilder();
        mb.AddBox(new Vector3(0, 0, 0), 1, 1, 1);
        mesh.Geometry = mb.ToMeshGeometry3D();
        mesh.Material = PhongMaterials.Blue; // Синий цвет для новых кубов

        // Добавляем компонент имени
        cubeEntity.AddComponent<NameComponent>(new NameComponent($"Cube_{_world.EntityCount}"));

        StatusText = $"Создан куб в позиции ({position.X:F1}, {position.Z:F1})";
    }

}

// Простая реализация RelayCommand
public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool>? _canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke() ?? true;
    }

    public void Execute(object? parameter)
    {
        _execute();
    }
}