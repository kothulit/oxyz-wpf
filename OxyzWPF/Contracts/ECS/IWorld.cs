using OxyzWPF.ECS;

namespace OxyzWPF.Contracts.ECS;

public interface IWorld
{
    public int EntityCount { get; }
    public int SystemCount { get; }
    public Entity CreateEntity(string name = "");
    public bool RemoveEntity(int entityId);
    public Entity? GetEntity(int entityId);
    public IEnumerable<Entity> GetEntitiesWithComponents<T1>() where T1 : class, IOxyzComponent;
    public IEnumerable<Entity> GetEntitiesWithComponents<T1, T2>()
        where T1 : class, IOxyzComponent
        where T2 : class, IOxyzComponent;
    public IEnumerable<Entity> GetEntitiesWithComponents<T1, T2, T3>()
        where T1 : class, IOxyzComponent
        where T2 : class, IOxyzComponent
        where T3 : class, IOxyzComponent;

    public void AddSystem(ISystem system);
    public bool RemoveSystem(ISystem system);
    public void Update(double deltaTime);
    public void Clear();
}
