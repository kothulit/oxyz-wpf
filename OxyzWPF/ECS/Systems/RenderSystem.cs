using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.ECS.Components;

namespace OxyzWPF.ECS.Systems
{
    public class RenderSystem : ISystem
    {
        public string Name => typeof(RenderSystem).Name;
        public bool IsEnable { get; set; } = true;

        private readonly World _world;
        private readonly IMessenger _messenger;
        private readonly ISelection _selection;
        private readonly Dictionary<int, MeshGeometryModel3D> _renderedObjects;

        public RenderSystem(World world, IMessenger messenger, ISelection selection)
        {
            _world = world;
            _messenger = messenger;
            _selection = selection;
            _renderedObjects = new Dictionary<int, MeshGeometryModel3D>();
        }

        public void Update(double deltaTime)
        {
            // Получаем все сущности с компонентами Transform и Mesh
            var entitiesToRender = _world.GetEntitiesWithComponents<TransformComponent, MeshComponent>();

            // Обновляем существующие объекты
            foreach (var entity in entitiesToRender)
            {
                var transformComponent = entity.GetComponent<TransformComponent>()!;
                var meshComponent = entity.GetComponent<MeshComponent>()!;
                var selectionComponent = entity.GetComponent<SelectionComponent>();

                if (_renderedObjects.TryGetValue(entity.Id, out var model))
                {
                    // Обновляем трансформацию
                    model.Transform = transformComponent.GetTransform3D();

                    // Обновляем геометрию и материал если изменились
                    if (model.Geometry != meshComponent.Geometry)
                        model.Geometry = meshComponent.Geometry;
                    if (model.Material != meshComponent.Material)
                        model.Material = meshComponent.Material;

                    if (selectionComponent != null)
                    {
                        if (selectionComponent.IsSelected == true)
                        {
                            model.Material = PhongMaterials.Indigo;
                        }
                    }
                }
                else
                {
                    // Создаем новый объект для рендеринга
                    var newModel = new MeshGeometryModel3D
                    {
                        Geometry = meshComponent.Geometry,
                        Material = meshComponent.Material,
                        Tag = entity.Id,
                        Transform = transformComponent.GetTransform3D()
                    };

                    _renderedObjects[entity.Id] = newModel;
                    _messenger.Publish(EventEnum.ElementAdded.ToString(), this, new GeometryEventArgs(newModel));
                }
            }

            // Удаляем объекты, которые больше не существуют
            var entitiesToRemove = _renderedObjects.Keys
                .Where(id => _world.GetEntity(id) == null)
                .ToList();

            foreach (var id in entitiesToRemove)
            {
                if (_renderedObjects.TryGetValue(id, out var model))
                {
                    _messenger.Publish(EventEnum.ElementRemoved.ToString(), this, new GeometryEventArgs(model));
                    _renderedObjects.Remove(id);
                }
            }
        }
    }
}