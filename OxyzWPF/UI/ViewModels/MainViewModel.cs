using System.Windows.Media.Media3D;

namespace OxyzWPF.UI.ViewModels;

internal class MainViewModel : ViewModelBase
{
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

    private double _rotationAngle = 0.0;

    public void Update(double deltaTime)
    {
        _rotationAngle += deltaTime * 45; // Поворачиваем на 1 градусов в секунду

        // Создаем группу трансформаций для комбинирования поворотов
        var transformGroup = new Transform3DGroup();

        // Сначала перемещаем куб в нужную позицию
        transformGroup.Children.Add(new TranslateTransform3D(-5, 0, -5));

        // Поворот вокруг оси Y (вертикальная ось)
        transformGroup.Children.Add(new RotateTransform3D(
            new AxisAngleRotation3D(new Vector3D(0, 1, 0), _rotationAngle), new Point3D(-5, 0, -5)));

        // Поворот вокруг оси X (горизонтальная ось) - медленнее
        transformGroup.Children.Add(new RotateTransform3D(
            new AxisAngleRotation3D(new Vector3D(1, 0, 0), _rotationAngle), new Point3D(-5, 0, -5)));

        CubeTransform = transformGroup;
    }
}