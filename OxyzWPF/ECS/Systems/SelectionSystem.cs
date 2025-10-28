using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.ECS.Components;

namespace OxyzWPF.ECS.Systems;

public class SelectionSystem : ISystem
{
    public bool IsOn { get; set; } = false;

    private readonly IWorld _world;
    private readonly IMailer _mailer;
    private ISelection _selection;

    public SelectionSystem(IWorld world, IMailer mailer, ISelection selection)
    {
        _world = world;
        _mailer = mailer;
        _selection = selection;
    }

    public void Update(double deltaTime)
    {
        throw new NotImplementedException();
        if (_selection.SelectionIds.Count > 0)
        {
            foreach (var item in _selection.SelectionIds)
            {

            }
        }

        IsOn = false;
    }

    private void Select(int index)
    {
        _world.GetEntity(index).GetComponent<SelectionComponent>().IsSelected = true;
    }

    private void UnSelect(int index)
    {
        _world.GetEntity(index).GetComponent<SelectionComponent>().IsSelected = false;
    }
}