namespace OxyzWPF.ECS.Components;

/// <summary>
/// Компонент для имени объекта
/// </summary>
public class NameComponent : IComponent
{
    public string Name { get; set; }

    public NameComponent(string name = "")
    {
        Name = name;
    }

    public override string ToString()
    {
        return $"Name({Name})";
    }
}