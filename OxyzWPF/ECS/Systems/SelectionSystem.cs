using HelixToolkit.Wpf.SharpDX;
using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Mailing;

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
}