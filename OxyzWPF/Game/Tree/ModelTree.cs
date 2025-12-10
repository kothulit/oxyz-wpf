namespace OxyzWPF.Game.Tree;

public class ModelTree
{
    private Dictionary<int, ModelNode> _nodes = new();

    public IEnumerable<ModelNode> Nodes => _nodes.Values;

    public ModelNode CreateNode(int entityId, int parentId)
    {
        var newNode = new ModelNode(entityId, parentId);
        _nodes[entityId] = newNode;
        _nodes[parentId].ChildrenIds.Add(entityId);
        return newNode;
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
