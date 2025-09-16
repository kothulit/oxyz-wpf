using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using OxyzWPF.UI.ViewModels;
using OxyzWPF.Game;
using OxyzWPF.ECS.Systems;
using OxyzWPF.ECS;
using OxyzWPF.ECS.Components;

namespace OxyzWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        private GameLoop _gameLoop;
        private World _world;
        private RenderSystem _renderSystem;

        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();

            _viewModel = mainViewModel;
            DataContext = _viewModel;

            view1.EffectsManager = new DefaultEffectsManager();

            // Инициализируем ECS
            InitializeECS();

            // Передаем мир в ViewModel
            _viewModel.SetWorld(_world);

            // Создаем сетку горизонтальной плоскости
            CreateGrid();

            // Создаем тестовые объекты через ECS
            CreateTestEntities();

            // Создаем куб через MeshBuilder
            var mb = new MeshBuilder();
            mb.AddBox(new Vector3(0, 0, 0), 1, 1, 1);
            CubeModel.Geometry = mb.ToMeshGeometry3D();

            // Запускаем game loop
            _gameLoop = new GameLoop(_viewModel, _world);
        }

        private void InitializeECS()
        {
            _world = new World();
            _renderSystem = new RenderSystem(_world, view1);
            _world.AddSystem(_renderSystem);
        }

        private void CreateTestEntities()
        {
            // Создаем куб через ECS
            var cubeEntity = _world.CreateEntity("Test Cube");
            var cubeTransform = cubeEntity.AddComponent<TransformComponent>();
            cubeTransform.Position = new Vector3(0, 0.5f, 0); // Поднимаем куб над сеткой

            var cubeMesh = cubeEntity.AddComponent<MeshComponent>();
            var mb = new MeshBuilder();
            mb.AddBox(new Vector3(0, 0, 0), 1, 1, 1);
            cubeMesh.Geometry = mb.ToMeshGeometry3D();
            cubeMesh.Material = PhongMaterials.Green;

            // Создаем еще один объект
            var sphereEntity = _world.CreateEntity("Test Sphere");
            var sphereTransform = sphereEntity.AddComponent<TransformComponent>();
            sphereTransform.Position = new Vector3(2, 0.5f, 0);

            var sphereMesh = sphereEntity.AddComponent<MeshComponent>();
            var sphereBuilder = new MeshBuilder();
            sphereBuilder.AddSphere(new Vector3(0, 0, 0), 0.5f);
            sphereMesh.Geometry = sphereBuilder.ToMeshGeometry3D();
            sphereMesh.Material = PhongMaterials.Red;
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

        private void Viewport3DX_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                var position = e.GetPosition(view1);
                var point2D = new Vector2((float)position.X, (float)position.Y);
                _viewModel.OnMouseClick(point2D, view1);
            }
        }
    }
}