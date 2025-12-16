namespace OxyzWPF.ECS;

public class ModelNode
{
    private List<int> _childrenIds = new();

    public int EntityId { get; }
    public int ParentId { get; } = -1;
    public int ChildrenCount { get; }
    public List<int> ChildrenIds
    {
        get { return _childrenIds; }
        set
        {
            _childrenIds = value;
        }
    }
    public ModelNode(int entityId, int parentId)
    {
        EntityId = entityId;
        ParentId = parentId;
    }
}
