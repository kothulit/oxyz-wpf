using OxyzWPF.ECS;

namespace OxyzWPF.Contracts.ECS;

public interface IModelTree
{
    public int CurrentParentId { get; set; }
    public ModelNode CreateNode(int entityId);
    public void RemoveNode(int entityId);
    public ModelNode? GetNode(int id);
    public IEnumerable<ModelNode> Traverse(int rootId);
}
