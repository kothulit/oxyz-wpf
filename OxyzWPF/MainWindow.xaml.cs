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
        private GameLoop _gameLoop;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new MainViewModel();
            DataContext = _viewModel;

            view1.EffectsManager = new DefaultEffectsManager();

            // Создаем куб через MeshBuilder
            var mb = new MeshBuilder();
            mb.AddBox(new Vector3(0, 0, 0), 1, 1, 1);
            CubeModel.Geometry = mb.ToMeshGeometry3D();

            // Запускаем game loop
            _gameLoop = new GameLoop(_viewModel);
        }
    }
}