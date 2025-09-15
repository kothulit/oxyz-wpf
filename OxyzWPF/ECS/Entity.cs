namespace OxyzWPF.ECS;

/// <summary>
/// Сущность - контейнер для компонентов с уникальным ID
/// </summary>
public class Entity
{
    private static int _nextId = 1;

    public int Id { get; }
    public string Name { get; set; }
    private readonly Dictionary<Type, IComponent> _components;

    public Entity(string name = "")
    {
        Id = _nextId++;
        Name = string.IsNullOrEmpty(name) ? $"Entity_{Id}" : name;
        _components = new Dictionary<Type, IComponent>();
    }

    /// <summary>
    /// Добавляет компонент к сущности
    /// </summary>
    public T AddComponent<T>() where T : class, IComponent, new()
    {
        var component = new T();
        _components[typeof(T)] = component;
        return component;
    }

    /// <summary>
    /// Добавляет существующий компонент к сущности
    /// </summary>
    public void AddComponent<T>(T component) where T : class, IComponent
    {
        _components[typeof(T)] = component;
    }

    /// <summary>
    /// Получает компонент по типу
    /// </summary>
    public T? GetComponent<T>() where T : class, IComponent
    {
        return _components.TryGetValue(typeof(T), out var component) ? component as T : null;
    }

    /// <summary>
    /// Проверяет наличие компонента
    /// </summary>
    public bool HasComponent<T>() where T : class, IComponent
    {
        return _components.ContainsKey(typeof(T));
    }

    /// <summary>
    /// Удаляет компонент
    /// </summary>
    public bool RemoveComponent<T>() where T : class, IComponent
    {
        return _components.Remove(typeof(T));
    }

    /// <summary>
    /// Получает все компоненты сущности
    /// </summary>
    public IEnumerable<IComponent> GetAllComponents()
    {
        return _components.Values;
    }

    public override string ToString()
    {
        return $"{Name} (ID: {Id})";
    }
}