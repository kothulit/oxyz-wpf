using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.ECS.Components;

namespace OxyzWPF.ECS.Systems;

public class SelectionSystem : IOxyzSystem
{
    private readonly World _world;
    private readonly IMessenger _messenger;
    private readonly ISelection _selection;
    public string Name => typeof(SelectionSystem).Name;
    public bool IsEnable { get; set; } = true;

    public SelectionSystem(World world, IMessenger messenger, ISelection selection)
    {
        _world = world;
        _messenger = messenger;
        _selection = selection;
    }

    public void Update(double deltaTime)
    {
        var enities = _world.GetEntitiesWithComponents<SelectionComponent>();
        if (_selection.SelectionIds != null)
        {
            foreach (var en in enities)
            {
                if (_selection.SelectionIds.Contains(en.Id))
                {
                    en.GetComponent<SelectionComponent>().IsSelected = true;
                }
                else
                {
                    en.GetComponent<SelectionComponent>().IsSelected = false;
                }
            }
        }
    }
}
