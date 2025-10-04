using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.ECS.Components;

namespace OxyzWPF.ECS.Systems
{
    /// <summary>
    /// Система для рендеринга 3D объектов
    /// </summary>
    public class RenderSystem : ISystem
    {
        private readonly IWorld _world;
        private readonly IMailer _mailer;
        private readonly Dictionary<int, MeshGeometryModel3D> _renderedObjects;

        public RenderSystem(IWorld world, IMailer mailer)
        {
            _world = world;
            _mailer = mailer;
            _renderedObjects = new Dictionary<int, MeshGeometryModel3D>();
        }

        public void Update(double deltaTime)
        {
            // Получаем все сущности с компонентами Transform и Mesh
            var entitiesToRender = _world.GetEntitiesWithComponents<TransformComponent, MeshComponent>();

            // Обновляем существующие объекты
            foreach (var entity in entitiesToRender)
            {
                var transform = entity.GetComponent<TransformComponent>()!;
                var mesh = entity.GetComponent<MeshComponent>()!;

                if (_renderedObjects.TryGetValue(entity.Id, out var model))
                {
                    // Обновляем трансформацию
                    model.Transform = transform.GetTransform3D();

                    // Обновляем геометрию и материал если изменились
                    if (model.Geometry != mesh.Geometry)
                        model.Geometry = mesh.Geometry;
                    if (model.Material != mesh.Material)
                        model.Material = mesh.Material;
                }
                else
                {
                    // Создаем новый объект для рендеринга
                    var newModel = new MeshGeometryModel3D
                    {
                        Geometry = mesh.Geometry,
                        Material = mesh.Material,
                        Transform = transform.GetTransform3D()
                    };

                    _renderedObjects[entity.Id] = newModel;
                    _mailer.Publish(EventEnum.ObjectAdded, newModel);
                    
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
                    _mailer.Publish(EventEnum.ObjectRemoved, model);
                    _renderedObjects.Remove(id);
                }
            }
        }
    }
}