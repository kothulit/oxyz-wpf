using OxyzWPF.Contracts.ECS;

namespace OxyzWPF.ECS;

/// <summary>
/// Мир - контейнер для всех сущностей и систем
/// </summary>
public class World : IWorld
{
    private readonly List<Entity> _entities;
    private readonly List<ISystem> _systems;

    private readonly ISystemsSwitcher _systemsStateMachine;

    public World(ISystemsSwitcher systemsStateMachine)
    {
        _systemsStateMachine = systemsStateMachine;
        _entities = new List<Entity>();
        _systems = new List<ISystem>();
    }

    /// <summary>
    /// Создает новую сущность
    /// </summary>
    public Entity CreateEntity(string name = "")
    {
        var entity = new Entity(name);
        _entities.Add(entity);
        return entity;
    }

    /// <summary>
    /// Удаляет сущность
    /// </summary>
    public bool RemoveEntity(Entity entity)
    {
        return _entities.Remove(entity);
    }

    /// <summary>
    /// Удаляет сущность по ID
    /// </summary>
    public bool RemoveEntity(int entityId)
    {
        var entity = _entities.FirstOrDefault(e => e.Id == entityId);
        return entity != null && _entities.Remove(entity);
    }

    /// <summary>
    /// Получает сущность по ID
    /// </summary>
    public Entity? GetEntity(int entityId)
    {
        return _entities.FirstOrDefault(e => e.Id == entityId);
    }

    /// <summary>
    /// Получает все сущности
    /// </summary>
    public IEnumerable<Entity> GetAllEntities()
    {
        return _entities;
    }

    /// <summary>
    /// Получает сущности с определенными компонентами
    /// </summary>
    public IEnumerable<Entity> GetEntitiesWithComponents<T1>() where T1 : class, IComponent
    {
        return _entities.Where(e => e.HasComponent<T1>());
    }

    /// <summary>
    /// Получает сущности с определенными компонентами
    /// </summary>
    public IEnumerable<Entity> GetEntitiesWithComponents<T1, T2>()
        where T1 : class, IComponent
        where T2 : class, IComponent
    {
        return _entities.Where(e => e.HasComponent<T1>() && e.HasComponent<T2>());
    }

    /// <summary>
    /// Получает сущности с определенными компонентами
    /// </summary>
    public IEnumerable<Entity> GetEntitiesWithComponents<T1, T2, T3>()
        where T1 : class, IComponent
        where T2 : class, IComponent
        where T3 : class, IComponent
    {
        return _entities.Where(e => e.HasComponent<T1>() && e.HasComponent<T2>() && e.HasComponent<T3>());
    }

    /// <summary>
    /// Добавляет систему
    /// </summary>
    public void AddSystem(ISystem system)
    {
        _systems.Add(system);
    }

    /// <summary>
    /// Удаляет систему
    /// </summary>
    public bool RemoveSystem(ISystem system)
    {
        return _systems.Remove(system);
    }

    /// <summary>
    /// Обновляет все системы
    /// </summary>
    public void Update(double deltaTime)
    {
        foreach (var system in _systems)
        {
            system.Update(deltaTime);
        }
    }

    /// <summary>
    /// Получает количество сущностей
    /// </summary>
    public int EntityCount => _entities.Count;

    /// <summary>
    /// Получает количество систем
    /// </summary>
    public int SystemCount => _systems.Count;
}
