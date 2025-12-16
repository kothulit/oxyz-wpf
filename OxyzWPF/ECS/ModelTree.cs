using OxyzWPF.Contracts.ECS;

namespace OxyzWPF.ECS;

public class ModelTree : IModelTree
{
    private Dictionary<int, ModelNode> _nodes = new();

    public IEnumerable<ModelNode> Nodes => _nodes.Values;
    public int CurrentParentId { get; set; } = -1;

    public ModelNode CreateNode(int entityId)
    {
        var newNode = new ModelNode(entityId, CurrentParentId);
        _nodes[entityId] = newNode;
        if (CurrentParentId != -1) _nodes[CurrentParentId].ChildrenIds.Add(entityId);
        return newNode;
    }

    public void RemoveNode(int entityId)
    {
        int parentId = _nodes[entityId].ParentId;
        _nodes[parentId].ChildrenIds.Remove(entityId);
        foreach (ModelNode child in Traverse(entityId))
        {
            _nodes.Remove(child.EntityId);
        }
    }

    public ModelNode? GetNode(int id)
    {
        _nodes.TryGetValue(id, out var node);
        return node;
    }

    public IEnumerable<ModelNode> Traverse(int rootId)
    {
        var stack = new Stack<int>();
        stack.Push(rootId);

        while (stack.Count > 0)
        {
            var id = stack.Pop();
            var node = _nodes[id];
            yield return node;

            foreach (var child in node.ChildrenIds)
                stack.Push(child);
        }
    }
}
