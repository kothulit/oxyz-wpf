using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF.ECS;

/// <summary>
/// Мир - контейнер для всех сущностей и систем
/// </summary>
public class World : IWorld
{
    private List<Entity> _entities;
    private List<ISystem> _systems;
    private SystemManeger _systemManeger;

    public World(IMessenger messenger)
    {
        _entities = new List<Entity>();
        _systems = new List<ISystem>();
        _systemManeger = new SystemManeger(messenger, _systems);
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
        _systemManeger.Systems = _systems;
    }

    /// <summary>
    /// Удаляет систему
    /// </summary>
    public bool RemoveSystem(ISystem system)
    {
        var result = _systems.Remove(system);
        _systemManeger.Systems = _systems;
        return result;
    }

    /// <summary>
    /// Обновляет все системы
    /// </summary>
    public void Update(double deltaTime)
    {
        foreach (var system in _systems)
        {
            if (system.IsEnable) system.Update(deltaTime);
        }
    }

    /// <summary>
    /// Ужаляет все Entity
    /// </summary>
    public void Clear()
    {
        _entities.Clear();
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
