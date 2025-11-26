//⭐ Пример использования
//var graph = new ModelGraph();

//var building = graph.CreateNode(NodeType.Building);
//var floor1 = graph.CreateNode(NodeType.Floor);
//var flat101 = graph.CreateNode(NodeType.Flat);
//var room1 = graph.CreateNode(NodeType.Room);
//var room2 = graph.CreateNode(NodeType.Room);

//graph.AddChild(building.Id, floor1.Id);
//graph.AddChild(floor1.Id, flat101.Id);
//graph.AddChild(flat101.Id, room1.Id);
//graph.AddChild(flat101.Id, room2.Id);

//Console.WriteLine("Дерево здания:");
//foreach (var n in graph.Traverse(building.Id))
//{
//Console.WriteLine($"{n.Type} ({n.Id}) parent={n.ParentId}");
//}

//🧩 Привязка к ECS

//Обычно компонент:

//public struct ModelLink : IComponent
//{
//    public Guid ModelNodeId;
//}


//Когда система создаёт ECS-сущность:

//var node = graph.CreateNode(NodeType.Room);

//int entity = ecs.CreateEntity();
//ecs.AddComponent(entity, new ModelLink { ModelNodeId = node.Id });

//node.EcsEntityId = entity;

namespace OxyzWPF.Model;
public class ModelGraph
{
    //центральная структура данных

    private readonly Dictionary<Guid, ModelNode> _nodes = new();

    public event Action<ModelNode>? NodeAdded;
    public event Action<ModelNode>? NodeRemoved;
    public event Action<ModelNode>? NodeChanged;

    public ModelNode CreateNode(NodeType type)
    {
        var node = new ModelNode(type);
        _nodes[node.Id] = node;

        NodeAdded?.Invoke(node);
        return node;
    }

    public ModelNode? GetNode(Guid id)
    {
        _nodes.TryGetValue(id, out var node);
        return node;
    }

    public IEnumerable<ModelNode> Nodes => _nodes.Values;

    //Добавление ребёнка к родителю
    public void AddChild(Guid parentId, Guid childId)
    {
        var parent = _nodes[parentId];
        var child = _nodes[childId];

        // Если уже был родитель — удаляем оттуда
        if (child.ParentId.HasValue)
            RemoveChild(child.ParentId.Value, childId);

        parent.Children.Add(childId);
        child.ParentId = parentId;

        NodeChanged?.Invoke(parent);
        NodeChanged?.Invoke(child);
    }

    //Удаление узла (с рекурсией)
    public void RemoveNode(Guid id)
    {
        var node = _nodes[id];

        // удалить всех детей
        foreach (var child in node.Children.ToList())
            RemoveNode(child);

        // удалить из родителя
        if (node.ParentId.HasValue)
        {
            var parent = _nodes[node.ParentId.Value];
            parent.Children.Remove(id);
            NodeChanged?.Invoke(parent);
        }

        _nodes.Remove(id);
        NodeRemoved?.Invoke(node);
    }

    //Перемещение узла (смена родителя)
    public void MoveNode(Guid nodeId, Guid newParentId)
    {
        var node = _nodes[nodeId];

        if (node.ParentId.HasValue)
            RemoveChild(node.ParentId.Value, nodeId);

        AddChild(newParentId, nodeId);

        NodeChanged?.Invoke(node);
    }
    public void RemoveChild(Guid parentId, Guid childId)
    {
        var parent = _nodes[parentId];
        var child = _nodes[childId];

        parent.Children.Remove(childId);
        child.ParentId = null;
    }

    //Пример обхода графа
    public IEnumerable<ModelNode> Traverse(Guid rootId)
    {
        var stack = new Stack<Guid>();
        stack.Push(rootId);

        while (stack.Count > 0)
        {
            var id = stack.Pop();
            var node = _nodes[id];
            yield return node;

            foreach (var child in node.Children)
                stack.Push(child);
        }
    }
}