using System.Windows.Media.Media3D;

namespace OxyzWPF.UI.ViewModels;

internal class MainViewModel : ViewModelBase
{
    private Point3D _cubePosition = new Point3D(0, 0, 0);
    public Point3D CubePosition
    {
        get => _cubePosition;
        set
        {
            _cubePosition = value;
            OnPropertyChanged();
        }
    }

    public void Update(double deltaTime)
    {
        // Перемещаем куб по оси X со скоростью 1 единица в секунду
        CubePosition = new Point3D(CubePosition.X + deltaTime, CubePosition.Y, CubePosition.Z);
        //CubePosition = Point3D.Add(_cubePosition, new Vector3D((float)deltaTime, 0, 0));
    }
}
