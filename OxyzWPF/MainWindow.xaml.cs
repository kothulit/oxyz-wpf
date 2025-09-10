using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using OxyzWPF.UI.ViewModels;
using OxyzWPF.Game;

namespace OxyzWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        private readonly MeshGeometryModel3D _cubeModel;
        private GameLoop _gameLoop;
        public MainWindow()
        {
            InitializeComponent();

            // ViewModel
            _viewModel = new MainViewModel();
            DataContext = _viewModel;

            // EffectsManager & Camera (можно также вынести в VM)
            view1.EffectsManager = new DefaultEffectsManager();
            view1.Camera = new HelixToolkit.Wpf.SharpDX.PerspectiveCamera
            {
                Position = new Point3D(5, 5, 5),
                LookDirection = new Vector3D(-5, -5, -5),
                UpDirection = new Vector3D(0, 1, 0)
            };

            // Создаем куб в коде (MeshBuilder -> ToMeshGeometry3D())
            var mb = new MeshBuilder();
            mb.AddBox(new Vector3(0, 0, 0), 1, 1, 1); // SharpDX.Vector3
            var mesh = mb.ToMeshGeometry3D();

            _cubeModel = new MeshGeometryModel3D
            {
                Geometry = mesh,
                Material = PhongMaterials.Green,
                Transform = Transform3D.Identity
            };
            view1.Items.Add(_cubeModel);

            // Подписываемся на изменения VM, чтобы обновлять Transform модели
            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            UpdateModelTransform();

            // Запускаем loop
            _gameLoop = new GameLoop(_viewModel);
        }
        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainViewModel.CubePosition))
                UpdateModelTransform();
        }

        private void UpdateModelTransform()
        {
            var p = _viewModel.CubePosition;
            _cubeModel.Transform = new TranslateTransform3D(p.X, p.Y, p.Z);
        }
    }
}