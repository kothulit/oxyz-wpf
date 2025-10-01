using System.Windows;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using OxyzWPF.UI.ViewModels;
using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        private readonly IMailer _mailer;
        public Viewport3DX ViewPort => viewPort;

        public MainWindow(MainViewModel mainViewModel, IMailer mailer)
        {
            InitializeComponent();

            _mailer = mailer;
            _viewModel = mainViewModel;
            DataContext = _viewModel;

            viewPort.EffectsManager = new DefaultEffectsManager();

            // Создаем сетку горизонтальной плоскости
            CreateGrid();

            // Создаем куб через MeshBuilder
            var mb = new MeshBuilder();
            mb.AddBox(new Vector3(0, 0, 0), 1, 1, 1);
            CubeModel.Geometry = mb.ToMeshGeometry3D();

            //Подписываемся на событие Object3DAdded
            mailer.Subscribe<object>("Object3DAdded", Create3DObject);
            mailer.Subscribe<object>("Object3DRemoved", Remove3DObject);
        }

        private void Create3DObject(object newModel)
        {
            var mgm = newModel as MeshGeometryModel3D;
            viewPort.Items.Add(mgm);
        }
        private void Remove3DObject(object newModel)
        {
            var mgm = newModel as MeshGeometryModel3D;
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

        private void Viewport3DX_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                var position = e.GetPosition(viewPort);
                var point2D = new Vector2((float)position.X, (float)position.Y);
                _viewModel.OnMouseClick(point2D, viewPort);
            }
        }
    }
}