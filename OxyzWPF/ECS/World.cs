using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF.ECS;

public class World : IOxyzWorld
{
    private List<Entity> _entities;
    private List<IOxyzSystem> _systems;
    private SystemManeger _systemManeger;

    public World(IMessenger messenger)
    {
        _entities = new List<Entity>();
        _systems = new List<IOxyzSystem>();
        _systemManeger = new SystemManeger(messenger, _systems);
    }

    public Entity CreateEntity(string name = "")
    {
        var entity = new Entity(name);
        _entities.Add(entity);
        return entity;
    }

    public bool RemoveEntity(Entity entity)
    {
        return _entities.Remove(entity);
    }

    public bool RemoveEntity(int entityId)
    {
        var entity = _entities.FirstOrDefault(e => e.Id == entityId);
        return entity != null && _entities.Remove(entity);
    }

    public Entity? GetEntity(int entityId)
    {
        return _entities.FirstOrDefault(e => e.Id == entityId);
    }

    public List<Entity> GetEntities(List<int> entityId)
    {
        return _entities.Where(e => entityId.Contains(e.Id)).ToList();
    }

    public IEnumerable<Entity> GetAllEntities()
    {
        return _entities;
    }
    public IEnumerable<Entity> GetEntitiesWithComponents<T1>() where T1 : class, IOxyzComponent
    {
        return _entities.Where(e => e.HasComponent<T1>());
    }

    public IEnumerable<Entity> GetEntitiesWithComponents<T1, T2>()
        where T1 : class, IOxyzComponent
        where T2 : class, IOxyzComponent
    {
        return _entities.Where(e => e.HasComponent<T1>() && e.HasComponent<T2>());
    }

    public IEnumerable<Entity> GetEntitiesWithComponents<T1, T2, T3>()
        where T1 : class, IOxyzComponent
        where T2 : class, IOxyzComponent
        where T3 : class, IOxyzComponent
    {
        return _entities.Where(e => e.HasComponent<T1>() && e.HasComponent<T2>() && e.HasComponent<T3>());
    }

    public void AddSystem(IOxyzSystem system)
    {
        _systems.Add(system);
        _systemManeger.Systems = _systems;
    }

    public bool RemoveSystem(IOxyzSystem system)
    {
        var result = _systems.Remove(system);
        _systemManeger.Systems = _systems;
        return result;
    }

    public void Update(double deltaTime)
    {
        foreach (var system in _systems)
        {
            if (system.IsEnable) system.Update(deltaTime);
        }
    }

    public void Clear()
    {
        _entities.Clear();
    }

    public int EntityCount => _entities.Count;

    public int SystemCount => _systems.Count;
}
