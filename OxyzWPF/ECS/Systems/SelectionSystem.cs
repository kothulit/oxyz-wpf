using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.ECS.Components;

namespace OxyzWPF.ECS.Systems;

internal class SelectionSystem : ISystem
{
    private readonly IWorld _world;
    private readonly IMailer _mailer;
    private List<int> _selectedObjectIds;

    public SelectionSystem(IWorld world, IMailer mailer)
    {
        _world = world;
        _mailer = mailer;
        _selectedObjectIds = new List<int>();
    }

    public void Update(double deltaTime)
    {
        throw new NotImplementedException();
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