using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Controls;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.UI.ViewModels;
using SharpDX;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OxyzWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        private readonly IMessenger _messenger;
        public Viewport3DX ViewPort => viewPort;

        public MainWindow(MainViewModel mainViewModel, IMessenger messenger)
        {
            InitializeComponent();

            _messenger = messenger;
            _viewModel = mainViewModel;
            DataContext = _viewModel;

            viewPort.EffectsManager = new DefaultEffectsManager();

            // Создаем сетку горизонтальной плоскости
            CreateGrid();

            CompositionTarget.Rendering += (s, e) =>
            {
                var fps = viewPort.RenderHost?.RenderStatistics?.FPSStatistics?.AverageValue ?? 0;
                _viewModel.FPS = fps;
            };

            //Подписываемся на событие
            _messenger.Subscribe<GeometryEventArgs>(EventEnum.ElementAdded.ToString(), Create3DObject);
            _messenger.Subscribe<GeometryEventArgs>(EventEnum.ElementRemoved.ToString(), Remove3DObject);
        }

        private void Create3DObject(object sender, GeometryEventArgs e)
        {
            var mgm = e.GeometryModel;
            viewPort.Items.Add(mgm);
        }
        private void Remove3DObject(object sender, GeometryEventArgs e)
        {
            var mgm = e.GeometryModel;
            viewPort.Items.Remove(mgm);
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

        private void ViewPort_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                var position = e.GetPosition(viewPort);
                var point2D = new Vector2((float)position.X, (float)position.Y);
                _viewModel.OnMouseClick(point2D, viewPort);
            }
        }

        private void ViewPort_MouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(viewPort);
            var point2D = new Vector2((float)position.X, (float)position.Y);
            _viewModel.OnMouseClick(point2D, viewPort);
        }

        private void viewPort_KeyDown(object sender, KeyEventArgs e)
        {
            _viewModel.OnKeyDown(sender, e);
        }
    }
}