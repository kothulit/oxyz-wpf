using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Instruction;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.ECS;
using OxyzWPF.ECS.Components;
using SharpDX;

namespace OxyzWPF.Instruction.Instrutions;

internal class ExtrudeContour : BaseInstruction, IInstruction
{
    public string Name { get; } = nameof(ExtrudeContour);
    private List<Vector2> _contourPoints = new List<Vector2>();
    private int _surfaceEntyId = -1;
    private bool _isContourComplete = false;
    private bool _isPreviosPointEnable = false;
    private Vector3 _previosPoint;
    private float _extrusionHeight = 1.0f; // Высота выдавливания по умолчанию

    public ExtrudeContour(World world, IMessenger messenger, IInstructor instructor)
        : base(world, messenger, instructor) { }

    public void OnStart(object args)
    {
        _messenger.Publish(EventEnum.GameStateChangeRequest.ToString(), this,
            new GameStateChangeRequestEventArgsy(GameStateEnum.EntityAdding.ToString()));
        _instructor.ActiveInstruction = this;
        _contourPoints.Clear();
        _isContourComplete = false;

        _messenger.Publish(EventEnum.StatusChangedEvent.ToString(), this,
            new StatusEventArgs("Режим создания контура для выдавливания. Кликните точки контура, затем нажмите Enter для завершения."));
        _messenger.Subscribe<EventArgs>(EventEnum.Apply.ToString(), OnApply);
    }

    public void Execute(object args)
    {
        if (!_isContourComplete)
        {
            // Добавляем точку в контур
            Vector3 currentPoint = (args as InstructionCallEventArgs).ScenePoint;
            _contourPoints.Add(new Vector2(currentPoint.X, currentPoint.Z));

            // Рисуем временные объекты
            // Точку
            Factory.CreatePoint(_world, currentPoint);
            // Линию
            if (_isPreviosPointEnable) Factory.CreateLine(_world, currentPoint, _previosPoint);
            // Грань на нуле (пока создаются новые)
            if (_contourPoints.Count > 2  && _surfaceEntyId == -1)
            {
                
                _surfaceEntyId = Factory.CreatSurface(_world, _contourPoints, new Plane(new Vector3(0, 1, 0), 0)).Id;
            }
            else if (_contourPoints.Count > 2)
            {
                _surfaceEntyId = Factory.CreatSurface(_world, _contourPoints, new Plane(new Vector3(0, 1, 0), 0)).Id;
            }
            _previosPoint = currentPoint;
            _isPreviosPointEnable = true;

            _messenger.Publish(EventEnum.StatusChangedEvent.ToString(), this,
                new StatusEventArgs($"Добавлена точка контура: ({currentPoint.X:F2}, {currentPoint.Z:F2}). Всего точек: {_contourPoints.Count}"));
        }
        else
        {
            if (_isPreviosPointEnable) Factory.CreateLine(_world, new Vector3(_contourPoints[0].X, 0, _contourPoints[0].Y), _previosPoint);
            // Устанавливаем высоту 5
            _extrusionHeight = 5.0f;

            // Создаем объект с контуром и высотой
            CreateExtrudedObject();
        }
    }

    public void OnEnd(object args)
    {
        if (!_isContourComplete && _contourPoints.Count >= 3)
        {
            // Завершаем контур и переходим к установке высоты
            _isContourComplete = true;
            _messenger.Publish(EventEnum.StatusChangedEvent.ToString(), this,
                new StatusEventArgs($"Контур завершен. Установите высоту выдавливания кликом мыши."));
        }
        else if (_isContourComplete)
        {
            // Завершаем операцию
            _messenger.Publish(EventEnum.GameStateChangeRequest.ToString(), this,
                new GameStateChangeRequestEventArgsy(GameStateEnum.Browsing.ToString()));
            _contourPoints.Clear();
            _isContourComplete = false;
        }
    }

    private void OnApply(object _, EventArgs e)
    {
        _isContourComplete = true;
        Execute(e);
    }

    private void CreateExtrudedObject()
    {
        if (_contourPoints.Count < 3)
        {
            _messenger.Publish(EventEnum.StatusChangedEvent.ToString(), this,
                new StatusEventArgs("Ошибка: для выдавливания нужно минимум 3 точки контура."));
            return;
        }

        var extrudedEntity = _world.CreateEntity($"Extruded_{_world.EntityCount}");
        var selection = extrudedEntity.AddComponent<SelectionComponent>();
        selection.IsSelected = false;

        var transform = extrudedEntity.AddComponent<TransformComponent>();
        // Позиция будет в центре контура
        var center = CalculateContourCenter(_contourPoints);
        transform.Position = new Vector3(center.X, _extrusionHeight / 2.0f, center.Y);

        // Добавляем SpaceComponent с контуром и высотой
        var spaceComponent = extrudedEntity.AddComponent<SpaceComponent>();
        spaceComponent.Contour = new List<Vector2>(_contourPoints);
        spaceComponent.Height = _extrusionHeight;

        // Создаем меш сразу (или можно сделать через систему)
        var meshComponent = extrudedEntity.AddComponent<MeshComponent>();
        meshComponent.Geometry = CreateExtrudedMesh(_contourPoints, _extrusionHeight);
        meshComponent.Material = PhongMaterials.Green; // Зеленый цвет для выдавленных объектов

        _messenger.Publish(EventEnum.StatusChangedEvent.ToString(), this,
            new StatusEventArgs($"Создан выдавленный объект с высотой {_extrusionHeight:F2}"));
    }

    private Vector2 CalculateContourCenter(List<Vector2> points)
    {
        float sumX = 0, sumY = 0;
        foreach (var point in points)
        {
            sumX += point.X;
            sumY += point.Y;
        }
        return new Vector2(sumX / points.Count, sumY / points.Count);
    }

    private MeshGeometry3D CreateExtrudedMesh(List<Vector2> contour, float height)
    {
        var meshBuilder = new MeshBuilder();

        // Преобразуем 2D точки контура в 3D (на плоскости Y=0)
        var bottomPoints = contour.Select(p => new Vector3(p.X, 0, p.Y)).ToList();
        var topPoints = contour.Select(p => new Vector3(p.X, height, p.Y)).ToList();

        // 1. Нижняя грань (базовый контур)
        if (bottomPoints.Count > 2)
        {
            meshBuilder.AddPolygon(bottomPoints);
        }

        // 2. Верхняя грань (контур на высоте)
        if (topPoints.Count > 2)
        {
            // Переворачиваем порядок точек для правильной нормали
            topPoints.Reverse();
            meshBuilder.AddPolygon(topPoints);
        }

        // 3. Боковые грани (стенки между точками контура)
        for (int i = 0; i < contour.Count; i++)
        {
            int nextIndex = (i + 1) % contour.Count;

            var bottom1 = bottomPoints[i];
            var bottom2 = bottomPoints[nextIndex];
            var top1 = topPoints[i];
            var top2 = topPoints[nextIndex];

            // Создаем четырехугольник для боковой грани
            meshBuilder.AddQuad(
                bottom1,  // Нижняя левая
                bottom2,  // Нижняя правая
                top2,     // Верхняя правая
                top1      // Верхняя левая
            );
        }

        return meshBuilder.ToMeshGeometry3D();
    }
}
