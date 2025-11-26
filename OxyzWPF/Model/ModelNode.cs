namespace OxyzWPF.Model;

public class ModelNode
{
    public Guid Id { get; }
    public NodeType Type { get; set; }
    public Guid? ParentId { get; internal set; }
    public List<Guid> Children { get; } = new();

    // Можно хранить ссылку на ECS-сущность:
    public int? EcsEntityId { get; set; }

    public ModelNode(NodeType type)
    {
        Id = Guid.NewGuid();
        Type = type;
    }
}