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

            // Создаем сетку горизонтальной плоскости
            CreateGrid();

            // Создаем куб через MeshBuilder
            var mb = new MeshBuilder();
            mb.AddBox(new Vector3(0, 0, 0), 1, 1, 1);
            CubeModel.Geometry = mb.ToMeshGeometry3D();

            // Запускаем game loop
            _gameLoop = new GameLoop(_viewModel);
        }

        private void CreateGrid()
        {
            var gridBuilder = new MeshBuilder();

            // Параметры сетки
            int gridSize = 20; // Размер сетки (20x20)
            float cellSize = 1.0f; // Размер одной ячейки
            float halfSize = (gridSize * cellSize) / 2.0f;

            // Создаем линии сетки
            for (int i = 0; i <= gridSize; i++)
            {
                float pos = -halfSize + i * cellSize;

                // Вертикальные линии (параллельные оси Z)
                gridBuilder.AddCylinder(
                    new Vector3(pos, 0, -halfSize),
                    new Vector3(pos, 0, halfSize),
                    0.01f, // Толщина линии
                    8 // Количество сегментов
                );

                // Горизонтальные линии (параллельные оси X)
                gridBuilder.AddCylinder(
                    new Vector3(-halfSize, 0, pos),
                    new Vector3(halfSize, 0, pos),
                    0.01f, // Толщина линии
                    8 // Количество сегментов
                );
            }

            // Применяем геометрию к модели сетки
            GridModel.Geometry = gridBuilder.ToMeshGeometry3D();
        }
    }
}