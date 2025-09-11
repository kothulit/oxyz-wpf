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

    private double _xCoordinate = 0.0;

    public void Update(double deltaTime)
    {
        _xCoordinate += deltaTime; // Увеличиваем координату X со временем
        CubeTransform = new TranslateTransform3D(_xCoordinate, 0, 0);
    }
}